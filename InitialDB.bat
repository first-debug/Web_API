@echo off
for /f "usebackq delims==LR" %%i in (`sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "USE master"`) do set sqlcmdResponse=%%i
if "%sqlcmdResponse%" == "Changed database context to 'master'." goto localDbInstall
echo 'MS SQL Server' doesn't install
exit

:localDbInstall
set sqlcmdResponse=
echo All cool

:TryAgain
set sqlcmdResponse=
echo Enter name of DB:
set /p dbname=
for /f "usebackq delims==LR" %%i in (`sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "CREATE DATABASE %dbname%"`) do set sqlcmdResponse=%%i
if "%sqlcmdResponse%" == "" goto dbCreated
echo %sqlcmdResponse%
goto TryAgain

:dbCreated
echo Db succesful created
if EXIST Web_API.sln (
	dotnet ef migrations add InitialCreate -p .\Web_API.Infrastructure\ -s .\Web_API\
	dotnet ef database update -p .\Web_API.Infrastructure\ -s .\Web_API\
) else (
	echo Is this the project?
	pause
)
