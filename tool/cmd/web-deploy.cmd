@echo off
SETLOCAL
@REM  ----------------------------------------------------------------------------
@REM  web-deploy.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set msbuild_folder="C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin"
set webdeploy_folder="C:\Program Files\IIS\Microsoft Web Deploy V3"
set solution_folder=%CD%\..\..
set solution_name=atm.sln
set remote_computer=[host-ip]
set website=atm
set user=[user-name]
set password=[user-password]
set param_file="%solution_folder\%Src\Common\Dev.Web.Deployment.SetParameters.xml"
set deploycmd_path=Src\Atm.Web\obj\Release\Package\Atm.Web.deploy.cmd

@REM  Shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

@REM Change to the directory where the solution file resides
pushd "%solution_folder%"

REM Generate Package
%msbuild_folder%\msbuild %solution_name% /p:Configuration=Release /p:DeployOnBuild=true /p:DeployTarget=Package /p:CreatePackageOnBuild=True
@if %errorlevel%  NEQ 0  goto :error

REM Recycle AppPool
rem %webdeploy_folder%\msdeploy -verb:sync -source:recycleApp -dest:recycleApp="%website%",recycleMode="RecycleAppPool",computerName=%remote_computer%
rem @if %errorlevel%  NEQ 0  goto :error

REM Deploy Package
"%deploycmd_path%" /Y /M:%remote_computer% /U:%user% /P:%password% -allowUntrusted -setParamFile:%param_file%
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