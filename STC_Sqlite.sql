/*******************************************************************************

   STC Database - Version 1.0

   Script: STC_Sqlite.sql

   Description: Creates the STC database.

   DB Server: Sqlite

   Author: Andrew Z

********************************************************************************/



/*******************************************************************************

   Drop Foreign Keys Constraints

********************************************************************************/











/*******************************************************************************

   Drop Tables

********************************************************************************/

DROP TABLE IF EXISTS [Product];



DROP TABLE IF EXISTS [Episode];



DROP TABLE IF EXISTS [Season];





/*******************************************************************************

   Create Tables

********************************************************************************/

CREATE TABLE [Product]

(

    [ProductId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,

    [ProductName] TEXT,

    [ProductDescription] TEXT,

	[ProductType] INTEGER,

	[ProductUrl] TEXT,

	[PurchaseUrl] TEXT,

	[ImageUrl] TEXT,

	[WasFunded] BOOLEAN,

	[DateCreated] DATETIME,

	[DateUpdated] DATETIME,

	[EpisodeId] INTEGER,

    FOREIGN KEY ([EpisodeId]) REFERENCES [Episode] ([EpisodeId]) 

		ON DELETE NO ACTION ON UPDATE NO ACTION

);



CREATE TABLE [Episode]

(

    [EpisodeId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,

    [EpisodeName] TEXT,

	[EpisodeNumber] INTEGER,

	[EpisoeDate] DATETIME,

	[DateCreated] DATETIME,

	[DateUpdated] DATETIME,

	[SeasonId] INTEGER,

	FOREIGN KEY ([SeasonId]) REFERENCES [Season] ([SeasonId]) 

		ON DELETE NO ACTION ON UPDATE NO ACTION

);



CREATE TABLE [Season]

(

    [SeasonId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,

    [SeasonNumber] INTEGER,

	[SeasonDate] DATETIME,

	[DateCreated] DATETIME,

	[DateUpdated] DATETIME

);







/*******************************************************************************

   Create Primary Key Unique Indexes

********************************************************************************/

CREATE UNIQUE INDEX [IPK_Product] ON [Product]([ProductId]);



CREATE UNIQUE INDEX [IPK_Episode] ON [Episode]([EpisodeId]);



CREATE UNIQUE INDEX [IPK_Season] ON [Season]([SeasonId]);





/*******************************************************************************

   Create Foreign Keys

********************************************************************************/

CREATE INDEX [IFK_ProductEpisodeId] ON [Product] ([EpisodeId]);



CREATE INDEX [IFK_EpisodeSeasonId] ON [Episode] ([SeasonId]);