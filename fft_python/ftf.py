# -*- coding: utf-8 -*-
import wave
import numpy as np
import matplotlib.pyplot as plt
import csv
import struct
import sys


    # logのコード
    # with open('g.csv','w',newline='') as csvfile:
    #     w = csv.writer(csvfile, delimiter='\n')
    #     w.writerow(g)
    #
    # np.savetxt('F1.csv',F1.real,delimiter=',')



def search_pow2(n):
    bn = bin(n)
    ans = int(len(bn))-3
    return pow(2,ans)


def main():
    st = 25
    wf = wave.open("../csv2wav0/バイオリン/one.wav" , "r" ) #inai.wav  バイオリン/output391_3009.wav byteの文字列で起きさ
    fs = wf.getframerate()                          # サンプリング周波数
    g = wf.readframes(wf.getnframes()) #getframes(オーディオフレーム：長さ) readframes最大 n 個のオーディオフレームを読み、bytes文字列で返す

    g = np.frombuffer(g, dtype= "int16")    # -1～1に正規化  32767
    N = search_pow2(len(g))  # サンプル数 y=2^x  log2 y

    wf.close()

    n0 = 0                                        # サンプリング開始位置

    G = np.fft.fft(g[n0:n0+N])                      # 高速フーリエ変換

    amp = [np.abs(c)*2/N for c in G]       # 振幅スペクトル 実部　
    phase = [np.arctan2(int(c.imag), int(c.real)) for c in G]   # 位相スペクトル
    flist = np.fft.fftfreq(N, d=1.0/fs)             # 周波数リスト???あってる？

    n1 = int(N/2)

    F1=G.copy()
    F2=G.copy()

    F1=F1[0:n1] #ヒルベルト変換？
    F2=F2[0:n1]

    F1[n0:int(n1/st)]=0.0+0.0j
    F2[int(n1/st):n1]=0.0+0.0j


    np.savetxt('F1.csv',F1.real,delimiter=',')
    np.savetxt('F2.csv',F2.real,delimiter=',')

    G1 = np.fft.ifft(F1)
    G2 = np.fft.ifft(F2)
    #
    O1 = G1.real
    O2 = G2.real


    data1 = O1.astype(np.int16).tobytes()
    data2 = O2.astype(np.int16).tobytes()


    #記録用
    len1=str(int(n1/st)-1)
    len2=str(int(n1/st))
    print(flist[int(n1/st)])

    with open('output.csv','w',newline='') as csvfile:
        w = csv.writer(csvfile,delimiter=',') #, delimiter='\n'
        for i in range(n1):
            data = [flist[i],F1[i].real,F2[i].real]
            w.writerow(data)


    # 波形サンプルを描画
    plt.subplot(411)#3行,1列,1番目
    plt.plot(range(n0, n0+N), g[n0:n0+N]) #range listつくる
    #plt.axis([n0, n0+N, -3.0, 3])
    plt.xlabel("Time")
    plt.ylabel("Amplitude")
    ## 振幅スペクトルを描画
    plt.subplot(412)
    plt.plot(flist[n0:n1], amp[n0:n1])
    plt.xlabel("Frequency [Hz]")
    plt.ylabel("Amplitude spectrum")



    # 波形サンプルを描画
    plt.subplot(413)#3行,1列,1番目
    plt.plot(flist[n0:n1],F1[n0:n1].real)
    plt.xlabel("Frequency [Hz]")
    plt.ylabel("fft")

    # 波形サンプルを描画
    plt.subplot(414)#3行,1列,1番目
    plt.plot(flist[n0:n1],F2[n0:n1].real)
    plt.xlabel("Frequency [Hz]")
    plt.ylabel("fft")

    plt.show()
    ##wav出力
    fr1 = wave.Wave_write(len2+"_"+str(n1)+".wav")
    p = (1,2,fs,N,'NONE','not compressed') #16bit= 2byte
    fr1.setparams(p)
    fr1.writeframes(data1) #byte or bumarray
    fr1.close()

    fr2 = wave.Wave_write(len1+"_"+str(n0)+".wav")
    p = (1,2,fs,N,'NONE','not compressed') #16bit= 2byte
    fr2.setparams(p)
    fr2.writeframes(data2)
    fr2.close()


if __name__ == '__main__':
    main()
