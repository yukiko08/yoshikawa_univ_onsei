cd C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin
set num=1
for /l %%i in (0,1,10) do (
set file=%num%.wav
ffmpeg -i C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\input.wav -ss %i -t 1 %file%
set /a num =num+1 
)