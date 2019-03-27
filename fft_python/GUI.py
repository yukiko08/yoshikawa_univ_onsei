# -*- coding: utf-8 -*-
import wave
import pyaudio
import time
import numpy as np
from scipy import signal
import matplotlib.pyplot as plt
import csv
import struct
import os,sys

import threading
import tkinter
from tkinter import *
from tkinter import ttk
from tkinter import filedialog
from tkinter import messagebox



#最大の二の乗数を返す
def search_pow2(n):
    bn = bin(n)
    ans = int(len(bn))-2
    return pow(2,ans)


#ファイル参照
def button1_clicked():
    global filename
    fTyp = [("","*")]
    iDir = os.path.abspath(os.path.dirname(__file__))
    filepath = filedialog.askopenfilename(filetypes = fTyp,initialdir = iDir)
    filename,ext = os.path.splitext(os.path.basename(filepath))
    file1.set(filepath)


# button2クリック時 fft
def button2_clicked():
    global fs
    global N
    global data1
    global data2
    global f1
    global f2
    global g

    if slice.get()=="":
        st = 2
    else:
        st = int(slice.get())


    wf = wave.open(file1.get(), "r" ) #inai.wav  バイオリン/output391_3009.wav byteの文字列で起きさ
    fs = wf.getframerate()                          # サンプリング周波数
    g = wf.readframes(wf.getnframes()) #getframes(オーディオフレーム：長さ) readframes最大 n 個のオーディオフレームを読み、bytes文字列で返す
    g = np.frombuffer(g, dtype= "int16")    # -1～1に正規化
    N = search_pow2(len(g))  # サンプル数 y=2^x  log2 y

    wf.close()

    n0 = 0                                        # サンプリング開始位置
    g.flags.writeable = True                    #書き込み可能

    q = np.zeros(N-len(g))
    g=np.append(g,q)
    G = np.fft.fft(g[n0:n0+N])                      # 高速フーリエ変換


    amp = [np.abs(c)*2/N for c in G]       # パワースペクトル 実部　
    phase = [np.arctan2(int(c.imag), int(c.real)) for c in G]   # 位相スペクトル
    flist = np.fft.fftfreq(N, d=1.0/fs)             # 周波数リスト???あってる？


    n1 = int(N/2)

    h1=np.where(flist.astype('int64')==200)[0][0]
    h2=np.where(flist.astype('int64')==1000)[0][0]
##500~2000hzの間のパワースペクトル周波数平均で分割
    w=np.average( flist[h1:h2],weights =amp[h1:h2])
    index = np.where(flist.astype('int64')==int(w))
    bunkatu = index[0][0]
### 元の自分で設定する分割のやつ
#    bunkatu=int(n1/st)
# ## ピークで分割
#     bunkatu= np.argmax(amp[h1:h2])
    print(bunkatu)




    F1=G.copy()
    F2=G.copy()

    F1=F1[0:n1] #ヒルベルト変換？
    F2=F2[0:n1]

    F1[n0:bunkatu]=0.0+0.0j
    F2[bunkatu+1:n1]=0.0+0.0j


    G1 = np.fft.ifft(F1)
    G2 = np.fft.ifft(F2)
    #
    O1 = G1.real
    O2 = G2.real

    data1 = O1.astype(np.int16).tobytes()
    data2 = O2.astype(np.int16).tobytes()


    #記録用
    len1=str(int(flist[bunkatu]))
    len2=str(int(flist[bunkatu-1]))
    print(len1)


    f1 =filename+len1+"_"+str(n0)+".wav"
    f2 =filename+len2+"_"+str(n1)+".wav"

    ##作成ファイルと周波数のログ
    # with open('output.csv','w',newline='') as csvfile:
    #     w = csv.writer(csvfile,delimiter=',') #, delimiter='\n'
    #     for i in range(N):
    #         data = [flist[i],F1[i].real,F2[i].real]
    #         w.writerow(data)


    # 波形サンプルを描画
    plt.subplot(411)#3行,1列,1番目
    plt.plot(range(n0, n0+N), g[n0:n0+N]) #range listつくる
    #plt.axis([n0, n0+N, -3.0, 3])
    plt.xlabel("Time")
    plt.ylabel("Amplitude")
    ## 振幅スペクトルを描画
    plt.subplot(412)
    plt.plot(flist[n0:n1], amp[n0:n1])
    #plt.axis([n0, (n0+N)/2,0,max(amp)])
    plt.xlabel("Frequency [Hz]")
    plt.ylabel("Amplitude spectrum")
    # 振幅スペクトルを描画
    plt.subplot(413)#3行,1列,1番目
    plt.plot(range(n0,len(G1)),G1[n0:len(G1)])
    #plt.plot(flist[n0:n1],F1.real)
    # plt.xlabel("Frequency [Hz]")
    # plt.ylabel("fft")
    plt.xlabel("Time")
    plt.ylabel("Amplitude")
    # 振幅スペクトルを描画
    plt.subplot(414)#3行,1列,1番目
    plt.plot(range(n0,len(G2)),G2[n0:len(G2)])
    #plt.plot(flist[n0:n1],F2.real)
    # plt.xlabel("Frequency [Hz]")
    # plt.ylabel("fft")
    plt.xlabel("Time")
    plt.ylabel("Amplitude")
    plt.show()




# button3クリック時 再生
def button3_clicked():
    CHUNK = 1024 # 1024個読み取り
    k=0
    py1 = pyaudio.PyAudio()
    stream1 = py1.open(format = pyaudio.paInt16,channels=1,rate=fs,output=True)
    stream1.start_stream()

    while k < len(data2):
        stream1.write(data2[k:k+CHUNK])          # ファイルから1024個 ストリームへの書き込み(バイナリ)
        k=k+CHUNK
    stream1.stop_stream()
    stream1.close()
    py1.terminate()


def button5_clicked():
    CHUNK = 1024 # 1024個読み取り
    k=0
    py2 = pyaudio.PyAudio()
    stream2 = py2.open(format = pyaudio.paInt16,channels=1,rate=fs,output=True)
    stream2.start_stream()

    while k < len(data1):
        stream2.write(data1[k:k+CHUNK]) #ファイルから1024個*2個 ストリームへの書き込み(バイナリ)
        k=k+CHUNK

    stream2.stop_stream()
    stream2.close()
    py2.terminate()


# button4クリック時 保存
def button4_clicked():
    messagebox.showinfo('FileReference Tool', u'作成ファイルは↓↓\n' + f1 + u"と" + f2)
    ##wav出力
    p = (1,2,fs,N,'NONE','not compressed') #16bit= 2byte

    fr1 = wave.Wave_write(f2)
    fr1.setparams(p)
    fr1.writeframes(data1) #byte or bumarray
    fr1.close()

    fr2 = wave.Wave_write(f1)
    fr2.setparams(p)
    fr2.writeframes(data2)
    fr2.close()

    root.quit()


def main():
    #GUI
    global root
    root=tkinter.Tk()
    root.title("周波数で分解")
    root.geometry("500x300")

    # root2 =tkinter.Tk()
    # root2.title(再生画面)

    frame1 = ttk.Frame(root,padding=20)
    frame1.grid()

    #button1 参照ボタン
    button1 = ttk.Button(root,text=u'参照', command=button1_clicked)
    button1.grid(row=0,column=3)

    #ラベル１　
    s = StringVar() #文字列を保持するもの
    s.set('ファイル>>')
    label1 = ttk.Label(frame1, textvariable=s)
    label1.grid(row=0, column=0)


    # 参照ファイルパス(file1)表示ラベルの作成
    global file1
    file1 = StringVar()
    file1_entry = ttk.Entry(frame1, textvariable=file1, width=50)
    file1_entry.grid(row=0, column=2)

    # Frame2の作成
    frame2 = ttk.Frame(root, padding=(0,5))
    frame2.grid(row=1)

    #ラベル2　
    l = StringVar() #文字列を保持するもの
    l.set('分割値>>')
    label2 = ttk.Label(frame2, textvariable=l)
    label2.grid(row=1, column=0)

    # 参照分割数(slice)ラベルの作成
    global slice
    slice = StringVar()
    slice_entry = ttk.Entry(frame2, textvariable=slice, width=20)
    slice_entry.grid(row=1, column=2)

    # Frame3の作成
    frame3 = ttk.Frame(root, padding=(0,5))
    frame3.grid(row=2)

    # Startボタンの作成
    button2 = ttk.Button(frame3, text='Start', command=button2_clicked)
    button2.pack(side=LEFT)

    #playボタンの作成
    button3 = ttk.Button(frame3, text='Play1', command=button3_clicked)
    button3.pack(side=LEFT)
    #playボタンの作成
    button5 = ttk.Button(frame3, text='Play2', command=button5_clicked)
    button5.pack(side=LEFT)

    # Cancelボタンの作成
    button4 = ttk.Button(frame3, text='Save', command=button4_clicked)
    button4.pack(side=LEFT)

    root.mainloop()

if __name__ == '__main__':
    main()
