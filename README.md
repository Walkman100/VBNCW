# VBNC Wrapper
A wrapper for the Mono VB.Net Compiler, with some tests to make sure you have Mono and Mono-VB installed correctly.

Written for use on Linux, because `xbuild` (Mono version of `MSBuild`) doesn't like `VBProj`s :(

Run `make` to build, requires `vbnc` or other VB compiler (change in Makefile). Then use `mono vbncw <SLN_file.sln>` to parse the SLN and any linked VBproj's
