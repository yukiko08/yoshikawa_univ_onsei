# -*- coding: utf-8 -*-
import wave
import numpy as np
import matplotlib.pyplot as plt
import csv
import struct


def main():
    # wf = wave.open("../csv2wav0/バイオリン/output391_3009.wav" , "r" ) #inai.wav  byteの文字列で起きさ
    # fs = wf.getframerate()                          # サンプリング周波数
    # g = wf.readframes(wf.getnframes()) #getframes(オーディオフレーム：長さ) readframes最大 n 個のオーディオフレームを読み、bytes文字列で返す
    #
    # print(g[2:4])
    # a= g[2:4]
    # print(a)
    # data = np.frombuffer(a,dtype="int16")
    # print(data)
    # data = struct.pack("f",data)
    # print(data)


    name = "../csv2wav0/バイオリン/output391_3009.wav"
    wavefile = wave.open(name, "r")
    framerate = wavefile.getframerate()
    x = wavefile.readframes(wavefile.getnframes())
    x = np.frombuffer(x, dtype="int16")
    G = np.fft.fft(x)
    G1 = np.fft.ifft(G[0:int(len(G)/2)])
    G2 = G1.real
    G3 =[ c for c in G2]
    G4 = np.array(G3,dtype = 'int16')

    w = wave.Wave_write("output.wav")
    w.setnchannels(1)
    w.setsampwidth(2)
    w.setframerate(44100)
    w.writeframes(G4)
    w.close()



if __name__ == '__main__':
    main()
