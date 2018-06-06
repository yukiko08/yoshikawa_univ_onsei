@echo off

setlocal enabledelayedexpansion

set num=0

for /L %%i in (1, 1, 10) do (
    set /a num=num+1
    set file=!num!.wav	
    echo !file!
)

pause