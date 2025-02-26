REM Run from dev command line

@ECHO OFF

VERIFY ON

F:
cd "\Desktop Projects\OnlyT"
rd OnlyT\bin /q /s
rd OnlyTFirewallPorts\bin /q /s
rd Installer\Output /q /s
rd Installer\Staging /q /s

ECHO.
ECHO Publishing OnlyT
dotnet publish OnlyT\OnlyT.csproj -p:PublishProfile=FolderProfile -c:Release
IF %ERRORLEVEL% NEQ 0 goto ERROR

ECHO.
ECHO Publishing OnlyTFirewallPorts
dotnet publish OnlyTFirewallPorts\OnlyTFirewallPorts.csproj -p:PublishProfile=FolderProfile -c:Release
IF %ERRORLEVEL% NEQ 0 goto ERROR

md Installer\Staging

ECHO.
ECHO Copying OnlyTFirewallPorts items into staging area
xcopy OnlyTFirewallPorts\bin\Release\net8.0\publish\*.* Installer\Staging /q /s /y /d
IF %ERRORLEVEL% NEQ 0 goto ERROR

ECHO.
ECHO Copying OnlyT items into staging area
xcopy OnlyT\bin\Release\net8.0-windows\publish\*.* Installer\Staging /q /s /y /d
IF %ERRORLEVEL% NEQ 0 goto ERROR

ECHO.
ECHO Copying Sample task_schedule file into staging area
xcopy talk_schedule.xml Installer\Staging /q /y
IF %ERRORLEVEL% NEQ 0 goto ERROR

ECHO.
ECHO Removing unwanted x32 DLLs
del Installer\Staging\libmp3lame.32.dll
IF %ERRORLEVEL% NEQ 0 goto ERROR

ECHO.
ECHO Creating installer
"C:\Program Files (x86)\Inno Setup 6\iscc" Installer\onlytsetup.iss
IF %ERRORLEVEL% NEQ 0 goto ERROR

ECHO.
ECHO Creating portable zip
powershell Compress-Archive -Path Installer\Staging\* -DestinationPath Installer\Output\OnlyTPortable.zip
IF %ERRORLEVEL% NEQ 0 goto ERROR

goto SUCCESS

:ERROR
ECHO.
ECHO ******************
ECHO An ERROR occurred!
ECHO ******************

:SUCCESS

PAUSE