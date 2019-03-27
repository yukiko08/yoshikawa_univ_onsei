#ifndef DALLOC_H
#define DALLOC_H

#include <stdlib.h>
#include <stdio.h>

static int DallocIdxStart=0;

/*===========================================================================*/
double *dalloc(int n);
void    dfree(double *p);

void    ClrDdata(double *p,int n);

int     CpyDdataPrmPtr(double *p,int n,double *q);
double *CpyDdataNewPtr(double *p,int n);

int     WriteDdata(double *p,int n,char *fname);

/*===========================================================================*/
double *dalloc(int n)
{
	double *p;
	int i;
	if( (p=(double *)calloc( n, sizeof(double) )) != NULL ){
		for(i=0;i<n;++i)
			p[i] = 0.0;
	}
	return p;
}

void dfree(double *p)
{
	if( p != NULL ){
		free(p);
		p = NULL;
	}
	return;
}

void ClrDdata(double *p,int n)
{
	int i;
	for(i=0;i<n;++i)
		p[i] = 0.0;
	return;
}

int CpyDdataPrmPtr(double *p,int n,double *q)
{
	int i;
	for(i=0;i<n;++i)
		q[i] = p[i];
	return 1;
}

double *CpyDdataNewPtr(double *p,int n)
{
	double *q;
	if( (q=dalloc(n)) != NULL )
		CpyDdataPrmPtr(p,n,q);
	return q;
}

int WriteDdata(double *p,int n,char *fname)
{
	int ret=0,i;
	FILE *fp;
	if( (fp=fopen(fname,"wt")) != NULL ){
		fprintf(fp,"%d\n",n);
		for(i=0;i<n;++i)
			fprintf(fp,"%d,%le\n",i+DallocIdxStart,p[i]);
		ret = !ret;
		fclose(fp);
	}
	return ret;
}
#endif
