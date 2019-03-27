#ifndef _DARRAY_H_
#define _DARRAY_H_

#include <stdio.h>
#include <stdlib.h>
#include "dalloc.h"

static int DaryIdxStart=0;

typedef struct
{
	double *ary,**dary;
	int n,w,h;
}DARRAY;

/*===========================================================================*/
DARRAY *NewDary(int n);
void    FreeDary(DARRAY *p);
int     ClrDary(DARRAY *p);
int     ShiftDary(DARRAY *p,int sh);

int     CpyDaryPrmPtr(DARRAY *src,DARRAY *dst);
DARRAY *CpyDaryNewPtr(DARRAY *src);

int     AddDaryDirPtr(DARRAY *p,DARRAY *q);
int     AddDaryPrmPtr(DARRAY *p,DARRAY *q,DARRAY *r);
DARRAY *AddDaryNewPtr(DARRAY *p,DARRAY *q);

int     SubDaryDirPtr(DARRAY *p,DARRAY *q);
int     SubDaryPrmPtr(DARRAY *p,DARRAY *q,DARRAY *r);
DARRAY *SubDaryNewPtr(DARRAY *p,DARRAY *q);

int     MulDaryDirPtr(DARRAY *p,DARRAY *q);
int     MulDaryPrmPtr(DARRAY *p,DARRAY *q,DARRAY *r);
DARRAY *MulDaryNewPtr(DARRAY *p,DARRAY *q);

int     DivDaryDirPtr(DARRAY *p,DARRAY *q);
int     DivDaryPrmPtr(DARRAY *p,DARRAY *q,DARRAY *r);
DARRAY *DivDaryNewPtr(DARRAY *p,DARRAY *q);

int     AddDaryDirPtrD(DARRAY *p,double d);
int     AddDaryPrmPtrD(DARRAY *p,double d,DARRAY *q);
DARRAY *AddDaryNewPtrD(DARRAY *p,double d);

int     SubDaryDirPtrD(DARRAY *p,double d);
int     SubDaryPrmPtrD(DARRAY *p,double d,DARRAY *q);
DARRAY *SubDaryNewPtrD(DARRAY *p,double d);

int     MulDaryDirPtrD(DARRAY *p,double d);
int     MulDaryPrmPtrD(DARRAY *p,double d,DARRAY *q);
DARRAY *MulDaryNewPtrD(DARRAY *p,double d);

int     DivDaryDirPtrD(DARRAY *p,double d);
int     DivDaryPrmPtrD(DARRAY *p,double d,DARRAY *q);
DARRAY *DivDaryNewPtrD(DARRAY *p,double d);

double  SumDary(DARRAY *p);
int     InproDary(DARRAY *p,DARRAY *q,double *d);

DARRAY *ReadDaryNewPtr(char *fname);
int     ReadDaryPrmPtr(char *fname,DARRAY *p);

int     WriteDary(char *fname,DARRAY *p);

/*===========================================================================*/
DARRAY *NewDary(int n)
{
	DARRAY *p;
	if( (p=(DARRAY *)malloc( sizeof(DARRAY) )) != NULL ){
		if( (p->ary=dalloc(n)) == NULL ){
			free(p);
			p = NULL;
		}else{
			p->n = n;
			p->w = p->h = 0;
			p->dary = NULL;
		}
	}
	return p;
}

void FreeDary(DARRAY *p)
{
	if( p != NULL ){

		if( p->ary != NULL ){
			dfree(p->ary);
			p->ary = NULL;
		}

		if( p->dary != NULL ){
			free(p->dary);
			p->dary = NULL;
		}

		free(p);
	}
	return;
}

int ClrDary(DARRAY *p)
{
	int i;
	if( p == NULL )
		return 0;
	for(i=0;i<(p->n);++i)
		p->ary[i] = 0.0;
	return 1;
}

int ShiftDary(DARRAY *p,int sh)
{
	double *d;
	int i;

	if( (d=dalloc(p->n+sh)) == NULL )
		return 0;

	for(i=0;i<p->n;++i)
		d[i+sh] = p->ary[i];

	dfree(p->ary);
	p->ary = d;
	p->n  += sh;
	return 1;
}

/*===========================================================================*/
int CpyDaryPrmPtr(DARRAY *src,DARRAY *dst)
{
	int i;

	if( src->n != dst->n )
		return 0;
	for(i=0;i<(src->n);++i)
		dst->ary[i] = src->ary[i];
	return 1;
}

DARRAY *CpyDaryNewPtr(DARRAY *src)
{
	int i;
	DARRAY *dst;

	if( (dst=NewDary(src->n)) != NULL ){
		for(i=0;i<(src->n);++i)
			dst->ary[i] = src->ary[i];
	}
	return dst;
}

/*===========================================================================*/
int AddDaryDirPtr(DARRAY *p,DARRAY *q)
{
	int i;
	if( p->n != q->n )
		return 0;
	for(i=0;i<(p->n);++i)
		p->ary[i] += q->ary[i];
	return 1;
}

int AddDaryPrmPtr(DARRAY *p,DARRAY *q,DARRAY *r)
{
	int i;
	if( p->n != q->n || p->n != r->n)
		return 0;
	for(i=0;i<(p->n);++i)
		r->ary[i] = p->ary[i] + q->ary[i];
	return 1;
}

DARRAY *AddDaryNewPtr(DARRAY *p,DARRAY *q)
{
	int i;
	DARRAY *r;
	if( p->n != q->n )
		return NULL;
	if( (r=NewDary(p->n)) != NULL ){
		for(i=0;i<(p->n);++i)
			r->ary[i] = p->ary[i] + q->ary[i];
	}
	return r;
}

/*===========================================================================*/
int SubDaryDirPtr(DARRAY *p,DARRAY *q)
{
	int i;
	if( p->n != q->n )
		return 0;
	for(i=0;i<(p->n);++i)
		p->ary[i] -= q->ary[i];
	return 1;
}

int SubDaryPrmPtr(DARRAY *p,DARRAY *q,DARRAY *r)
{
	int i;
	if( p->n != q->n || p->n != r->n)
		return 0;
	for(i=0;i<(p->n);++i)
		r->ary[i] = p->ary[i] - q->ary[i];
	return 1;
}

DARRAY *SubDaryNewPtr(DARRAY *p,DARRAY *q)
{
	int i;
	DARRAY *r;
	if( p->n != q->n )
		return NULL;
	if( (r=NewDary(p->n)) != NULL ){
		for(i=0;i<(p->n);++i)
			r->ary[i] = p->ary[i] - q->ary[i];
	}
	return r;
}

/*===========================================================================*/
int MulDaryDirPtr(DARRAY *p,DARRAY *q)
{
	int i;
	if( p->n != q->n )
		return 0;
	for(i=0;i<(p->n);++i)
		p->ary[i] *= q->ary[i];
	return 1;
}

int MulDaryPrmPtr(DARRAY *p,DARRAY *q,DARRAY *r)
{
	int i;
	if( p->n != q->n || p->n != r->n)
		return 0;
	for(i=0;i<(p->n);++i)
		r->ary[i] = p->ary[i] * q->ary[i];
	return 1;
}

DARRAY *MulDaryNewPtr(DARRAY *p,DARRAY *q)
{
	int i;
	DARRAY *r;
	if( p->n != q->n )
		return NULL;
	if( (r=NewDary(p->n)) != NULL ){
		for(i=0;i<(p->n);++i)
			r->ary[i] = p->ary[i] * q->ary[i];
	}
	return r;
}

/*===========================================================================*/
int DivDaryDirPtr(DARRAY *p,DARRAY *q)
{
	int i;
	if( p->n != q->n )
		return 0;
	for(i=0;i<(p->n);++i)
		p->ary[i] /= q->ary[i];
	return 1;
}

int DivDaryPrmPtr(DARRAY *p,DARRAY *q,DARRAY *r)
{
	int i;
	if( p->n != q->n || p->n != r->n)
		return 0;
	for(i=0;i<(p->n);++i)
		r->ary[i] = p->ary[i] / q->ary[i];
	return 1;
}

DARRAY *DivDaryNewPtr(DARRAY *p,DARRAY *q)
{
	int i;
	DARRAY *r;
	if( p->n != q->n )
		return NULL;
	if( (r=NewDary(p->n)) != NULL ){
		for(i=0;i<(p->n);++i)
			r->ary[i] = p->ary[i] / q->ary[i];
	}
	return r;
}

/*===========================================================================*/
int AddDaryDirPtrD(DARRAY *p,double d)
{
	int i;
	for(i=0;i<(p->n);++i)
		p->ary[i] += d;
	return 1;
}

int AddDaryPrmPtrD(DARRAY *p,double d,DARRAY *q)
{
	int i;
	if( p->n != q->n )
		return 0;
	for(i=0;i<(p->n);++i)
		q->ary[i] = p->ary[i] + d;
	return 1;
}

DARRAY *AddDaryNewPtrD(DARRAY *p,double d)
{
	int i;
	DARRAY *q;
	if( (q=NewDary(p->n)) != NULL ){
		for(i=0;i<(p->n);++i)
			q->ary[i] = p->ary[i] + d;
	}
	return q;
}

/*===========================================================================*/
int SubDaryDirPtrD(DARRAY *p,double d)
{
	int i;
	for(i=0;i<(p->n);++i)
		p->ary[i] -= d;
	return 1;
}

int SubDaryPrmPtrD(DARRAY *p,double d,DARRAY *q)
{
	int i;
	if( p->n != q->n )
		return 0;
	for(i=0;i<(p->n);++i)
		q->ary[i] = p->ary[i] - d;
	return 1;
}

DARRAY *SubDaryNewPtrD(DARRAY *p,double d)
{
	int i;
	DARRAY *q;
	if( (q=NewDary(p->n)) != NULL ){
		for(i=0;i<(p->n);++i)
			q->ary[i] = p->ary[i] - d;
	}
	return q;
}

/*===========================================================================*/
int MulDaryDirPtrD(DARRAY *p,double d)
{
	int i;
	for(i=0;i<(p->n);++i)
		p->ary[i] *= d;
	return 1;
}

int MulDaryPrmPtrD(DARRAY *p,double d,DARRAY *q)
{
	int i;
	if( p->n != q->n )
		return 0;
	for(i=0;i<(p->n);++i)
		q->ary[i] = p->ary[i] * d;
	return 1;
}

DARRAY *MulDaryNewPtrD(DARRAY *p,double d)
{
	int i;
	DARRAY *q;
	if( (q=NewDary(p->n)) != NULL ){
		for(i=0;i<(p->n);++i)
			q->ary[i] = p->ary[i] * d;
	}
	return q;
}

/*===========================================================================*/
int DivDaryDirPtrD(DARRAY *p,double d)
{
	int i;
	for(i=0;i<(p->n);++i)
		p->ary[i] /= d;
	return 1;
}

int DivDaryPrmPtrD(DARRAY *p,double d,DARRAY *q)
{
	int i;
	if( p->n != q->n )
		return 0;
	for(i=0;i<(p->n);++i)
		q->ary[i] = p->ary[i] / d;
	return 1;
}

DARRAY *DivDaryNewPtrD(DARRAY *p,double d)
{
	int i;
	DARRAY *q;
	if( (q=NewDary(p->n)) != NULL ){
		for(i=0;i<(p->n);++i)
			q->ary[i] = p->ary[i] / d;
	}
	return q;
}

/*===========================================================================*/
double SumDary(DARRAY *p)
{
	int i;
	double s=0.0;
	for(i=0;i<(p->n);++i)
		s += p->ary[i];
	return s;
}

int InproDary(DARRAY *p,DARRAY *q,double *d)
{
	DARRAY *r;
	if( p->n != q->n )
		return 0;
	if( (r=MulDaryNewPtr(p,q)) == NULL )
		return 0;

	*d = SumDary(r);
	FreeDary(r);
	return 1;
}

/*===========================================================================*/
DARRAY *ReadDaryNewPtr(char *fname)
{
	DARRAY *p=NULL;
	FILE *fp;
	double dmy,tmp1;
	int n,i;

	if( (fp=fopen(fname,"rt")) == NULL )
		return NULL;

	fscanf(fp,"%d\n",&n);
	if( (p=NewDary(n)) != NULL ){
		for(i=0;i<n;++i){
			fscanf(fp,"%d,%lf\n",&dmy,&tmp1);
			p->ary[i] = tmp1;
		}
	}
	fclose(fp);
	return p;
}

int ReadDaryPrmPtr(char *fname,DARRAY *p)
{
	FILE *fp;
	double dmy,tmp1;
	int n,i;

	if( (fp=fopen(fname,"rt")) == NULL )
		return 0;

	fscanf(fp,"%d\n",&n);
	for(i=0;i<n;++i){
		fscanf(fp,"%d,%lf\n",&dmy,&tmp1);
		p->ary[i] = tmp1;
	}
	fclose(fp);
	return 1;
}


int WriteDary(char *fname,DARRAY *p)
{
	int ret=0,i;
	FILE *fp;
	if( (fp=fopen(fname,"wt")) != NULL ){
		fprintf(fp,"%d\n",p->n);
		for(i=0;i<(p->n);++i)
			fprintf(fp,"%d,%le\n",i+DaryIdxStart,p->ary[i]);
		ret = !ret;
		fclose(fp);
	}
	return ret;
}

#endif
