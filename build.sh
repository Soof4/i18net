#!/bin/bash

# Publish for Linux
dotnet publish -c Release -r linux-x64 --self-contained /p:PublishSingleFile=true /p:PublishTrimmed=true -o ./publish/linux

# Rename Linux executable
mv ./publish/linux/i18net ./publish/linux/i18net-linux

# Publish for Windows
dotnet publish -c Release -r win-x64 --self-contained /p:PublishSingleFile=true /p:PublishTrimmed=true -o ./publish/windows

# Rename Windows executable
mv ./publish/windows/i18net.exe ./publish/windows/i18net-win.exe

# Publish for macOS
dotnet publish -c Release -r osx-x64 --self-contained /p:PublishSingleFile=true /p:PublishTrimmed=true -o ./publish/macos

# Rename macOS executable
mv ./publish/macos/i18net ./publish/macos/i18net-mac

