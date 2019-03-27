#ifndef _MREAD_H_ 
#define _MREAD_H_ 


#include <stdio.h>
#include <stdlib.h>
#include <stdarg.h>
#include <string.h>
#include "darray.h"

static unsigned merror=0;
#define NOTRECOVERY	1
#define NOTOPEN		2
#define NOHEADER	4
#define NOMEMORY	8
#define OUTCOLUMN	16
#define OUTOFRANGE	32
#define LESSTHAN	64
#define OUTOFCOLUMN	128
#define NOMATCH		256
#define NOCONTENTS	512
#define NOFNAME			1024

static char *textbuffer;
static int textbuffersize;

static double *DataBuf;
static int DataBufIdx;
static int MreadFlag=0;         /* MreadInit()を実行したかの判定フラグ */
static int MreadEnableColumn=0; /* 読み込み可能なカラム数 */
static int MreadMaxColumn=0;    /* 読み込みデータの最大カラム数 */
static int MreadMinColumn=0;    /* 読み込みデータの最小カラム数 */

static int  MREADSTARTIDX=0;
static int  MREADDEFAULTCLM=0;
static char MREADDELIMITER=',';


int MreadInit(int clm,int txtbf);
void MreadClose(void);
int MreadErrorMes(FILE *fp);
char *ForwardToValue(char *_line);
void AnlyzLine(char *_line);
int MultiWrite(FILE *fp,int flag,DARRAY *p, ...);
int MultiWriteAry(FILE *fp,int flag,DARRAY **ptr);
DARRAY *MultiRead(char *fname,int col);
DARRAY *MultiReadCmd(char *str);



int MreadInit(int clm,int txtbf)
{
	if( !MreadFlag ){
		if( (DataBuf=(double *)calloc(clm,sizeof(double))) == NULL )
			return 0;
		if( (textbuffer=(char *)calloc(txtbf,sizeof(char))) == NULL ){
			free(DataBuf);
			return 0;
		}
		textbuffersize = txtbf;
		MreadEnableColumn = clm;
		MreadMaxColumn = 0;
		MreadMinColumn = 0;
	}else{
		MreadClose();
		MreadInit(clm,txtbf);
	}
	MreadFlag = 1;
	return 1;
}

void MreadClose(void)
{
	if( MreadFlag ){
		free(textbuffer);
		free(DataBuf);
		textbuffersize = 0;
		MreadEnableColumn = 0;
		MreadMaxColumn = 0;
		MreadMinColumn = 0;
		MreadFlag = 0;
	}
}


int MreadErrorMes(FILE *fp)
{
	if( (merror & NOTOPEN) == NOTOPEN )
		fputs("Can't open file.\n",fp);
	if( (merror & NOHEADER) == NOHEADER )
		fputs("Can't find header.\n",fp);
	if( (merror & NOMEMORY) == NOMEMORY )
		fputs("Memory allocation error.\n",fp);
	if( (merror & OUTCOLUMN) == OUTCOLUMN )
		fputs("In the selected column, can't find data.\n",fp);
	if( (merror & OUTOFRANGE) == OUTOFRANGE ){
		fputs("Header number is more than amount of data in the file.\n",fp);
		fputs("Rest data is cut.\n",fp);
	}
	if( (merror & LESSTHAN) == LESSTHAN )
		fputs("Header number is less than amount of data in the file.\n",fp);
	if( (merror & OUTOFCOLUMN) == OUTOFCOLUMN )
		fputs("In fuction MultiWrite(), parameter DARRAY more than column.\n",fp);
	if( (merror & NOMATCH) == NOMATCH )
		fputs("In fuction MultiWrite(),amount of data is no match.\n",fp);
	if( (merror & NOCONTENTS) == NOCONTENTS )
		fputs("In fuction MultiWrite(),no data.\n",fp);
	if( (merror & NOFNAME) == NOFNAME )
		fputs("In fuction MultiReadCmd(),file name not found.\n",fp);

	return( !( (merror & NOTRECOVERY) == NOTRECOVERY ) );
}
//------------------------------------------------------------------------------
char *ForwardToValue(char *_line)
{
	char *cp=NULL;
	int f;
	while( 1 ){
		f = 0;
		switch(*_line){
			case ' ':
			case ',':
			case '\t':
			case '\n':
				break;
			case '*':
			case ';':
			case '#':
			case '\0':
				cp = NULL;
				f =1;
				break;
			default:
				cp = _line;
				f = 1;
				break;
		}

		if( f )
			break;
		else
			++_line;
	}
	return cp;
}
//------------------------------------------------------------------------------
void AnlyzLine(char *_line)
{
	char *cp,*nxt,*q;
	cp = _line;
	DataBufIdx = 0;
	while(1){
		q = ForwardToValue(cp);
		if( q != NULL ){
			DataBuf[DataBufIdx] = strtod(q,&nxt);
			cp = nxt;
			++DataBufIdx;
		}else{
			break;
		}
	}
}

//------------------------------------------------------------------------------
int MultiWriteAry(FILE *fp,int flag,DARRAY **ptr)
{
	va_list ap;
	int idx=0,nmax,i,cnt;
	DARRAY *tmp;

	merror = 0;
	idx = 0;
	nmax = ptr[0]->n;
	while( ptr[idx] != NULL ){
		if( nmax < ptr[idx]->n ){
			nmax = ptr[idx]->n;
			merror |= NOMATCH;
		}
		++idx;
	}

	fprintf(fp,"%d\n",nmax);
	cnt = 0;
	while( 1 ){
		if( flag )
			fprintf(fp,"%d,",cnt+MREADSTARTIDX);

		for(i=0;i<idx-1;++i){
			if( cnt < ptr[i]->n)
				fprintf(fp,"%le,",ptr[i]->ary[cnt]);
			else
				fputc(',',fp);
		}
		fprintf(fp,"%le\n",ptr[i]->ary[cnt]);

		if( cnt < nmax-1 )
			++cnt;
		else
			break;
	}

	return 1;
}
//------------------------------------------------------------------------------
int MultiWrite(FILE *fp,int flag,DARRAY *p, ...)
{
	va_list ap;
	int idx=0,nmax,i,cnt,clm=0;
	DARRAY **ptr,*tmp;

	tmp = p ;
	merror = 0;
	va_start(ap,p);
	while( p != NULL ){
		p = va_arg(ap,DARRAY *);
		++clm;
	}
	va_end(ap);

	if( clm == 0 ){
		free(ptr);
		merror |= NOCONTENTS|NOTRECOVERY;
		return 0;
	}

	if( (ptr=(DARRAY **)calloc(clm,sizeof(DARRAY *))) == NULL ){
		merror |= NOMEMORY|NOTRECOVERY;
		return 0;
	}

	p = tmp;
	va_start(ap,p);
	while( p != NULL ){
		ptr[idx] = p;
		p = va_arg(ap,DARRAY *);
		++idx;
	}
	va_end(ap);
/*
	if( idx != clm ){
		fprintf(stderr,"idx:%d,clm:%d\n",idx,clm);
		exit(0);
	}
*/
	nmax = ptr[0]->n;
	for(i=1;i<idx;++i){
		if( nmax < ptr[i]->n ){
			nmax = ptr[i]->n;
			merror |= NOMATCH;
		}
	}

	fprintf(fp,"%d\n",nmax);
	cnt = 0;
	while( 1 ){
		if( flag )
			fprintf(fp,"%d%c",cnt+MREADSTARTIDX,MREADDELIMITER);

		for(i=0;i<idx-1;++i){
			if( cnt < ptr[i]->n)
				fprintf(fp,"%le%c",ptr[i]->ary[cnt],MREADDELIMITER);
			else
				fputc(MREADDELIMITER,fp);
		}
		fprintf(fp,"%le\n",ptr[i]->ary[cnt]);

		if( cnt < nmax-1 )
			++cnt;
		else
			break;
	}

	free(ptr);
	return 1;
}
//------------------------------------------------------------------------------
DARRAY *MultiRead(char *fname,int col)
{
	FILE *fp;
	int f,cnt,n;
	DARRAY *dary;

	merror = 0;
	if( (fp=fopen(fname,"rt")) == NULL ){
		merror |= NOTOPEN|NOTRECOVERY;
		return NULL;
	}

	/* Read header */
	f = 0;
	while( fgets(textbuffer,textbuffersize-1,fp) != NULL ){
		AnlyzLine(textbuffer);
		if( DataBufIdx > 0 ){
			if( DataBufIdx == 1 ){
				n = DataBuf[DataBufIdx-1];
				f = 1;
				break;
			}else{
				f = 0;
				merror |= NOHEADER|NOTRECOVERY;
				break;
			}
		}
	}
	
	if( !f ){
		merror |= NOHEADER|NOTRECOVERY;
		fclose(fp);
		return NULL;
	}else{
		if( (dary=NewDary(n)) == NULL ){
			fclose(fp);
			merror |= NOMEMORY|NOTRECOVERY;
			return NULL;
		}
	}


	/* Read Data main */
	f = 0;
	cnt = 0;
	MreadMaxColumn = 0;
	MreadMinColumn = MreadEnableColumn;
	while( fgets(textbuffer,textbuffersize-1,fp) != NULL ){
		AnlyzLine(textbuffer);
		if( DataBufIdx > 0 ){
			if( DataBufIdx <= col ){
				merror |= OUTCOLUMN;
				break;
			}else{
				if( cnt < n ){
					dary->ary[cnt] = DataBuf[col];
					++cnt;
				}else{
					merror |= OUTOFRANGE;
					break;
				}
				if( MreadMaxColumn < DataBufIdx )
					MreadMaxColumn = DataBufIdx;
				if( MreadMinColumn > DataBufIdx )
					MreadMinColumn = DataBufIdx;
			}
		}
	}

	if( cnt < n )
		merror |= LESSTHAN;

	dary->n = cnt;

	fclose(fp);
	return dary;

}

DARRAY *MultiReadCmd(char *str)
{
	char *idx;
	int clm=MREADDEFAULTCLM;

	merror = 0;
	if( (idx=strchr(str,',')) != NULL ){
		if( idx == str ){ /* str[0] == ',' */
			merror |= NOFNAME|NOTRECOVERY;
			return NULL;
		}

		*idx = '\0';
		++idx;
		if( *idx == '\0' ){
			/* merror |= NOSELECTCOLUMN; */
			clm = MREADDEFAULTCLM;
		}else{
			clm = atoi(idx);
		}
	}

	return( MultiRead(str,clm) );

}

#endif
