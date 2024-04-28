﻿DROP TABLE IF EXISTS [WeaponTypes];
CREATE TABLE [WeaponTypes] (
  [ID] INTEGER  NOT NULL UNIQUE
, [WpnBaseID] INTEGER  NOT NULL
, [Name_EN] text NOT NULL
, [Name_CN] text NOT NULL
, CONSTRAINT [sqlite_master_PK_WeaponTypes] PRIMARY KEY ([ID])
, FOREIGN KEY ([WpnBaseID]) REFERENCES [WeaponTypes] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
