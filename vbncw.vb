Imports System.IO.File
Imports System.Xml

Public Class VBNCW
    '   TO-DO
    'scan current directory for .slns or .vbprojs, or skip if a valid file is provided as a flag
    'output dir
    'pre- and post-build tasks
    'run pre-build tasks
    'run VBNC, check & report errors
    'run post-build tasks
    'run output executable (if a flag is present?)

    Shared tmpString As String = ""
    
    Shared ResGenCommand As String = "resgen "
    Shared ResGenSourceFile As String = ""
    Shared ResGenOutputFile As String = ""
    
    Shared VBNCCommand As String = "vbnc "
    Shared VBNCFiles As String = ""
    Shared ReferenceFiles As String = ""
    Shared ImportNamespaces As String

    Public Shared Sub Main(args As String())
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

    Shared Sub ParseSLN(filePath As String)
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
                Else
                    Console.WriteLine("VBProj at """ & tmpString & """ does not exist!")
                End If
            End If
        Next
    End Sub

    Shared Sub ParseVBProj(filePath As string)
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
                            Do While reader.Read AndAlso reader.IsStartElement
                                Console.WriteLine("PropertyGroup: " & reader.Name)
                                tmpString = ""
                                Select Case reader.Name
                                    Case "Configuration"
                                        tmpString = reader("Condition")
                                    Case "Platform"
                                        tmpString = reader("Condition")
                                End Select
                                
                                If tmpString <> "" Then
                                    Console.WriteLine(" With Attrib Condition = " & tmpString)
                                End If
                                
                                reader.Read
                                If reader.Value <> "" Then
                                    Console.WriteLine(" With Value = " & reader.Value)
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
                                            Console.WriteLine("Found Reference: " & reader("Include"))
                                        Case "Import"
                                            Console.WriteLine("Found Import: " & reader("Include"))
                                        Case "Compile"
                                            Console.WriteLine("Found Compile: " & reader("Include"))
                                        Case "None"
                                            Console.WriteLine("Found ""None"": " & reader("Include"))
                                        Case Else
                                            Console.WriteLine("ItemGroup: """ & reader.Name & """")
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

    ' Console commands:
    '  Console.WriteLine("text")
    '  text = Console.ReadLine("text")
    '  character = Console.ReadKey("text")
End Class
