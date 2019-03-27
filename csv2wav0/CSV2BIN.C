
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
#define VOLUME 0x0040
#define ALLF  (RFILE|OFILE)
#define EFLAG 0x0100
#define BFLAG 0x0200

static char *rcsid="$Header$";
typedef int (*FuncPtr)(int,FILE *);
FuncPtr put;

int putword(int w,FILE *fp)
{
	putc( w & 0xff, fp );
	putc( (w>>8) & 0xff, fp );
	return w;
}

int putbyte(int w,FILE *fp)
{
	return( putc(w+128,fp) );
}

void usage(void)
{
	fputs("CSV2BIN.EXE [option]\n",stderr);
	fputs("  option:\n",stderr);
	fputs("\t/o: output file name\n",stderr);
	fputs("\t/r: <fname,clm> input right sound source file name\n",stderr);
	fputs("\nunder the option need not use if you want.\n",stderr);
	fputs("\t/l: <fname,clm> input left sound source file name\n",stderr);
	fputs("\t/n: number of duplicate(default=1)\n",stderr);
	fputs("\t/b: sampling bit(default=16)(8 or 16)\n",stderr);
	fputs("\t/v: volume ratio(default=1.0)\n",stderr);
}

int main(int ac,char *av[])
{
	double rmax,lmax,max,normal,vol=1.0;
	char *dmy,*opt="l:r:o:n:b:";
	int sbit=16,flag,i,j,n=1;
	char *ofile,*lfile,*rfile;
	DARRAY *lsource,*rsource;
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
			lsource->ary[i] = ( lsource->ary[i] / max ) * normal;
		rsource->ary[i] = ( rsource->ary[i] / max ) * normal;
	}


	if( (fp=fopen(ofile,"wb")) == NULL ){
		fputs("Error:\n",stderr);
		fputs("can't open write file\n",stderr);
		return;
	}

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
