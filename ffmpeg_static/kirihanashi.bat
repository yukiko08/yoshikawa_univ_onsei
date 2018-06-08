cd C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin

setlocal enabledelayedexpansion



for /l %%j in (0,1,10) do (
for /l %%i in (0,1,9) do (

ffmpeg -i C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\input_a.wav -ss %%j.%%i -t 0.1 "%%j_%%i.wav"

)
)
pause