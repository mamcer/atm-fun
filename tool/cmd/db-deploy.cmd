@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  db-deploy.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set msbuild_folder=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin
set webdeploy_folder=%ProgramW6432%\IIS\Microsoft Web Deploy V3
set solution_folder=..\..
set solution_name=Atm.sln
set default_build_type=Release
set dacpac_path=Src\Atm.Database\bin\Release\Atm.Database.dacpac
set data_source=[server-ip]
set database=atm
set user_id=[user-name]
set password=[user-password]

@REM  Shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

@REM Change to the directory where the solution file resides
pushd "%solution_folder%"

rem "%msbuild_folder%\MSBuild.exe" /m %solution_name% /t:Rebuild /p:Configuration=%default_build_type%
@if %errorlevel%  NEQ 0  goto :error

echo "%webdeploy_folder%\msdeploy.exe" -verb:sync -source:dbDacFx="%CD%\%dacpac_path%" -dest:dbDacFx="Data Source=%data_source%;Initial Catalog=%database%;User ID=%user_id%;Password=%password%"
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