Imports System

Public Class VBNCW
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
        Me.UnsharedMain() 'Otherwise I have to put "Me." before every reference to local objects -_-
    End Sub

    Dim tmpString as String = ""

    Sub UnsharedMain()
        ParseSLN(Console.Readline("Enter SLN location:"))
        Console.Readline()
        End

        System.Diagnostics.Process.Start("vbnc", "helloVB.vb")
        System.Threading.Thread.Sleep(500)
        System.Diagnostics.Process.Start("mono", "helloVB.exe")
        System.Threading.Thread.Sleep(100)
    End Sub

    Sub ParseSLN(filePath as String)
        For Each line as String In System.IO.File.ReadLines(filePath)
            If line.StartsWith("Project(", True, Nothing) Then
                  ParseVBProj(line)
                tmpString = line.SubString(line.IndexOf(","))
                  ParseVBProj(tmpString)
                tmpString = tmpString.Substring(tmpString.IndexOf(""""))
                  ParseVBProj(tmpString)
                tmpString = tmpString.Remove(tmpString.IndexOf(""""))
                ParseVBProj(tmpString)
            End If
        Next
    End Sub

    Sub ParseVBProj(filePath as string)
        Console.WriteLine("Found VBProj at: " & filePath)
    End Sub

    Sub BuildProject()

    End Sub

    ' Console commands:
    '  Console.WriteLine("text")
    '  text = Console.ReadLine("text")
    '  character = Console.ReadKey("text")
End Class
