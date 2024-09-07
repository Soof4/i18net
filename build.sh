#!/bin/bash

dotnet publish -c Release -r linux-x64 --self-contained /p:PublishSingleFile=true /p:PublishTrimmed=true -o ./publish/linux
dotnet publish -c Release -r win-x64 --self-contained /p:PublishSingleFile=true /p:PublishTrimmed=true -o ./publish/windows
dotnet publish -c Release -r osx-x64 --self-contained /p:PublishSingleFile=true /p:PublishTrimmed=true -o ./publish/macos

