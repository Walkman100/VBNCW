#!/bin/sh

eecho() {
    echo
    echo "$* Failed!"
}

# Console Hello World
if mcs hello.cs; then
    mono hello.exe || eecho "Running hello.exe"
else
    eecho "Compiling hello.cs"
fi
echo

# HTTPS connections
if wget -nv https://raw.github.com/mono/mono/master/mcs/class/Mono.Security/Test/tools/tlstest/tlstest.cs; then
    if mcs tlstest.cs /r:System.dll /r:Mono.Security.dll; then
        mono tlstest.exe https://www.nuget.org || eecho "Running tlstest.exe"
    else
        eecho "Compiling tlstest.cs"
    fi
else
    eecho "Downloading tlstest.cs"
fi
echo

# Winforms Hello World
if mcs helloWinForms.cs -pkg:dotnet; then
    mono helloWinForms.exe || eecho "Running helloWinForms.exe"
else
    eecho "Compiling helloWinForms.cs"
fi
echo

# VB.Net Console Hello World
if vbnc -nologo -utf8output -quiet helloVB.vb; then
    mono helloVB.exe || eecho "Running helloVB.exe"
else
    eecho "Compiling helloVB.vb"
fi
echo

# ASP.Net Hello World
echo "Open http://localhost:9000/hello.aspx in your browser, once XSP4 has started, to complete this test"
echo "Starting..."
xsp4 --port 9000 || die "Starting ASP.Net server"

# Gtk# Hello World
if mcs helloGtk.cs -pkg:gtk-sharp-2.0; then
    echo "The Gtk# test program doesn't exit on it's own, once you close the window you need to ^C it to kill it"
    mono helloGtk.exe || eecho "Running helloGtk.exe"
else
    eecho "Compiling helloGtk.cs"
fi
