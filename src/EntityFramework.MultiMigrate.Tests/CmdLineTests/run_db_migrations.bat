@rem Try to run multiple migrations using migrate.exe... this WILL fail
SET CurrentPath=%CD%\..\
SET ConfigFile=%CurrentPath%\bin\Debug\CcAcca.EntityFramework.MultiMigrate.Tests.dll.config
SET MigrateExe=..\..\packages\EntityFramework.6.1.2\tools\migrate.exe

%MigrateExe% DemoSharedDataMigrations.dll /startUpDirectory:%CurrentPath%\bin\Debug\ /startUpConfigurationFile:"%ConfigFile%" /connectionStringName:DemoDbContext-Compare
%MigrateExe% DemoDataMigrations.dll Configuration /startUpDirectory:%CurrentPath%\bin\Debug\ /startUpConfigurationFile:"%ConfigFile%" /connectionStringName:DemoDbContext-Compare
PAUSE