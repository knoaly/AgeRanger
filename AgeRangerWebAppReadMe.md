AgeRanger things to note
1. Uses sqlite3, for the dll, please get sqlite-dll-win64-x64-3170000.zip from sqlite website. The dll must be placed in the following project folder
a. AgeRangerDO.Tests\bin\Debug
b. AgeRangerWebApi\bin
2. Test project was setup to use the relative path of the db but the web api somehow needs full path to work, so set that up at the web.config, where the current path should reside at AgeRangerDO's Data folder.
3. WebApp has custom error, if there's any error and you want a quick view of what it was, you have to go to the web.config to switch it off (line 17)
4. Finally, if the start up project isn't AgeRangerWebApp, please set that as the start up project.
thank you.

