DROP TABLE IF EXISTS [Items];
CREATE TABLE [Items] (
  [ID] INTEGER  NOT NULL UNIQUE
, [ItemTypeID] INTEGER  NOT NULL
, [Name_EN] text NOT NULL
, [Name_CN] text NOT NULL
, CONSTRAINT [PK_Items] PRIMARY KEY ([ID])
, FOREIGN KEY ([ItemTypeID]) REFERENCES [ItemTypes] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
