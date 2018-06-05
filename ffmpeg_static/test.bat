set num=1
for /l %%i in (0,1,3) do (
 echo %num%
 set \a num=
)