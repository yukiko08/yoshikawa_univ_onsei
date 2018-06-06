cd C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin

ffmpeg -f concat -safe 0 -i <(for f in C:\Users\S2\Documents\yoshikawa_univ_onsei\ffmpeg_static\bin\slice\*.wav; do echo "file '$PWD/$f'"; done) -codec copy output.wav