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

    Shared tmpString as String = ""

    Public Shared Sub Main(args as String())
        If args.Length = 0 Then
            Console.Write("Enter SLN location: ")
            tmpString = Console.Readline()
        Else
            tmpString = args(0)
        End If
        
        If Exists(tmpString)
            ParseSLN(tmpString)
            End
        Else
            Console.WriteLine("File """ & tmpString & """ not found!")
            End
        End If
        
        System.Diagnostics.Process.Start("vbnc", "helloVB.vb")
        System.Threading.Thread.Sleep(500)
        System.Diagnostics.Process.Start("mono", "helloVB.exe")
        System.Threading.Thread.Sleep(100)
    End Sub

    Shared Sub ParseSLN(filePath as String)
        For Each line as String In ReadLines(filePath)
            If line.StartsWith("Project(", True, Nothing) Then
                tmpString = line.SubString(line.IndexOf(","))
                tmpString = tmpString.Substring(tmpString.IndexOf("""") +1)
                tmpString = tmpString.Remove(tmpString.IndexOf(""""))
                tmpString = tmpString.Replace("\", "/")
                ' tmpString now contains relative path, now get full path
                tmpString = filePath.Remove(filePath.LastIndexOf("/") +1) & tmpString
                
                ParseVBProj(tmpString)
            End If
        Next
    End Sub

    Shared Sub ParseVBProj(filePath as string)
        Console.WriteLine("File indicates VBProj at: " & filePath)
        If Exists(filePath)
            Console.WriteLine("File """ & filePath & """ exists!")
            BuildProject()
        Else
            Console.WriteLine("File """ & filePath & """ does not exist!")
        End If
    End Sub

    Shared Sub BuildProject()
        
    End Sub

    ' Console commands:
    '  Console.WriteLine("text")
    '  text = Console.ReadLine("text")
    '  character = Console.ReadKey("text")
End Class
