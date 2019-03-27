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

int     optind  = 1;    /* 次の引数のインデックス */
char   *optarg;         /* 現在のオプションの引数へのポインタ */
int     opterr  = 1;    /* エラーメッセージを出力することを許可 */

static  char   *letP = NULL;    /* 次のオプション文字の位置を記憶 */

/*
    コマンドラインオプションを UNIX System V 形式で解析します。

    標準的なオプションの形式は、次のとおりです。

    option ::= SW [optLetter]* [argLetter space* argument]

    ここで
    - SW は、'/' か '-' のいずれかです。これは、MSDOS のスイッチ文字の
      設定(INT 21H, ファンクション 37H)に依存します。
    - optLetter と argLetter の前にスペースはありません。
    - optLetter と argLetter は、英字であり区切り文字ではありません。
    - optLetter は、もしあれば optionS にある文字と一致しなければなりません。
    - argLetter は、もしあれば optionS にある文字の後ろに ':' が
      なければなりません。
    - argument は、ホワイトスペースで区切られた文字列です。SW 文字を
      含むこともできます。
    - 大文字と小文字は区別されます。
 
    コマンドラインで、それぞれに SW 文字で始まる複数のオプションを
    指定することもできます。しかし、すべてのオプションは、非オプション
    （SW 文字で始まらない引数）の前に指定しなければなりません。
    optLetter や argLetter は繰り返して使われるかもしれません。
    それがエラーであるかどうかは呼び出し側で判断します。

    SW 文字が最後の引数として単独で現れるとエラーになります。
    連続する２つの SW 文字("--"または"//")は、それ自身を表わし、
    その後に続く引数がオプションでないことを示します。

    文字列 *optionS は、認識される正しいオプションや引数を指定します。
    argLetters は、後ろに ':' が続きます。getopt() は、見つかったオプ
    ション文字コードを返し、コマンドラインにオプションが残っていない
    場合は、EOF を返します。オプションが argLetter であれば、グローバ
    ル変数 optarg には、引数文字列へのポインタが与えられています（ホ
    ワイトスペースは含まれていません）。

    グローバル optind は、初期状態では１になっています。これは、常に
    getopt でオプションを解析される argv[] の次の引数を指しています。
    もし、"--" か "//" が使われた場合は、optind は次の引数に移動して
    getopt() は、EOF を返します。

    SW 文字に続いて指定されていない文字があった場合のように、エラーが
    発生すると、getopt() は '?' を返し、通常は perror を通してエラー
    メッセージを表示します。もし、グローバル変数 opterr が getopt を
    呼び出す前に false (ゼロ)に設定されていれば、エラーメッセージは
    表示されません。

    たとえば、MS-DOS のスイッチ文字が '/' (MSDOS の標準) で、
 
        *optionS == "A:F:PuU:wXZ:"

    であれば、'P', 'u', 'w', 'X' はオプション文字であり、'A', 'F', 'U',
    'Z' は、引数をともないます。次のコマンドラインは正しいものと認識
    されます。
 
        aCommand  /uPFPi /X /A L someFile
 
    ここで、
    - 'u' と 'P' は、単独のオプション文字として返されます。
    - 'F' は引数文字列 "Pi" とともに返されます。
    - 'X' は、単独のオプションです。
    - 'A' は、引数文字列 "L" とともに返されます。
    - "someFile" は、オプションではありません。ここで、getopt の処理を
      終了します。呼び出し側は argv ポインタを使って残りの引数を処理します。
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
