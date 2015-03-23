@rem run_db_migrations.cmd
SET CurrentPath=%CD%
SET ConfigFile=%CurrentPath%\Library.Web\Web.config
SET MigrateExe=..\..\lib\packages\EntityFramework.6.1.2\tools\migrate.exe

%MigrateExe% CcAcca.BaseLibraryMigrations.dll /startUpDirectory:%CurrentPath%\Library.Web\bin\ /startUpConfigurationFile:"%ConfigFile%" /connectionStringName:LibraryDbContext
%MigrateExe% CcAcca.LibraryMigrations.dll /startUpDirectory:%CurrentPath%\Library.Web\bin\ /startUpConfigurationFile:"%ConfigFile%" /connectionStringName:LibraryDbContext
PAUSE