# EntityFramework-MultiMigrate
EntityFramework migration tool and library that supports migrating multiple models

## Purpose

Out of the box, Entityframework supports multiple models being persisted to the same database. However...

**Multiple models cannot easily be integrated and treated by the consuming application as if it were a *single* model.**

This project provides the necessary tooling to support developing a component library with it's own set of migrations that 
are then *merged* with the migrations of a consuming class library.

## What's included?

* **MultiMigrate.exe** - a replacement to the [Migrate.exe tool](https://msdn.microsoft.com/en-gb/data/jj618307.aspx])
* **CcAcca.EntityFramework.MultiMigrate.dll** provides the class `MultiMigrateDbToLastestVersion` as a replacement to 
the database initializer `MigrateDatabaseToLatestVersion` 

## Background

Its usually desirable to develop highly *decoupled* models with an application layer that provides the necessary integration
to the GUI.

However, there are times when its an advantage to develop a single unified model but to internally identify a kernel
that is shared by multiple libraries. 

This kernel is developed as an base class that is then inherited and extended by the concreate class libraries.

In such scenarios, typically migrations for the base class library end up having to be repeatedily scalfolded by each 
extending library. For simple libraries this might be ok. 

This is not viable as complexity of the base library increases and custom migrations are required and/or migrations include 
data.
