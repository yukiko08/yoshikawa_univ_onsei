import pyaudio
import wave
import numpy as np

CHUNK = 1024
filename="../csv2wav0/inai2048_65536.wav"

wf = wave.open(filename, 'rb')

p = pyaudio.PyAudio()

stream = p.open(format = pyaudio.paInt16,
                channels=wf.getnchannels(),
                rate=wf.getframerate(),
                output=True)
print(p.get_format_from_width(wf.getsampwidth()))
"""
   format  : ストリームを読み書きするときのデータ型
   channels: ステレオかモノラルかの選択 1でモノラル 2でステレオ
   rate    : サンプル周波数
   output  : 出力モード

"""

# 1024個読み取り
c=wf.getnchannels()
print(c)
g = wf.readframes(wf.getnframes()) #getframes(オーディオフレーム：長さ) readframes最大 n 個のオーディオフレームを読み、bytes文字列で返す
print(type(g))
g = np.frombuffer(g, dtype= "int16")
print(type(g))
g=g.tobytes()
#bytes(np.array(g, dtype=np.uint16))
print(type(g))

    # 1024個読み取り
k=0
print(len(g))
while k < len(g):
    data = g[k:k+CHUNK] # ファイルから1024個*2個の
    stream.write(data)          # ストリームへの書き込み(バイナリ)
    k=k+CHUNK


stream.stop_stream()
stream.close()

p.terminate()
