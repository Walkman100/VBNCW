VBC = vbnc
VBCFLAGS = -nologo -quiet -utf8output -rootnamespace:vbncw -target:exe
VBFILES = vbncw.vb

vbncw: $(VBFILES)
	$(VBC) $(VBCFLAGS) -out:vbncw $(VBFILES)

clean:
	$(RM) vbncw
	$(RM) -r bin
# in case you had been using MonoDevelop

all: vbncw
