Imports System.IO.File
Imports System.Xml

Module VBNCW
    '   TO-DO
    'scan current directory for .slns or .vbprojs, or skip if a valid file is provided as a flag
    'output dir
    'pre- and post-build tasks
    'run pre-build tasks
    'run VBNC, check & report errors
    'run post-build tasks
    'run output executable (if a flag is present?)

    Dim tmpString As String = ""
    
    Dim ResGenCommand As String = ""
    Dim ResGenSourceFile As String = ""
    Dim ResGenOutputFile As String = ""
    
    Dim VBNCCommand As String = ""
    Dim VBNCFiles As String = ""
    Dim ReferenceFiles As String = ""
    Dim ImportNamespaces As String = ""

    Sub Main(args As String())
        If args.Length = 0 Then
            Console.Write("Enter SLN location: ")
            tmpString = Console.Readline()
        Else
            tmpString = args(0)
        End If
        
        If Exists(tmpString)
            ParseSLN(tmpString)
        Else
            Console.WriteLine("File """ & tmpString & """ not found!")
        End If
    End Sub

    Sub ParseSLN(filePath As String)
        For Each line As String In ReadLines(filePath)
            If line.StartsWith("Project(", True, Nothing) Then
                tmpString = line.SubString(line.IndexOf(","))
                tmpString = tmpString.Substring(tmpString.IndexOf("""") +1)
                tmpString = tmpString.Remove(tmpString.IndexOf(""""))
                tmpString = tmpString.Replace("\", "/")
                ' tmpString now contains relative path, now get full path
                tmpString = filePath.Remove(filePath.LastIndexOf("/") +1) & tmpString
                
                If Exists(tmpString)
                    Console.WriteLine("Found VBProj at: " & tmpString)
                    ParseVBProj(tmpString)
                    'PreBuild
                    If BuildProject() <> 0 Then
                        Console.Write("Press any key to continue . . . ")
                        Console.ReadKey(True)
                        Console.WriteLine()
                    End If
                    'PostBuild
                Else
                    Console.WriteLine("VBProj at """ & tmpString & """ does not exist!")
                End If
            End If
        Next
    End Sub

    Sub ParseVBProj(filePath As string)
        Dim reader As XmlReader = XmlReader.Create(filePath)
        Try
            reader.Read()
        Catch ex As XmlException
            reader.Close
            Console.WriteLine("Reading config failed! The error was: " & ex.ToString)
            Exit Sub
        End Try
        
        If reader.IsStartElement() AndAlso reader.Name = "Project" Then
            While reader.Read
                If reader.IsStartElement Then
                    Select Case reader.Name
                        Case "Import"
                            Console.WriteLine("Import Project = " & reader("Project"))
                            If reader("Condition") <> "" Then
                                Console.WriteLine(" With Condition = " & reader("Condition"))
                            End If
                        Case "PropertyGroup"
                            Console.WriteLine("<Start PropertyGroup>")
                            If reader("Condition") <> "" Then
                                Console.WriteLine("With Condition = " & reader("Condition"))
                            End If
                            Do While reader.Read AndAlso reader.IsStartElement
                                Console.Write(" " & reader.Name)
                                tmpString = ""
                                Select Case reader.Name
                                    Case "Configuration"
                                        tmpString = reader("Condition")
                                    Case "Platform"
                                        tmpString = reader("Condition")
                                End Select
                                
                                reader.Read
                                If reader.Value <> "" Then
                                    Console.WriteLine(" = " & reader.Value)
                                End If
                                
                                If tmpString <> "" Then
                                    Console.WriteLine("  With Attrib Condition = " & tmpString)
                                End If
                                
                                reader.Read
                            Loop
                            Console.WriteLine("</End PropertyGroup>")
                        Case "ItemGroup"
                            Console.WriteLine("<Start ItemGroup>")
                            Do While reader.Read
                                If reader.IsStartElement Then
                                    Select Case reader.Name
                                        Case "Reference"
                                            Console.WriteLine(" Found Reference: " & reader("Include"))
                                        Case "Import"
                                            Console.WriteLine(" Found Import: " & reader("Include"))
                                        Case "Compile"
                                            Console.WriteLine(" Found Compile: " & reader("Include"))
                                        Case "None"
                                            Console.WriteLine(" Found ""None"": " & reader("Include"))
                                        Case Else
                                            Console.Write("  """ & reader.Name)
                                            reader.Read
                                            Console.WriteLine(""" = " & reader.Value)
                                    End Select
                                Else
                                    If reader.Name = "ItemGroup" Then
                                        Exit Do
                                    End If
                                End If
                            Loop
                            Console.WriteLine("</End ItemGroup>")
                    End Select
                End If
            End While
        End If
        
        reader.Close
    End Sub

    Function BuildProject as Integer
        ' Run ResGen
        Dim process as System.Diagnostics.Process
        process = System.Diagnostics.Process.Start("resgen", ResGenCommand & _
          "/compile """ & ResGenSourceFile & """," & ResGenOutputFile)
        process.WaitForExit
        If process.ExitCode = 0 Then
            ' Run VBNC
            process = System.Diagnostics.Process.Start("vbnc", VBNCCommand & _
            "/imports:" & ImportNamespaces & " /reference:" & ReferenceFiles & _
            " /resource:" & ResGenOutputFile & " " & VBNCFiles)
            process.WaitForExit
        End If
        Return process.ExitCode
    End Function

    ' Console commands:
    '  Console.WriteLine("text")
    '  text = Console.ReadLine("text")
    '  character = Console.ReadKey("text")
End Module
