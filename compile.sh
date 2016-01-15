#!/bin/sh
vbnc "-out:vbncw.exe" -nologo -utf8output -quiet -rootnamespace:vbncw -target:exe  "vbncw.vb"

# flags:
# -nologo -utf8output -quiet -debug:full -optionstrict- -optionexplicit+ -optioncompare:binary -optioninfer- -rootnamespace:vbncw -target:exe