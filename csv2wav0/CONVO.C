#include <math.h>
#include <stdio.h>
#include "mread.h"
#include "flag.h"
#include "getopt.h"
#define XFILE 0x0001
#define HFILE 0x0002
#define YFILE 0x0004
#define ALLF  (XFILE|HFILE|YFILE)
#define EFLAG 0x0010
#define BFLAG 0x0020
static char *rcsid="$Header: convo.cv  1.3  97/08/06 15:52:08  Exp $";


/*
ary 'y' need clear zero.
*/
void convo(double *x,int xn,double *h,int hn,double *y)
{
	int i,j,yn;
	double *xp,*hp,*yp;

	yn = xn + hn;
	yp = y;
	for(i=0;i<yn;++i,++yp){
		xp = x;
		hp = &(h[i]);
		for(j=0;j<xn;++j,++xp,--hp){
			if( i < j )
				break;
			if( i-j >= hn ){
				j = i-hn;
				xp = &(x[j]);
				hp = &(h[i-j]);
				continue;
			}
			(*yp) += (*xp) * (*hp);
			//printf("%le x %le (%d,%d)+",*xp,*hp,i,j);
		}
			//printf("\n");
	}
	return;
}
/*
#include "ringbuf.h"
int convo(double *xdata,int xn,double *hdata,int hn,double *ydata)
{
	int i,j,k;
	double sum;
	RINGBUF *rbuf;
	if( (rbuf=initringbuf(hn)) == NULL )
		return 0;

	for(i=0;i<xn;++i){
		chkidx(i);
		ldringbuf(xdata[i],rbuf);
		sum = 0.0;
		for(j=0;j<hn;++j)
			sum += inpringbuf(j,rbuf) * hdata[j];
		ydata[i] = sum;
	}

	for(j=0;j<hn;++j,++i){
		chkidx(i);
		shringbuf(rbuf);
		sum = 0.0;
		for(k=0;k<hn;++k)
			sum += inpringbuf(k,rbuf) * hdata[k];
		ydata[i] = sum;
	}

	closeringbuf(rbuf);
	return 1;
}
*/


void usage(void)
{
	fprintf(stderr,"%s\n",rcsid);
	fputs("CONVO.EXE  option:\n",stderr);
	fputs("\t/x: input x data file name\n",stderr);
	fputs("\t/h: input h data file name\n",stderr);
	fputs("\t/y: output y data file name\n",stderr);
}

void main(int ac,char *av[])
{
	DARRAY *xdata,*hdata,*ydata;
	int xn,hn,yn,i;
	char *dmy,*opt="x:h:y:";
	int flag;
	char *xfname,*hfname,*yfname;
	FILE *fp;

	allresetflag(&flag);

	if( ac > 1 ){
		while(1){
			switch( getopt(ac,av,opt) ){
				case 'x':
					xfname = optarg;
					setflag(&flag,XFILE);
					break;
				case 'h':
					hfname = optarg;
					setflag(&flag,HFILE);
					break;
				case 'y':
					yfname = optarg;
					setflag(&flag,YFILE);
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


/*	オプションなしのパラメータがあるとき、エラーとする場合はコメントを外す*/
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

	if( !MreadInit(256,1024) ){
		fputs("MreadInit() failed",stderr);
		return;
	}

	MREADDEFAULTCLM=1;
	if( (xdata=MultiReadCmd(xfname)) == NULL ){
		MreadErrorMes(stderr);
		fputs("MultiRead() failed",stderr);
		return;
	}else{
		MreadErrorMes(stderr);
	}

	if( (hdata=MultiReadCmd(hfname)) == NULL ){
		MreadErrorMes(stderr);
		fputs("MultiRead() failed",stderr);
		return;
	}else{
		MreadErrorMes(stderr);
	}


	if( (ydata=NewDary((xdata->n)+(hdata->n))) == NULL ){
		fputs("memory allocation error for output\n",stderr);
		return;
	}else{
		ClrDary(ydata);
	}


	convo(xdata->ary,xdata->n,hdata->ary,hdata->n,ydata->ary);


	if( (fp=fopen(yfname,"wt")) == NULL )
		fputs("Can't open file for write\n",stderr);
	if( !MultiWrite(fp,1,ydata,NULL) ){
		fputs("MreadWrite() failed\n",stderr);
		MreadErrorMes(stderr);
	}else{
		MreadErrorMes(stderr);
	}

	/*MreadClose();*/
	fputs("\nsuccess\n",stderr);
}
