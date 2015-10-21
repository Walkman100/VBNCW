#!/bin/sh

# Console Hello World
mcs hello.cs
mono hello.exe

# HTTPS connections
wget https://raw.github.com/mono/mono/master/mcs/class/Mono.Security/Test/tools/tlstest/tlstest.cs
mcs tlstest.cs /r:System.dll /r:Mono.Security.dll
mono tlstest.exe https://www.nuget.org

# Winforms Hello World
mcs helloWinForms.cs -pkg:dotnet
mono helloWinForms.exe

# ASP.Net Hello World
#xsp4 --port 9000
#curl http://localhost:9000/hello.aspx > /dev/null
## Disabled because xsp4 runs in the current terminal

# VB.Net Console Hello World
vbnc helloVB.vb
mono helloVB.exe

# Gtk# Hello World
mcs helloGtk.cs -pkg:gtk-sharp-2.0
mono helloGtk.exe
