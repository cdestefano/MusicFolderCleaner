# MusicFolderCleaner

## Shortcuts
[Summary](#summary)   
[Running](#running)   
[Help](#help)

## Summary

A small command line application for moving directories that don't contain a certain file extension.  I originally wrote this for music, but it could  be used for any file extension.  It will create the directory that you're trying to move to if it doesn't exist. Then it will start at the root and traverse all files under each directory.  If a file does contain the extension, it won't touch the directory. If not, it will move it to the specified directory. (Preserving file structure)

I wrote this for a case where I wanted to extract all my music by formats.  I wanted to have all my FLAC files in one folder while moving all my MP3 files.  This way all my FLAC files were in one location and I could point MusicBee's music folder to this location to only contain my FLAC files.

## Running
This is a .net core 3.1 application, while it should run on an OS, I have only tested using Windows.  You can run inside of Visual Studio, but everything can be done through the command line. 

In the root of the project, you can specify the solution or run without. 
```
dotnet build --configuration Release
```

This will build the Release folder, cd to the location
```
cd MusicFolderCleaner\bin\Release\netcoreapp3.1\
```

Run the application with
```
.\MusicFolderCleaner D:\Mixed-Music-Files D:\Not-Flac-Files .flac
```

## Help

Help is also available by typing either `-help` or `?`

```
.\MusicFolderCleaner -help
.\MusicFolderCleaner ?
```
