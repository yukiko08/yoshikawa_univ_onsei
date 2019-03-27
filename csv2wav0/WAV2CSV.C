
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "mread.h"
#include "flag.h"
#include "getopt.h"

#define IFILE 0x0001
#define OFILE 0x0002
#define SBIT  0x0004
#define CHANNEL	0x0008
#define SHIFT 0x0010
#define COMMENT 0x0020
#define DISPLAY 0x0040
#define ALLF  (OFILE|IFILE)
#define EFLAG 0x0100
#define BFLAG 0x0200
#define WAVEHEADERSIZE 44

static char *rcsid="$Header$";
typedef int (*FuncPtr)(FILE *);
FuncPtr get;


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


void usage(void);
long getdword(FILE *fp);
int getword(FILE *fp);
void PrintWaveStat(WAVEHEADER *whd);
int ChkWaveHeader(WAVEHEADER *p);
WAVEHEADER *ReadWaveHeader(FILE *fp);


WAVEHEADER *ReadWaveHeader(FILE *fp)
{
	WAVEHEADER *p=NULL;
	char *chk;

	if( (p=(WAVEHEADER *)malloc( sizeof(WAVEHEADER) )) == NULL )
		return NULL;

	fgets(p->id,5,fp);
	p->wavefilesize = getdword(fp);
	fgets(p->wave,5,fp);
	fgets(p->fmt,5,fp);
	p->headersize   = getdword(fp);
	p->wavetype     = getword(fp);
	p->ch = getword(fp);
	p->samplingfreq = getdword(fp);
	p->datarate     = getdword(fp);
	p->samplesize   = getword(fp);
	p->samplebits   = getword(fp);
	chk = fgets(p->data,5,fp);
	p->pcmsize      = getdword(fp);

	if( chk == NULL ){
		free(p);
		p = NULL;
	}
	return p;
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

void PrintWaveStat(WAVEHEADER *whd)
{
	fprintf(stderr,"          [id]: %s\n",whd->id);
	fprintf(stderr,"  wavefilesize: %ld\n",whd->wavefilesize);
	fprintf(stderr,"        [wave]: %s\n",whd->wave);
	fprintf(stderr,"        [fmt_]: %s\n",whd->fmt);
	fprintf(stderr,"    headersize: %ld\n",whd->headersize);
	fprintf(stderr,"      wavetype: %d\n",whd->wavetype);
	fprintf(stderr,"            ch: %d\n",whd->ch);
	fprintf(stderr,"  samplingfreq: %ld\n",whd->samplingfreq);
	fprintf(stderr,"      datarate: %ld\n",whd->datarate);
	fprintf(stderr,"    samplesize: %d\n",whd->samplesize);
	fprintf(stderr,"    samplebits: %d\n",whd->samplebits);
	fprintf(stderr,"        [data]: %s\n",whd->data);
	fprintf(stderr,"       pcmsize: %ld\n",whd->pcmsize);
	fprintf(stderr,"\n");
	fprintf(stderr,"      Channels: %s\n",((whd->ch==1)?"Monoral":"Stereo"));
	fprintf(stderr," Sampling Rate: %ld Hz\n",whd->samplingfreq);
	fprintf(stderr,"      Byte/sec: %ld Byte\n",whd->datarate);
	fprintf(stderr,"   Byte/sample: %d Byte\n",whd->samplesize);
	fprintf(stderr,"  Sampling Bit: %d Bit\n",whd->samplebits);
	fprintf(stderr,"     Data Size: %ld Byte\n",whd->pcmsize);
	fprintf(stderr,"Number of data: %ld\n",(whd->pcmsize)/(long)(whd->samplesize));
	fprintf(stderr,"     Play Time: %.1lf Sec\n",(double)(whd->pcmsize)/(whd->datarate));
}


int getword(FILE *fp)
{
	long ret;
	ret  = (long)fgetc(fp);
	ret |= ((long)fgetc(fp))<<8L;
	if( ret & 0x8000 )
		ret |= 0xffff0000;
	return ret;
}


long getdword(FILE *fp)
{
	long ret;
	ret  = (long)fgetc(fp);
	ret |= ((long)fgetc(fp))<<8L;
	ret |= ((long)fgetc(fp))<<16L;
	ret |= ((long)fgetc(fp))<<24L;
	return ret;
}


void usage(void)
{
	fprintf(stderr,"%s\n",rcsid);
	fputs("WAV2CSV.EXE [option]\n",stderr);
	fputs("  option:\n",stderr);
	fputs("   /i: input wave file name\n",stderr);
	fputs("   /o: output csv text file name\n",stderr);
	fputs("\n  if you need,use under the option\n",stderr);
	fputs("   /s: if sampling bit is 8,true:128->0 true:shift false:none default:true\n",stderr);
	fputs("   /c: write comment wave file status to output file. true:write false:none default:false\n",stderr);
	fputs("   /d: display wave file header status. true:display /false:none default:false\n",stderr);
}

int main(int ac,char *av[])
{
	char *dmy,*opt="i:o:c:s:d:",*ifile,*ofile;
	int flag,i,sbit,ch=1,n;
	FILE *fpb,*fpo;
	DARRAY *r=NULL,*l=NULL;
	WAVEHEADER *whd;
	long fsize;

	allresetflag(&flag);
	setflag(&flag,SHIFT);

	if( ac > 1 ){
		while(1){
			switch( getopt(ac,av,opt) ){
				case 'i':
					ifile = optarg;
					setflag(&flag,IFILE);
					break;
				case 'o':
					ofile = optarg;
					setflag(&flag,OFILE);
					break;
				case 'b':
					sbit = atoi(optarg);
					setflag(&flag,SBIT);
					break;
				case 's':
					if( atoi(optarg) )
						setflag(&flag,SHIFT);
					else
						resetflag(&flag,SHIFT);
					break;
				case 'c':
					if( atoi(optarg) )
						setflag(&flag,COMMENT);
					else
						resetflag(&flag,COMMENT);
					break;
				case 'd':
					if( atoi(optarg) )
						setflag(&flag,DISPLAY);
					else
					resetflag(&flag,DISPLAY);
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


/*	オプションなしのパラメータがあるとき、エラーとする場合はコメントを外す */
	if( ac != optind ){
		fputs("error\n",stderr);
		usage();
		return;
	}


/* 最低限必要なパラメータ */
	if( !isflag(flag,ALLF) ){
		fputs("not enouph parameter\n",stderr);
		usage();
		return;
	}


	if( (fpb=fopen(ifile,"rb")) == NULL ){
		fputs("Error:\n",stderr);
		fputs("can't open input file\n",stderr);
		return;
	}else{
		fsize=filelength(fileno(fpb)) - WAVEHEADERSIZE;
	}

	if( (whd=ReadWaveHeader(fpb)) == NULL ){
		fputs("Error:\n",stderr);
		fputs("ReadWaveHeader() : return NULL\n",stderr);
		return;
	}

	if( !ChkWaveHeader(whd) ){
		fputs("Error:\n",stderr);
		fputs("ChkWaveHeader() : return false\n",stderr);
		return;
	}

	if( whd->pcmsize != fsize ){
		fputs("Error:\n",stderr);
		fputs("both data size and pcmsize is mismatch\n",stderr);
		return;
	}


	if( ( whd->samplebits == 8 && whd->ch == 2 ) || ( whd->samplebits == 16 && whd->ch == 1 ) ){
		if( (fsize % 2) != 0 ){
			fputs("Error:\n",stderr);
			fputs("both file size and data mismatch\n",stderr);
			return;
		}else{
			n = (int)(fsize>>1);
		}
	}else if( whd->samplebits == 16 && whd->ch == 2 ){
		if( (fsize % 4L) != 0 ){
			fputs("Error:\n",stderr);
			fputs("both file size and data mismatch\n",stderr);
			return;
		}else{
			n = (int)(fsize>>2);
		}
	}else{
		n = (int)fsize;
	}

	if( n != (int)( (whd->pcmsize)/(whd->samplesize) ) ){
		fputs("Error:\n",stderr);
		fputs("both file size and data mismatch\n",stderr);
		return;
	}



	if( (r=NewDary(n)) == NULL ){
		fputs("memory allocation error\n",stderr);
		return;
	}
	if( whd->ch == 2 ){
		if( (l=NewDary(n)) == NULL ){
			fputs("memory allocation error\n",stderr);
			return;
		}
	}

	if( whd->samplebits == 8 )
		get = fgetc;
	else
		get = getword;

	for(i=0;i<n;++i){
		if( whd->ch == 2)
			l->ary[i] = get(fpb);
		r->ary[i] = get(fpb);

		if( isflag(flag,SHIFT) && whd->samplebits == 8 ){
			if( whd->ch == 2)
				l->ary[i] -= 128;
			r->ary[i] -= 128;
		}
		
	}

	fclose(fpb);
	if( (fpo=fopen(ofile,"wt")) == NULL){
		fputs("can't open output file\n",stderr);
		return;
	}

	if( isflag(flag,COMMENT) ){
		fprintf(fpo,"#Wavefile name: %s\n",ifile);
		fprintf(fpo,"#Channel      : %d\n",whd->ch);
		fprintf(fpo,"#Sampling bits: %d\n",whd->samplebits);
		fprintf(fpo,"#Sampling freq: %ld\n",whd->samplingfreq);
		fprintf(fpo,"#Play Time    : %.1lf Sec\n",(double)(whd->pcmsize)/(whd->datarate));
	}

	if( !MultiWrite(fpo,1,r,l,NULL) ){
		fputs("MreadWrite() failed\n",stderr);
		MreadErrorMes(stderr);
	}else{
		if( isflag(flag,DISPLAY) )
			PrintWaveStat(whd);
		MreadErrorMes(stderr);
	}
	fclose(fpo);
	return;
}
