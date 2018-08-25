@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  sonarqube-scanner.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set sonarqube_scanner_folder=C:\root\bin\sonar-scanner-msbuild
set msbuild_folder=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin
set solution_folder=..\..
set solution_file=Atm.sln
set project_name=Atm
set project_version=1.0.0
set project_key=atm

@REM  Shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

@REM Change to the directory where the solution file resides
pushd "%solution_folder%"

"%sonarqube_scanner_folder%\SonarScanner.MSBuild.exe" begin /k:"%project_key%" /n:"%project_name%" /v:"%project_version%" /d:sonar.language=cs /d:sonar.scm.disabled=true /d:sonar.cs.opencover.reportsPaths=open-cover.xml
@if %errorlevel%  NEQ 0  goto :error

"%msbuild_folder%\msbuild.exe" %solution_file% /t:Rebuild
@if %errorlevel%  NEQ 0  goto :error

"%sonarqube_scanner_folder%\SonarScanner.MSBuild.exe" end
@if %errorlevel%  NEQ 0  goto :error

@REM  Restore the command prompt and exit
@goto :success

:error
echo An error has occured: %errorLevel%
echo start time: %start_time%
echo end time: %time%
goto :finish

:success
echo process successfully finished.
echo start time: %start_time%
echo end time: %Time%

:finish
popd
set prompt=%savedPrompt%

ENDLOCAL
echo on