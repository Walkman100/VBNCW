#!/bin/sh

die() {
    echo
    echo "$* Failed!"
    exit 1
}
# From the awesomeness that is https://stackoverflow.com/a/25515370/2999220

# Console Hello World
mcs hello.cs || die "Compiling hello.cs"
mono hello.exe || die "Running hello.exe"
echo

# HTTPS connections
wget https://raw.github.com/mono/mono/master/mcs/class/Mono.Security/Test/tools/tlstest/tlstest.cs || die "Downloading tlstest.cs"
mcs tlstest.cs /r:System.dll /r:Mono.Security.dll || die "Compiling tlstest.cs"
mono tlstest.exe https://www.nuget.org || die "Running tlstest.exe"
echo

# Winforms Hello World
mcs helloWinForms.cs -pkg:dotnet || die "Compiling helloWinForms.cs"
mono helloWinForms.exe || die "Running helloWinForms.exe"
echo

# ASP.Net Hello World
#xsp4 --port 9000 || die "Starting ASP.Net server"
#curl http://localhost:9000/hello.aspx > /dev/null || die "Connecting to ASP.Net server"
#echo
## Disabled because xsp4 runs in the current terminal

# VB.Net Console Hello World
vbnc helloVB.vb || die "Compiling helloVB.vb"
mono helloVB.exe || die "Running helloVB.exe"
echo

# Gtk# Hello World
mcs helloGtk.cs -pkg:gtk-sharp-2.0 || die "Compiling helloGtk.cs"
mono helloGtk.exe || die "Running helloGtk.exe"
