CREATE TABLE [dbo].[Caracteristiques] (
    [Id]     INT          NOT NULL,
    [Nom]    VARCHAR (32) NOT NULL,
    [Def]    VARCHAR (32) NOT NULL,
    [Type]   VARCHAR (32) NOT NULL,
    [Valeur] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Jedis] (
    [Id]     INT           NOT NULL,
    [Nom]    VARCHAR (32)  NOT NULL,
    [IsSith] BIT           NOT NULL,
    [Image]  VARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Stades] (
    [Id]       INT           NOT NULL,
    [Nom]      VARCHAR (32)  NOT NULL,
    [NbPlaces] INT           NOT NULL,
    [Image]    VARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Matches] (
    [Id]        INT NOT NULL,
    [Jedi1]     INT NOT NULL,
    [Jedi2]     INT NOT NULL,
    [Stade]     INT NOT NULL,
    [Vainqueur] INT NOT NULL,
    [Phase]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Tournois] (
    [Id]  INT          NOT NULL,
    [Nom] VARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Users] (
    [Id]       INT           NOT NULL,
    [Login]    VARCHAR (32)  NOT NULL,
    [Password] VARCHAR (555) NOT NULL,
    [Nom]	   VARCHAR (32)  NOT NULL, 
    [Prenom]   VARCHAR (32)  NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[JediCarac]
(
	[IdJedi] INT NOT NULL,
	[IdCarac] INT NOT NULL,
	CONSTRAINT PK_JediCarac PRIMARY KEY CLUSTERED (IdJedi,IdCarac)
)

CREATE TABLE [dbo].[StadeCarac]
(
	[IdStade] INT NOT NULL,
	[IdCarac] INT NOT NULL,
	CONSTRAINT PK_StadeCarac PRIMARY KEY CLUSTERED (IdStade,IdCarac)
)

CREATE TABLE [dbo].[MatchTournoi]
(
	[IdMatch] INT NOT NULL,
	[IdTournoi] INT NOT NULL,
	[IdMatchTournoi] INT IDENTITY(1,1) NOT NULL,
	CONSTRAINT PK_MatchTournoi PRIMARY KEY CLUSTERED (IdMatchTournoi)
)
