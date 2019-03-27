
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "mread.h"
#include "flag.h"
#include "getopt.h"
#define LFILE 0x0001
#define RFILE 0x0002
#define OFILE 0x0004
#define NUMBEROF 0x0008
#define SBIT 0x0010
#define SFREQ 0x0020
#define VOLUME 0x0040
#define ALLF  (RFILE|OFILE|SFREQ)
#define EFLAG 0x0100
#define BFLAG 0x0200

#define WAVEHEADERSIZE 44

typedef struct
{
	char id[4+1];     /* "RIFF"                                       */
	long wavefilesize;/* "WAVE"以降(08h)のサイズ == filesize - 8 byte */
	char wave[4+1];   /* "WAVE"                                       */
	char fmt[4+1];    /* "fmt "                                       */
	long headersize;  /* wavetype~samplingbitsのサイズ == 16          */
	int  wavetype;    /* 符号なし8/16bit PCM なら 1                   */
	int  ch;          /* モノラル : 1  ステレオ : 2                   */
	long samplingfreq;/* サンプリング周波数                           */
	long datarate;    /* １秒あたりのバイト数                         */
	int  samplesize;  /* １サンプルあたりのバイト数                   */
	int  samplebits;  /* 量子化ビット数                               */
	char data[4+1];   /* "data"                                       */
	long pcmsize;     /* 実データのサイズ                             */
}WAVEHEADER;


typedef int (*FuncPtr)(int,FILE *);
FuncPtr put;

int putbyte(int w,FILE *fp)
{
	return( putc(w+128,fp) );
}


int putword(int w,FILE *fp)
{
	putc( w & 0xff, fp );
	putc( (w>>8) & 0xff, fp );
	return w;
}


long putdword(long w,FILE *fp)
{
	putword( (int)(w & 0xffffL), fp );
	putword( (int)((w>>16L)&0xffffL), fp );
	return w;
}


void WriteHeader(FILE *fp,WAVEHEADER *whd)
{
	fprintf(fp,"%s",whd->id);
	putdword(whd->wavefilesize,fp);
	fprintf(fp,"%s",whd->wave);
	fprintf(fp,"%s",whd->fmt);
	putdword(whd->headersize,fp);
	putword(whd->wavetype,fp);
	putword(whd->ch,fp);
	putdword(whd->samplingfreq,fp);
	putdword(whd->datarate,fp);
	putword(whd->samplesize,fp);
	putword(whd->samplebits,fp);
	fprintf(fp,"%s",whd->data);
	putdword(whd->pcmsize,fp);
}

int ChkWaveHeader(WAVEHEADER *p)
{
	int c=0;
	c |= strncmp(p->id,"RIFF",4);
	c |= strncmp(p->wave,"WAVE",4);
	c |= strncmp(p->fmt,"fmt ",4);
	c |= strncmp(p->data,"data",4);

	c |= !( p->headersize == 16L );
	c |= !( p->wavetype == 1 );
/*c |= !( p->samplesize == ( (p->ch)* (p->ch) ) );*/
	c |= !( p->samplesize == ( (p->samplebits)/8 * (p->ch) ) );
	c |= !( p->datarate == ( p->samplingfreq * (long)(p->samplesize ) ) );
	c |= !( ( (p->pcmsize) + WAVEHEADERSIZE ) == ( (p->wavefilesize) + 8 ) );

	return(!c);
}

void usage(void)
{
	fputs("CSV2WAV.EXE [option]\n",stderr);
	fputs("  option:\n",stderr);
	fputs("\t/o: output file name\n",stderr);
	fputs("\t/r: <fname,clm> input right sound source file name\n",stderr);
	fputs("\t/f: sampling frequency(11025,22050,44100Hz)\n",stderr);
	fputs("\nunder the option need not use if you want.\n",stderr);
	fputs("\t/l: <fname,clm> input left sound source file name\n",stderr);
	fputs("\t/n: number of duplicate(default=1)\n",stderr);
	fputs("\t/b: sampling bit(default=16)(8 or 16)\n",stderr);
	fputs("\t/v: volume ratio(default=1.0)\n",stderr);
}

int main(int ac,char *av[])
{
	double rmax,lmax,max,normal,vol=1.0;
	char *dmy,*opt="l:r:o:n:b:f:v:";
	int sbit=16,sfreq,flag,i,j,n=1;
	char *ofile,*lfile,*rfile;
	DARRAY *lsource,*rsource;
	WAVEHEADER *whd;
	FILE *fp;

	allresetflag(&flag);

	if( ac > 1 ){
		while(1){
			switch( getopt(ac,av,opt) ){
				case 'o':
					ofile = optarg;
					setflag(&flag,OFILE);
					break;
				case 'l':
					lfile = optarg;
					setflag(&flag,LFILE);
					break;
				case 'r':
					rfile = optarg;
					setflag(&flag,RFILE);
					break;
				case 'n':
					n = atoi(optarg);
					setflag(&flag,NUMBEROF);
					break;
				case 'b':
					sbit = atoi(optarg);
					setflag(&flag,SBIT);
					break;
				case 'f':
					sfreq = atoi(optarg);
					setflag(&flag,SFREQ);
					break;
				case 'v':
					vol = strtod(optarg,&dmy);
					setflag(&flag,VOLUME);
					break;
				case  -1:
					setflag(&flag,BFLAG);
					break;
				case '?':
					setflag(&flag,EFLAG);
					break;
				default:
					fputs("unexpected error\n",stderr);
					exit(0);
			}

			if( isflag(flag,BFLAG) || isflag(flag,EFLAG) )
				break;
		}
	}

	if( isflag(flag,EFLAG) ){
		fputs("rised EFLAG\n",stderr);
		usage();
		return;
	}


/* オプションなしのパラメータがあるとき、エラーとする場合はコメントを外す */
	if( ac != optind ){
		fputs("error\n",stderr);
		usage();
		return;
	}


/* 最低限必要なパラメータ */
	if( !isflag(flag,ALLF) ){
		fputs("Error:\n",stderr);
		fputs("not enouph parameter\n",stderr);
		usage();
		return;
	}

	/* 量子化ビット数のチェック */
	if( sbit == 8 ){
		put = putbyte;
		normal = 127.0;
	}else if( sbit == 16 ){
		put = putword;
		normal = 32767.0;
	}else{
		fputs("Error:\n",stderr);
		fputs("sampling bit unknown\n",stderr);
		return;
	}

	/* サンプリング周波数のチェック */
	if( !( sfreq == 11025 || sfreq == 22050 || sfreq == 44100 ) ){
		fputs("Error:\n",stderr);
		fputs("sampling frequency unknown\n",stderr);
		return;
	}

	/* ボリュームのチェック */
	if( fabs(vol) > 1.0 ){
		fputs("Error:\n",stderr);
		fputs("volume ratio over range\n",stderr);
		return;
	}

/* ファイルからの読み込み */
	MREADDEFAULTCLM=1;
	if( !MreadInit(256,1024) ){
		fputs("Error:\n",stderr);
		fputs("MreadInit() failed",stderr);
		return;
	}


	if( isflag(flag,LFILE) ){
		if( (lsource=MultiReadCmd(lfile)) == NULL ){
			fputs("Error:\n",stderr);
			MreadErrorMes(stderr);
			fputs("MultiReadCmd():: failed for left sound source",stderr);
			return;
		}else{
			MreadErrorMes(stderr);
		}
	}


	if( (rsource=MultiReadCmd(rfile)) == NULL ){
		fputs("Error:\n",stderr);
		MreadErrorMes(stderr);
		fputs("MultiReadCmd():: failed for right sound source",stderr);
		return;
	}else{
		MreadErrorMes(stderr);
	}


	if( isflag(flag,LFILE) ){
		if( rsource->n != lsource->n ){
			fputs("Error:\n",stderr);
			fputs("right and left sounde source data is no match\n",stderr);
			return;
		}
	}



/* 絶対値の最大値の探索 */
	rmax = lmax = 0.0;
	if( isflag(flag,LFILE) ){
		lmax = fabs(lsource->ary[0]);
		for(i=0;i<(lsource->n);++i){
			if( fabs(lsource->ary[i]) > lmax )
				lmax = fabs(lsource->ary[i]);
		}
	}

	rmax = fabs(rsource->ary[0]);
	for(i=0;i<(rsource->n);++i){
		if( fabs(rsource->ary[i]) > rmax )
			rmax = fabs(rsource->ary[i]);
	}

	max = (rmax > lmax) ? rmax : lmax ;


/* 正規化 */
	for(i=0;i<rsource->n;++i){
		if( isflag(flag,LFILE) )
			lsource->ary[i] = ( lsource->ary[i] / max * vol ) * normal;
		rsource->ary[i] = ( rsource->ary[i] / max * vol ) * normal;
	}

	if( (whd=(WAVEHEADER *)malloc(sizeof(WAVEHEADER))) == NULL ){
		fputs("Error:\n",stderr);
		fputs("memory allocation error\n",stderr);
		return;
	}

/* ファイルへの書き込み */
	if( (fp=fopen(ofile,"wb")) == NULL ){
		fputs("Error:\n",stderr);
		fputs("can't open write file\n",stderr);
		return;
	}


	/* ヘッダの書き込み */
	strcpy(whd->id,"RIFF");
	strcpy(whd->wave,"WAVE");
	strcpy(whd->fmt,"fmt ");
	whd->headersize = 16L;
	whd->wavetype = 1;
	whd->ch = ( isflag(flag,LFILE) ? 2 : 1 );
	whd->samplingfreq = (long)sfreq;
	whd->samplesize = sbit/8 * ( isflag(flag,LFILE) ? 2 : 1 );;
	whd->samplebits = sbit;
	strcpy(whd->data,"data");
	whd->pcmsize = (long)(rsource->n) * (long)n * (long)(whd->samplesize);
	whd->wavefilesize = whd->pcmsize + (long)WAVEHEADERSIZE - 8L;
	whd->datarate = sfreq * (long)(whd->samplesize);

	if( ChkWaveHeader(whd) ){
		WriteHeader(fp,whd);
	}else{
		fputs("Error:\n",stderr);
		fputs("Header of wave file unrecognize.\n",stderr);
		return;
	}

	/* データの書き込み */
	for(i=0;i<n;++i){
		for(j=0;j<(rsource->n);++j){
			if( isflag(flag,LFILE) )
				put((int)(lsource->ary[j]),fp);
			put((int)(rsource->ary[j]),fp);
		}
	}


	fclose(fp);
/*
	if( !MultiWrite(stderr,1,rsource,NULL) ){
		fputs("MreadWrite() failed\n",stderr);
		MreadErrorMes(stderr);
	}else{
		MreadErrorMes(stderr);
	}
*/

}
