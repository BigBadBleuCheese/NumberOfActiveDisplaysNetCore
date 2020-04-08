@setlocal enableextensions
@echo off

echo [95mCleaning release directory...[0m
rd /s /q bin\Release
echo [95mCleaned release directory.[0m

timeout 3 /nobreak > nul

echo [95mBuilding...[0m
dotnet publish --configuration Release --verbosity normal /p:TreatWarningsAsErrors=true /warnaserror
if errorlevel 1 goto buildFailed
echo [95mBuilt.[0m

timeout 3 /nobreak > nul

echo [95mNumberOfActiveDisplays release built.[0m
goto :eof

:buildFailed
echo [31mBuilding failed![0m
goto :eof