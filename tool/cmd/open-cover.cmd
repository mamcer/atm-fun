@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  open-cover.cmd
@REM
@REM  author: m3mc4r@gmail.com
@REM  ----------------------------------------------------------------------------

pushd %~dp0
set start_time=%time%
set msbuild_bin="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"
set solution_name=Atm.sln
set solution_path="%CD%\..\..\\"
set opencover_bin="C:\root\bin\open-cover\tools\OpenCover.Console.exe"
set vstestconsole_bin="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"
set opencover_proj=open-cover.proj

@REM  shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

@REM rebuild solution
cd %solution_path%
rem %msbuild_bin% /m %solution_name% /t:Rebuild /p:Configuration=Debug
@if %errorlevel% NEQ 0 GOTO error

@REM run tests
%msbuild_bin% /p:WorkingDirectory=%solution_path% /p:OpenCoverBinPath=%opencover_bin% /p:MSTestBinPath=%vstestconsole_bin% %opencover_proj%
goto finish
@if %errorlevel% NEQ 0 goto error
goto success

:error
echo an error has occurred.
GOTO finish

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %Time%

:finish
popd
set prompt=%savedPrompt%

echo on