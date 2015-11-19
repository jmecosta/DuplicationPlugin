@echo on
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\vsvars32.bat"
call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools\VsDevCmd.bat"
msbuild BuildPlugin.msbuild /p:VisualStudioVersion=14.0 /p:EndVSQFile=ProjectDuplicationTracker.VSQ /p:AssemblyPatcherTaskOn=true /p:SkipCopy=No > buildlog2015.txt
