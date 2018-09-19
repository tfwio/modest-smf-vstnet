#! cmd.exe /c
@echo off

SET PROJECT=Solution\\modest-smf-vstnet.sln
SET TARGET=/t:gen_snd_vstsmfui:Rebuild
SET CONF=Release
SET PLAT=Win32

REM set msbuild_path=C:\Program Files (x86)\msbuild\14.0\bin
set msbuild_path=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin
set PATH=%PATH%;%msbuild_path%
msbuild "%PROJECT%" "%TARGET%" "/p:Platform=%PLAT%;Configuration=%CONF%" /m
