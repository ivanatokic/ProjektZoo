-- Migracija: Evidencija sadržaja nastambi
-- (primarne/sekundarne životinje + predmeti)
-- Pokrenuti na postojećoj bazi koja nema ove promjene.

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Predmet')
BEGIN
    CREATE TABLE Predmet (
        ID_predmeta INT IDENTITY(1,1) PRIMARY KEY,
        ID_nastambe INT NOT NULL FOREIGN KEY REFERENCES Nastamba(ID_nastambe),
        tip NVARCHAR(50) NOT NULL,
        naziv NVARCHAR(100) NULL,
        opis NVARCHAR(MAX) NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Jedinka') AND name = 'primarna')
BEGIN
    ALTER TABLE dbo.Jedinka
    ADD primarna BIT NOT NULL CONSTRAINT DF_Jedinka_primarna DEFAULT(1);
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Skupina') AND name = 'primarna')
BEGIN
    ALTER TABLE dbo.Skupina
    ADD primarna BIT NOT NULL CONSTRAINT DF_Skupina_primarna DEFAULT(1);
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Jedinka') AND name = 'broj')
BEGIN
    ALTER TABLE dbo.Jedinka ADD broj NVARCHAR(50) NULL, poveznica NVARCHAR(500) NULL, nacin_nabavke NVARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Skupina') AND name = 'poveznica')
BEGIN
    ALTER TABLE dbo.Skupina ADD poveznica NVARCHAR(500) NULL, nacin_nabavke NVARCHAR(50) NULL;
END
GO
