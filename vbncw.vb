Imports System

Public Class HelloWorld
    '   TO-DO
    'scan current directory for .slns or .vbprojs, or skip if a valid file is provided as a flag
    'parse .SLN (if any) to get VBProj location
    'parse VBProj:
    '  output dir
    '  VB files to include
    '  ResX files to pre-process (low-priority)
    '  other flags to VBNC
    '  pre- and post-build tasks
    'run pre-build tasks
    'run VBNC, check & report errors
    'run post-build tasks
    'run output executable (if a flag is present?)

    Public Shared Sub Main()
        Console.WriteLine("Hello Mono World")
        'I don't even know where to start without an IDE with autocomplete :/
        System.Diagnostics.Process.Start("vbnc", "helloVB.vb")
        System.Threading.Thread.Sleep(500)
        System.Diagnostics.Process.Start("mono", "helloVB.exe")
        System.Threading.Thread.Sleep(100)
    End Sub
End Class
