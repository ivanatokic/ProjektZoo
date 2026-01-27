-- Migracija: Tura (potreban_vodic, status, vrijeme_zavrsetka) + Radnik (kompetencije)
-- Pokrenuti na postojećoj bazi koja nema ove promjene.

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Tura') AND name = 'vrijeme_zavrsetka')
BEGIN
    ALTER TABLE dbo.Tura
    ADD vrijeme_zavrsetka DATETIME2 NULL,
        potreban_vodic    BIT NOT NULL CONSTRAINT DF_Tura_potreban_vodic DEFAULT(1),
        status            NVARCHAR(50) NOT NULL CONSTRAINT DF_Tura_status DEFAULT(N'planirana');
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Radnik') AND name = 'kompetencije')
BEGIN
    ALTER TABLE dbo.Radnik ADD kompetencije NVARCHAR(255) NULL;
END
GO
