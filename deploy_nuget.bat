@echo off

REM Build the version for framework 3.5
msbuild.exe build_nuget.proj /p:TargetFrameworkVersion=v3.5;Configuration=Release;PkgDir=lib\net35\;DefineConstants=NET35

REM Build the version for framework 4.0
msbuild.exe build_nuget.proj /p:TargetFrameworkVersion=v4.0;Configuration=Release;PkgDir=lib\net40\

REM Build the version for framework 4.5
msbuild.exe build_nuget.proj /p:TargetFrameworkVersion=v4.5;Configuration=Release;PkgDir=lib\net45\

nuget pack package_spec.nuspec