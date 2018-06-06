cd C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin

setlocal enabledelayedexpansion

set num=0
set file=%num%.wav
echo %file%

for /l %%i in (0,1,50) do (
set /a num=num+1
set file=!num!.wav
echo !file!
ffmpeg -i C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\input.wav -ss %%i -t 1 "%%i.wav"

)
pause