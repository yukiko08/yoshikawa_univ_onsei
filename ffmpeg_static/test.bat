setlocal enabledelayedexpansion

set num=0

for /L %%i in (1, 11, 200) do (
    set /a num=%%i%%10
    set file=!num!.wav	
    echo !file!
)

pause