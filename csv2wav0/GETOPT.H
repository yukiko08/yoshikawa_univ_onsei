/*
	Copyright (c) 1986,1992 by Borland International Inc.
	All Rights Reserved.

	Arranged by Kunihiko Hayano
	Arrange for runnning under other OS.
	1996.9
*/
 
#include <errno.h>
#include <string.h>
#include <dos.h>
#include <stdio.h>

int     optind  = 1;    /* ���̈����̃C���f�b�N�X */
char   *optarg;         /* ���݂̃I�v�V�����̈����ւ̃|�C���^ */
int     opterr  = 1;    /* �G���[���b�Z�[�W���o�͂��邱�Ƃ����� */

static  char   *letP = NULL;    /* ���̃I�v�V���������̈ʒu���L�� */

/*
    �R�}���h���C���I�v�V������ UNIX System V �`���ŉ�͂��܂��B

    �W���I�ȃI�v�V�����̌`���́A���̂Ƃ���ł��B

    option ::= SW [optLetter]* [argLetter space* argument]

    ������
    - SW �́A'/' �� '-' �̂����ꂩ�ł��B����́AMSDOS �̃X�C�b�`������
      �ݒ�(INT 21H, �t�@���N�V���� 37H)�Ɉˑ����܂��B
    - optLetter �� argLetter �̑O�ɃX�y�[�X�͂���܂���B
    - optLetter �� argLetter �́A�p���ł����؂蕶���ł͂���܂���B
    - optLetter �́A��������� optionS �ɂ��镶���ƈ�v���Ȃ���΂Ȃ�܂���B
    - argLetter �́A��������� optionS �ɂ��镶���̌��� ':' ��
      �Ȃ���΂Ȃ�܂���B
    - argument �́A�z���C�g�X�y�[�X�ŋ�؂�ꂽ������ł��BSW ������
      �܂ނ��Ƃ��ł��܂��B
    - �啶���Ə������͋�ʂ���܂��B
 
    �R�}���h���C���ŁA���ꂼ��� SW �����Ŏn�܂镡���̃I�v�V������
    �w�肷�邱�Ƃ��ł��܂��B�������A���ׂẴI�v�V�����́A��I�v�V����
    �iSW �����Ŏn�܂�Ȃ������j�̑O�Ɏw�肵�Ȃ���΂Ȃ�܂���B
    optLetter �� argLetter �͌J��Ԃ��Ďg���邩������܂���B
    ���ꂪ�G���[�ł��邩�ǂ����͌Ăяo�����Ŕ��f���܂��B

    SW �������Ō�̈����Ƃ��ĒP�ƂŌ����ƃG���[�ɂȂ�܂��B
    �A������Q�� SW ����("--"�܂���"//")�́A���ꎩ�g��\�킵�A
    ���̌�ɑ����������I�v�V�����łȂ����Ƃ������܂��B

    ������ *optionS �́A�F������鐳�����I�v�V������������w�肵�܂��B
    argLetters �́A���� ':' �������܂��Bgetopt() �́A���������I�v
    �V���������R�[�h��Ԃ��A�R�}���h���C���ɃI�v�V�������c���Ă��Ȃ�
    �ꍇ�́AEOF ��Ԃ��܂��B�I�v�V������ argLetter �ł���΁A�O���[�o
    ���ϐ� optarg �ɂ́A����������ւ̃|�C���^���^�����Ă��܂��i�z
    ���C�g�X�y�[�X�͊܂܂�Ă��܂���j�B

    �O���[�o�� optind �́A������Ԃł͂P�ɂȂ��Ă��܂��B����́A���
    getopt �ŃI�v�V��������͂���� argv[] �̎��̈������w���Ă��܂��B
    �����A"--" �� "//" ���g��ꂽ�ꍇ�́Aoptind �͎��̈����Ɉړ�����
    getopt() �́AEOF ��Ԃ��܂��B

    SW �����ɑ����Ďw�肳��Ă��Ȃ��������������ꍇ�̂悤�ɁA�G���[��
    ��������ƁAgetopt() �� '?' ��Ԃ��A�ʏ�� perror ��ʂ��ăG���[
    ���b�Z�[�W��\�����܂��B�����A�O���[�o���ϐ� opterr �� getopt ��
    �Ăяo���O�� false (�[��)�ɐݒ肳��Ă���΁A�G���[���b�Z�[�W��
    �\������܂���B

    ���Ƃ��΁AMS-DOS �̃X�C�b�`������ '/' (MSDOS �̕W��) �ŁA
 
        *optionS == "A:F:PuU:wXZ:"

    �ł���΁A'P', 'u', 'w', 'X' �̓I�v�V���������ł���A'A', 'F', 'U',
    'Z' �́A�������Ƃ��Ȃ��܂��B���̃R�}���h���C���͐��������̂ƔF��
    ����܂��B
 
        aCommand  /uPFPi /X /A L someFile
 
    �����ŁA
    - 'u' �� 'P' �́A�P�Ƃ̃I�v�V���������Ƃ��ĕԂ���܂��B
    - 'F' �͈��������� "Pi" �ƂƂ��ɕԂ���܂��B
    - 'X' �́A�P�Ƃ̃I�v�V�����ł��B
    - 'A' �́A���������� "L" �ƂƂ��ɕԂ���܂��B
    - "someFile" �́A�I�v�V�����ł͂���܂���B�����ŁAgetopt �̏�����
      �I�����܂��B�Ăяo������ argv �|�C���^���g���Ďc��̈������������܂��B
*/

int	getopt(int argc, char *argv[], char *optionS)
{
	unsigned char ch,c;
	char *optP;

	if (argc > optind) {

		if (letP == NULL) {
			letP = argv[optind];
			c = *letP;
			if ( letP == NULL || ( c != '/' && c != '-' ) )
				goto gopEOF;

			++letP;
			if ( *letP == c ) {
				optind++;  goto gopEOF;
			}
		}

		if (0 == (ch = *(letP++))) {
			optind++;  goto gopEOF;
		}

		if (':' == ch  ||  (optP = strchr(optionS, ch)) == NULL)  
			goto gopError;

		if (':' == *(++optP)) {
			optind++;
			if (0 == *letP) {
				if (argc <= optind)  goto  gopError;
				letP = argv[optind++];
			}
			optarg = letP;
			letP = NULL;
		} else {
			if (0 == *letP) {
				optind++;
				letP = NULL;
			}
			optarg = NULL;
		}
		return ch;
	}
gopEOF:
	optarg = letP = NULL;  
	return EOF;
 
gopError:
	optarg = NULL;
	errno  = EINVAL;
	if (opterr)
		perror ("get command line option");
	return ('?');
}