@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  vstest-console.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir=%CD%\..\..
set msbuild_bin_path=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe
set vstestconsole_bin_path=c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe
set vstestconsole_proj_path=vstest-console.proj

@REM  Shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

@REM Change to the directory where the solution file resides
pushd %working_dir%

@REM run vstestconsole
"%msbuild_bin_path%" "%CD%\%vstestconsole_proj_path%" /p:WorkingDirectory="%CD%" /p:VSTestConsoleBinPath="%vstestconsole_bin_path%"
@if %errorlevel% NEQ 0 goto error
goto success

:error
@exit /b errorLevel

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

ENDLOCAL
echo on