#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "flag.h"
#include "getopt.h"
#include "mread.h"
#ifndef PI
#define PI 3.141592
#endif
#include "dct.h"

/*
chngsamp /i fname /o fname /t n
*/


#define IFILE 0x0001
#define OFILE 0x0002
#define RATIO 0x0008
#define BLOCK 0x0020
#define ALLF  (IFILE|OFILE|BLOCK)
#define EFLAG 0x0100
#define BFLAG 0x0200

void usage(void)
{
	fputs("DCT.EXE [option]\n",stderr);
	fputs("  option:\n",stderr);
	fputs("\t/i: input file name\n",stderr);
	fputs("\t/o: output file name\n",stderr);
	fputs("\t/b: block size(integer)\n",stderr);
	fputs("\t/r: ratio(ex:1.5)\n",stderr);
}

int main(int ac,char *av[])
{
	DARRAY *original,*result,*blockdata,*resultblockdata;
	char *dmy,*opt="i:o:b:r:";
	double c,ratio;
	int flag,i,j,k,blocksize,blocknum;
	char *ofile,*ifile;
	FILE *fp;

	allresetflag(&flag);

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
				case 'r':
					ratio = fabs(strtod(optarg,&dmy));
					c = sqrt(ratio);
					setflag(&flag,RATIO);
					break;
				case 'b':
					blocksize = atoi(optarg);
					setflag(&flag,BLOCK);
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
		fputs("not enouph parameter",stderr);
		usage();
		return;
	}

	if( !MreadInit(256,1024) ){
		fputs("MreadInit() failed",stderr);
		return;
	}


	MREADDEFAULTCLM=1;
	if( (original=MultiReadCmd(ifile)) == NULL ){
		MreadErrorMes(stderr);
		fputs("MultiRead() failed",stderr);
		return;
	}else{
		MreadErrorMes(stderr);
	}


	if( !isflag(flag,BLOCK) ){
		blocksize = original->n;
	}else{
		if( blocksize <= 0 ){
			fputs("block size must be over zero\n",stderr);
			return;
		}
	}


	if( (blockdata=NewDary(blocksize)) == NULL ){
		FreeDary(original);
		fputs("memory allocation error for blockdata\n",stderr);
		return;
	}

	if( (resultblockdata=NewDary((int)((double)blocksize*ratio))) == NULL ){
		FreeDary(original);
		FreeDary(blockdata);
		fputs("memory allocation error for blockdata\n",stderr);
		return;
	}

	if( (original->n) % blocksize )
		blocknum = (original->n) / blocksize + 1;
	else
		blocknum = (original->n) / blocksize;

	if( (result=NewDary(blocknum * (resultblockdata->n))) == NULL ){
		FreeDary(original);
		FreeDary(blockdata);
		FreeDary(resultblockdata);
		fputs("memory allocation error for blockdata\n",stderr);
		return;
	}
	

	for(i=0;i<blocknum;++i){
		for(j=0;j<(blockdata->n);++j){
			k = i * (blockdata->n) + j;
			if( k < (original->n) )
				blockdata->ary[j] = original->ary[k];
			else
				blockdata->ary[j] = 0.0;
		}

		dct(blockdata->ary,blockdata->n);
		
		ClrDary(resultblockdata);
		if( resultblockdata->n > blockdata->n ){
			for(j=0;j<(blockdata->n);++j)
				resultblockdata->ary[j] = blockdata->ary[j];
		}else{
			for(j=0;j<(resultblockdata->n);++j)
				resultblockdata->ary[j] = blockdata->ary[j];
		}

		idct(resultblockdata->ary,resultblockdata->n);

		for(j=0;j<(resultblockdata->n);++j)
			result->ary[i*(resultblockdata->n)+j] = resultblockdata->ary[j] * c;
	}



	if( (fp=fopen(ofile,"wt")) == NULL ){
		fputs("cant open for write file\n",stderr);
		return;
	}

	if( !MultiWrite(fp,1,result,NULL) ){
		fputs("MreadWrite() failed\n",stderr);
		MreadErrorMes(stderr);
	}else{
		MreadErrorMes(stderr);
	}

	FreeDary(result);
	FreeDary(original);
	FreeDary(blockdata);
	FreeDary(resultblockdata);
	MreadClose();

	return;
}
