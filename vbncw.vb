Imports System.IO.File

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
        Console.Write("Enter SLN location: ")
        tmpString = Console.Readline()
        If Exists(tmpString)
            ParseSLN(tmpString)
        Else
            Console.Write("File """ & tmpString & """ not found!")
        End If
        Console.Readline()
        End

        System.Diagnostics.Process.Start("vbnc", "helloVB.vb")
        System.Threading.Thread.Sleep(500)
        System.Diagnostics.Process.Start("mono", "helloVB.exe")
        System.Threading.Thread.Sleep(100)
    End Sub

    Shared tmpString as String = ""

    Shared Sub ParseSLN(filePath as String)
        For Each line as String In ReadLines(filePath)
            If line.StartsWith("Project(", True, Nothing) Then
                tmpString = line.SubString(line.IndexOf(","))
                tmpString = tmpString.Substring(tmpString.IndexOf("""") +1)
                tmpString = tmpString.Remove(tmpString.IndexOf(""""))
                ParseVBProj(tmpString)
            End If
        Next
    End Sub

    Shared Sub ParseVBProj(filePath as string)
        Console.WriteLine("Found VBProj at: " & filePath)
    End Sub

    Shared Sub BuildProject()

    End Sub

    ' Console commands:
    '  Console.WriteLine("text")
    '  text = Console.ReadLine("text")
    '  character = Console.ReadKey("text")
End Class
