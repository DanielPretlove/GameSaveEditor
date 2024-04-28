DROP TABLE IF EXISTS [BladeSpecialsLv4];
CREATE TABLE [BladeSpecialsLv4] (
  [ID] INTEGER  NOT NULL UNIQUE
, [WpnBaseID] INTEGER  NOT NULL
, [Name_EN] text NOT NULL
, [Name_CN] text NOT NULL
, CONSTRAINT [sqlite_master_PK_Weapons] PRIMARY KEY ([ID])
, FOREIGN KEY ([WpnBaseID]) REFERENCES [Weapons] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
);