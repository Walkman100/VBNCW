# Deconstructing commands from MSBuild on Windows

## Compiling New:

```
Project "B:\Source\DirectoryImage\DirectoryImage.sln" on node 1 (default targets).
ValidateSolutionConfiguration:
  Building solution configuration "Release|Any CPU".
Project "B:\Source\DirectoryImage\DirectoryImage.sln" (1) is building "B:\Source\DirectoryImage\DirectoryImage.vbproj" (2) on node 1 (default targets).

PrepareForBuild:
  Creating directory "bin\Release\".
  Creating directory "obj\Release\".

CoreResGen:
  resgen.exe /useSourcePath /r:C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Core\v4.0_4.0.0.0__b77a5c561934e089\System.Core.dll /r:C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.dll /r:C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll /r:C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Windows.Forms\v4.0_4.0.0.0__b77a5c561934e089\System.Windows.Forms.dll /compile "My Project\Resources.resx",obj\Release\DirectoryImage.Resources.resources
  Processing resource file "My Project\Resources.resx" into "obj\Release\DirectoryImage.Resources.resources".

GenerateTargetFrameworkMonikerAttribute:
Skipping target "GenerateTargetFrameworkMonikerAttribute" because all output files are up-to-date with respect to the input files.

CoreCompile:
  C:\Windows\Microsoft.NET\Framework\v4.0.30319\Vbc.exe /noconfig /baseaddress:400000 /imports:Microsoft.VisualBasic,System,System.Diagnostics,System.IO,System.Windows.Forms /optioncompare:Binary /optionstrict:custom /nowarn:42016,41999,42017,42018,42019,42032,42036,42020,42021,42022 /optioninfer+ /nostdlib /platform:anycpu32bitpreferred /removeintchecks- /rootnamespace:DirectoryImage /sdkpath:C:\Windows\Microsoft.NET\Framework\v4.0.30319 /highentropyva+ /define:"CONFIG=\"Release\",TRACE=-1,_MyType=\"WindowsForms\",PLATFORM=\"AnyCPU\"" /reference:C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Core\v4.0_4.0.0.0__b77a5c561934e089\System.Core.dll,C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.dll,C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll,C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System.Windows.Forms\v4.0_4.0.0.0__b77a5c561934e089\System.Windows.Forms.dll /main:DirectoryImage.My.MyApplication /debug:Full /filealign:512 /optimize+ /out:obj\Release\DirectoryImage.exe /subsystemversion:6.00 /resource:obj\Release\DirectoryImage.Resources.resources /target:winexe /warnaserror- /win32icon:"My Project\Shell32(#326).ico" DirectoryImage.vb DirectoryImage.Designer.vb LinuxPathToWindowsPath.vb LinuxPathToWindowsDrive.vb "My Project\AssemblyInfo.vb" "My Project\Application.Designer.vb" "My Project\Resources.Designer.vb" "My Project\Settings.Designer.vb" "C:\Users\Walkman\AppData\Local\Temp\.NETFramework,Version=v4.5.AssemblyAttributes.vb"

_CopyAppConfigFile:
  Copying file from "App.config" to "bin\Release\DirectoryImage.exe.config".
CopyFilesToOutputDirectory:
  Copying file from "obj\Release\DirectoryImage.exe" to "bin\Release\DirectoryImage.exe".
  DirectoryImage -> B:\Source\DirectoryImage\bin\Release\DirectoryImage.exe
  Copying file from "obj\Release\DirectoryImage.pdb" to "bin\Release\DirectoryImage.pdb".
```

### ResGen Command:

```
resgen /useSourcePath /r:/usr/lib/mono/gac/System.Core/4.0.0.0__b77a5c561934e089/System.Core.dll /r:/usr/lib/mono/gac/System/4.0.0.0__b77a5c561934e089/System.dll /r:/usr/lib/mono/gac/System.Drawing/4.0.0.0__b77a5c561934e089/System.Drawing.dll /r:/usr/lib/mono/gac/System.Windows.Forms/4.0.0.0__b77a5c561934e089/System.Windows.Forms.dll /compile "My Project/Resources.resx",obj/Release/DirectoryImage.Resources.resources
  Processing resource file "My Project/Resources.resx" into "obj/Release/DirectoryImage.Resources.resources".
```

#### Deconstructed:

- `resgen`
- [ ] `/useSourcePath`
- [ ] References:

```
/r:/usr/lib/mono/gac/System.Core/4.0.0.0__b77a5c561934e089/System.Core.dll
/r:/usr/lib/mono/gac/System/4.0.0.0__b77a5c561934e089/System.dll
/r:/usr/lib/mono/gac/System.Drawing/4.0.0.0__b77a5c561934e089/System.Drawing.dll
/r:/usr/lib/mono/gac/System.Windows.Forms/4.0.0.0__b77a5c561934e089/System.Windows.Forms.dll
```
- [ ] `/compile "My Project/Resources.resx",obj/Release/DirectoryImage.Resources.resources`
  - `/compile "source_file",output_file`

### VBC Command:

```
vbnc /noconfig /baseaddress:400000 /imports:Microsoft.VisualBasic,System,System.Diagnostics,System.IO,System.Windows.Forms /optioncompare:Binary /optionstrict:custom /nowarn:42016,41999,42017,42018,42019,42032,42036,42020,42021,42022 /optioninfer+ /nostdlib /platform:anycpu32bitpreferred /removeintchecks- /rootnamespace:DirectoryImage /highentropyva+ /define:"CONFIG=\"Release\",TRACE=-1,_MyType=\"WindowsForms\",PLATFORM=\"AnyCPU\"" /reference:/usr/lib/mono/gac/System.Core/4.0.0.0__b77a5c561934e089/System.Core.dll,/usr/lib/mono/gac/System/4.0.0.0__b77a5c561934e089/System.dll,/usr/lib/mono/gac/System.Drawing/4.0.0.0__b77a5c561934e089/System.Drawing.dll,/usr/lib/mono/gac/System.Windows.Forms/4.0.0.0__b77a5c561934e089/System.Windows.Forms.dll /main:DirectoryImage.My.MyApplication /debug:Full /filealign:512 /optimize+ /out:obj/Release/DirectoryImage.exe /subsystemversion:6.00 /resource:obj/Release/DirectoryImage.Resources.resources /target:winexe /warnaserror- /win32icon:"My Project/Shell32(#326).ico" DirectoryImage.vb DirectoryImage.Designer.vb LinuxPathToWindowsPath.vb LinuxPathToWindowsDrive.vb "My Project/AssemblyInfo.vb" "My Project/Application.Designer.vb" "My Project/Resources.Designer.vb" "My Project/Settings.Designer.vb"
```

#### Deconstructed:

- `vbnc`
- [ ] `/noconfig`
- [ ] `/baseaddress:400000`
- [ ] `/imports:Microsoft.VisualBasic,System,System.Diagnostics,System.IO,System.Windows.Forms`
- [ ] `/optioncompare:Binary`
- [ ] `/optionstrict:custom`
- [ ] `/nowarn:42016,41999,42017,42018,42019,42032,42036,42020,42021,42022`
- [ ] `/optioninfer+`
- [ ] `/nostdlib`
- [ ] Platform type: `/platform:anycpu32bitpreferred`
- [ ] `/removeintchecks-`
- [ ] Root Namespace: `/rootnamespace:DirectoryImage`
- [ ] `/highentropyva+`
- [ ] Defined variables: `/define:"CONFIG=\"Release\",TRACE=-1,_MyType=\"WindowsForms\",PLATFORM=\"AnyCPU\""`
- [ ] References:

```
/reference:
/usr/lib/mono/gac/System.Core/4.0.0.0__b77a5c561934e089/System.Core.dll,
/usr/lib/mono/gac/System/4.0.0.0__b77a5c561934e089/System.dll,
/usr/lib/mono/gac/System.Drawing/4.0.0.0__b77a5c561934e089/System.Drawing.dll,
/usr/lib/mono/gac/System.Windows.Forms/4.0.0.0__b77a5c561934e089/System.Windows.Forms.dll
```
- [ ] `/main:DirectoryImage.My.MyApplication`
- [ ] `/debug:Full`
- [ ] `/filealign:512
- [ ] `/optimize+`
- [ ] Output File: `/out:obj/Release/DirectoryImage.exe`
- [ ] `/subsystemversion:6.00`
- [ ] Resources file: `/resource:obj/Release/DirectoryImage.Resources.resources`
- [ ] `/target:winexe /warnaserror-`
- [ ] Executable Icon: `/win32icon:"path"`
- [ ] Files to compile, i.e. all .vb files

## Existing Recompile:

```
Project "B:\Source\DirectoryImage\DirectoryImage.sln" on node 1 (default targets).
ValidateSolutionConfiguration:
  Building solution configuration "Release|Any CPU".
Project "B:\Source\DirectoryImage\DirectoryImage.sln" (1) is building "B:\Source\DirectoryImage\DirectoryImage.vbproj" (2) on node 1 (default targets).

CoreResGen:
  No resources are out of date with respect to their source files. Skipping resource generation.

GenerateTargetFrameworkMonikerAttribute:
Skipping target "GenerateTargetFrameworkMonikerAttribute" because all output files are up-to-date with respect to the input files.

CoreCompile:
Skipping target "CoreCompile" because all output files are up-to-date with respect to the input files.

_CopyAppConfigFile:
Skipping target "_CopyAppConfigFile" because all output files are up-to-date with respect to the input files.
CopyFilesToOutputDirectory:
  Copying file from "obj\Release\DirectoryImage.exe" to "bin\Release\DirectoryImage.exe".
  DirectoryImage -> B:\Source\DirectoryImage\bin\Release\DirectoryImage.exe
```

