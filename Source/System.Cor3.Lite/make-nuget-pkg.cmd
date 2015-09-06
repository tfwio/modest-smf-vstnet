@echo off
call:checkme
call:rethrow
call:dopkg
pause
goto:EOF
:: https://docs.nuget.org/create/Creating-and-Publishing-a-Package
:checkme
  @if NOT EXIST system.cor3.lite.nuspec (
    echo - no nuspec file
  ) else (
    echo - nuspec file found
  )
  goto:EOF

:rethrow
  @if NOT EXIST "nuget-out\" (
    echo - no nuget-out directory
    echo - create 'nuget-out' directory
    mkdir nuget-out
    echo - copy nuspec
    copy "System.Cor3.Lite.nuspec" "nuget-out/System.Cor3.Lite.nuspec"
  ) else (
    echo - nuget-out directory found
    rmdir /s/q nuget-out
    goto:rethrow
  )
  goto:EOF

:dopkg
  echo - generate nupkg
  nuget.exe pack System.Cor3.Lite.csproj -Build
  echo - move nupkg to 'nuget-out' dir
  move "System.Cor3.Lite.1.0.nupkg" "nuget-out/System.Cor3.Lite.1.0.nupkg"
  pushd nuget-out
  copy "System.Cor3.Lite.1.0.nupkg" "F:/home/dev/WIN/CS_ROOT/NuGet-Me/System.Cor3.Lite.1.0.nupkg"
  popd
  goto:EOF


::@powershell -NoProfile -ExecutionPolicy Bypass -Command "iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin
::PS:\>
