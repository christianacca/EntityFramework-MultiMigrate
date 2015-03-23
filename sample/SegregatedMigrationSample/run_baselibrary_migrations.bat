@rem run_db_migrations.cmd
SET CurrentPath=%CD%
SET ConfigFile=%CurrentPath%\BaseLibrary.Web\Web.config
SET MigrateExe=..\..\lib\packages\EntityFramework.6.1.2\tools\migrate.exe

%MigrateExe% CcAcca.BaseLibraryMigrations.dll /startUpDirectory:%CurrentPath%\BaseLibrary.Web\bin\ /startUpConfigurationFile:"%ConfigFile%" /connectionStringName:BaseLibraryDbContext
PAUSE