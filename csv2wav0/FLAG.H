#ifndef _FLAG_H_
#define _FLAG_H_

void allsetflag(int *flag)
{
	*flag = ~((int)0);
}

void allresetflag(int *flag)
{
	*flag = 0;
}

void setflag(int *flag,int f)
{
	*flag |= f;
}

void resetflag(int *flag,int f)
{
	*flag &= ~f;
}

int isflag(int src,int dst)
{
	return( ( src & dst ) == dst );
}

#endif
