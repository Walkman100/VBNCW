#!/bin/sh

# Console Hello World
mcs hello.cs || echo Failed!; exit 0
mono hello.exe || echo Failed!; exit 0
echo

# HTTPS connections
wget https://raw.github.com/mono/mono/master/mcs/class/Mono.Security/Test/tools/tlstest/tlstest.cs || echo Failed!; exit 0
mcs tlstest.cs /r:System.dll /r:Mono.Security.dll || echo Failed!; exit 0
mono tlstest.exe https://www.nuget.org || echo Failed!; exit 0
echo

# Winforms Hello World
mcs helloWinForms.cs -pkg:dotnet || echo Failed!; exit 0
mono helloWinForms.exe || echo Failed!; exit 0
echo

# ASP.Net Hello World
#xsp4 --port 9000 || echo Failed!; exit 0
#curl http://localhost:9000/hello.aspx > /dev/null || echo Failed!; exit 0
## Disabled because xsp4 runs in the current terminal
echo

# VB.Net Console Hello World
vbnc helloVB.vb || echo Failed!; exit 0
mono helloVB.exe || echo Failed!; exit 0
echo

# Gtk# Hello World
mcs helloGtk.cs -pkg:gtk-sharp-2.0 || echo Failed!; exit 0
mono helloGtk.exe || echo Failed!; exit 0
