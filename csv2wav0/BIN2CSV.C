
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
#define SWITCH 0x0010
#define ALLF  (OFILE|IFILE|SBIT)
#define EFLAG 0x0100
#define BFLAG 0x0200

static char *rcsid="$Header$";
typedef int (*FuncPtr)(FILE *);
FuncPtr get;

int getword(FILE *fp)
{
	long ret;
	ret  = (long)fgetc(fp);
	ret |= ((long)fgetc(fp))<<8L;
	if( ret & 0x8000 )
		ret |= 0xffff0000;
	return ret;
}

/*
long getdword(FILE *fp)
{
	long ret;
	ret  = (long)fgetc(fp);
	ret |= ((long)fgetc(fp))<<8L;
	ret |= ((long)fgetc(fp))<<16L;
	ret |= ((long)fgetc(fp))<<24L;
	return ret;
}
*/

void usage(void)
{
	fprintf(stderr,"%s\n",rcsid);
	fputs("BIN2CSV.EXE [option]\n",stderr);
	fputs("  option:\n",stderr);
	fputs("\t/i: input binary file name\n",stderr);
	fputs("\t/o: output csv text file name\n",stderr);
	fputs("\t/b: sampling bit 16 or 8\n",stderr);
	fputs("\t/c: channel 1 or 2\n",stderr);
	fputs("\t/s: if sampling bit is 8,true:128->0 false:none default:true\n",stderr);
}

int main(int ac,char *av[])
{
	char *dmy,*opt="i:o:b:c:s:",*ifile,*ofile;
	int flag,i,sbit,ch=1,n;
	FILE *fpb,*fpo;
	DARRAY *r=NULL,*l=NULL;
	long fsize;

	allresetflag(&flag);
	setflag(&flag,SWITCH);

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
				case 'c':
					ch = atoi(optarg);
					setflag(&flag,CHANNEL);
					break;
				case 's':
					if( atoi(optarg) )
						setflag(&flag,SWITCH);
					else
						resetflag(&flag,SWITCH);
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

	if( !( sbit == 8 || sbit == 16 ) ){
		fputs("Error:\n",stderr);
		fputs("unknown sampling bit\n",stderr);
		return;
	}

	if( !( ch == 1 || ch == 2 ) ){
		fputs("Error:\n",stderr);
		fputs("unknown channel\n",stderr);
		return;
	}


	if( (fpb=fopen(ifile,"rb")) == NULL ){
		fputs("Error:\n",stderr);
		fputs("can't open input file\n",stderr);
		return;
	}else{
		fsize=filelength(fileno(fpb));
	}


	if( ( sbit == 8 && ch == 2 ) || ( sbit == 16 && ch == 1 ) ){
		if( (fsize % 2) != 0 ){
			fputs("Error:\n",stderr);
			fputs("both file size and data mismatch\n",stderr);
			return;
		}else{
			n = (int)(fsize>>1);
		}
	}else if( sbit == 16 && ch == 2 ){
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


	if( (r=NewDary(n)) == NULL ){
		fputs("memory allocation error\n",stderr);
		return;
	}
	if( ch == 2 ){
		if( (l=NewDary(n)) == NULL ){
			fputs("memory allocation error\n",stderr);
			return;
		}
	}

	if( sbit == 8 )
		get = fgetc;
	else
		get = getword;

	for(i=0;i<n;++i){
		if( ch == 2)
			l->ary[i] = get(fpb);
		r->ary[i] = get(fpb);

		if( isflag(flag,SWITCH) && sbit == 8 ){
			if( ch == 2)
				l->ary[i] -= 128;
			r->ary[i] -= 128;
		}
		
	}

	fclose(fpb);
	if( (fpo=fopen(ofile,"wt")) == NULL){
		fputs("can't open output file\n",stderr);
		return;
	}

	if( !MultiWrite(fpo,1,r,l,NULL) ){
		fputs("MreadWrite() failed\n",stderr);
		MreadErrorMes(stderr);
	}else{
		MreadErrorMes(stderr);
	}
	fclose(fpo);
	return;
}
