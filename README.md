
[github/naudio/NAudio]: https://github.com/naudio/NAudio
[github.com/obiwanjacobi/vst.net]: https://github.com/obiwanjacobi/vst.net
[github.com/tfwio/smfio]: https://github.com/tfwio/smfio

modest-smf-vstnet
=================
https://github.com/tfwio/modest-smf-vstnet

A 'modest' Windows.Forms app testing sending MIDI data to a single VST v2.4 instrument and effect.

smfio + vstnet + naudio

TARGETS: Win32, .NET Framework v4.0

NUGET PACKAGE REFS:

[github/naudio/NAudio]            v2.7.3  *[custom build; will be updated]*  
[github.com/obiwanjacobi/vst.net] Jacobi.Vst.Core and Jacobi.Vst.Interop  

EMBEDDED:

[github.com/tfwio/smfio]          Standard MIDI Format Parser

OBVIOUS QUIRKS
--------------

- Win32/x86 VST are supported.
- No editing, no piano-view.. we just load up a midi file and play it via the
  VST chain selected (or active).
- Audio Configuration Panel needs to be configured each time the app is launched.  
  *vst-plugins may be loaded with erroneous configuration settings by default.*
- MIDI->SetTempo is not yet processed
- Most VST Plugins seem to have no problem loading, however a 'plugin-loader'
  does not seem possible.  Each Plugin should be loaded manually via the UI
  or by editing the XML configuration file next to the program.  
  Attempts at writing a Plugin-Loader have thus far failed.
- NAudio implementation here (customized: v1.7.3) is pretty old.

COMPILING
---------------

requirements:

- python3 for PREBUILD step(s)
- DotNet Framework v4.0 (e.g. Visual Studio Community 2015/17)
- GIT

> Some of the dependency `*.csproj` projects *may* Target .Net Framework v3.5.

clone this repo and enter into that directory
```
git clone https://github.com/tfwio/modest-smf-vstnet
pushd modest-smf-vstnet
```
Then before executing the main build-scripts (explained below), you'll need to call the bootstrap script which does a few things...

### THE BOOTSTRAP SCRIPT

> In order for the script to work:
> - **GIT** should be on the System Environment PATH.
> - **Python3** (python.exe) should be directed to a working python3.
>
> *Same goes for the embedded smfio project which will be cloned by this script.*

The bootstrap script does a number of things...

1. Checks if Jacobi.Vst and NAudio nuget packages are installed.  
  **IF** `./Solution/packages` **NOT FOUND**, then
  `./.nuget/nuget.exe` will install the needed packages via
  `Source/gen.snd.vst/packages.config`.

2. Checks if `./Source/smfio` was cloned.  
   **IF** `./Source/smfio` **NOT FOUND**, then it will clone the
   repository and checkout the revision (SHA1) as stored in
   `./version-smfio`.  
   **IF** `./Source/smfio` **IS FOUND OR…** after its cloned (as
   mentioned above) it will continue to write the current SHA1
   (revision id) to `./version-smfio`.

3. **FINALLY, CREATE**: `./Source/gen.snd.vstsmfui/Properties/AssemblyInfo.cs`  
   AssemblyInfo.cs is created using information gathered from
   the GIT repository.

The basic task of the bootstrap script is to tell us what version
of SMFIO to use (git clone/checkout).

`asm-nfo.bat` calls the bootstrap script.
```bat
#! cmd.exe /c
@echo off
pushd %~dp0
"%LOCALAPPDATA%\Programs\Python\Python36\python.exe" "bootstrap"
popd
```
Then it would be safe to call on either of the build scripts: 
`Build-cli40-Win32-Release.cmd` or `Build-cli40-Win32-Debug.cmd`.

`Build-cli40-Win32-Debug.cmd`
```bat
#! cmd.exe /c
@echo off

SET PROJECT=Solution\\modest-smf-vstnet.sln
SET TARGET=/t:gen_snd_vstsmfui:Rebuild
SET CONF=Debug
SET PLAT=Win32

REM set msbuild_path=C:\Program Files (x86)\msbuild\14.0\bin
set msbuild_path=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin
set PATH=%PATH%;%msbuild_path%
msbuild "%PROJECT%" "%TARGET%" "/p:Platform=%PLAT%;Configuration=%CONF%" /m
```

You may or not wish to omit `:Rebuild` from the Target
in the command to speed up compilation.

There is also a `.vscode/tasks.json` build definition
which calls the DEBUG build script by default for use from
Visual Studio Code.

DEBUGGING
---------

I'd attempting a debugging session to learn the app fails to launch,
due to problems loading one of the the J.Vst.(Core|Interop).DLLs.

To debug from VS-IDE, "Run Without Debugger" and once its launched,
under the Debug-Menu, select "Attach" and select "modest" from the
list of prospects.
