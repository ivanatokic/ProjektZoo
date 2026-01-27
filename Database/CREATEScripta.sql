
CREATE TABLE Vrsta (
    ID_vrste INT IDENTITY(1,1) PRIMARY KEY,
    hr_naziv NVARCHAR(100) NOT NULL,
    lat_naziv NVARCHAR(100) NOT NULL
);

CREATE TABLE Zooloski (
    ID_zoo INT IDENTITY(1,1) PRIMARY KEY,
    naziv NVARCHAR(100) NOT NULL,
    adresa NVARCHAR(100) NOT NULL,
    radno_vrijeme INT
);

CREATE TABLE dbo.Nastamba
(
    ID_nastambe INT IDENTITY(1,1) PRIMARY KEY,
    Oznaka     NVARCHAR(20) NOT NULL,
    opis       NVARCHAR(MAX),
    sijencenje NVARCHAR(50),
    Oblik      GEOMETRY NOT NULL,
    Koordinate GEOMETRY NULL,
    Povrsina_m2 AS (Oblik.STArea()) PERSISTED
);

CREATE TABLE Jedinka (
    ID_jedinke INT IDENTITY(1,1) PRIMARY KEY,
    nadimak NVARCHAR(50) NOT NULL,
    ID_vrste INT NOT NULL FOREIGN KEY REFERENCES Vrsta(ID_vrste),
    ID_nastambe INT NOT NULL FOREIGN KEY REFERENCES Nastamba(ID_nastambe),
    opis NVARCHAR(MAX),
    datum_nabavke DATE,
    trosak DECIMAL(10,2),
    tip_troska NVARCHAR(20) DEFAULT 'novčano',
    aktivna BIT DEFAULT 1
);

CREATE TABLE Skupina (
    ID_skupine INT IDENTITY(1,1) PRIMARY KEY,
    naziv NVARCHAR(100) NOT NULL,
    ID_vrste INT NOT NULL FOREIGN KEY REFERENCES Vrsta(ID_vrste),
    ID_nastambe INT NOT NULL FOREIGN KEY REFERENCES Nastamba(ID_nastambe),
    prosjecan_broj INT,
    opis NVARCHAR(MAX),
    datum_nabavke DATE,
    trosak DECIMAL(10,2),
    tip_troska NVARCHAR(20) DEFAULT 'novčano'
);

CREATE TABLE Predmet (
    ID_predmeta INT IDENTITY(1,1) PRIMARY KEY,
    ID_nastambe INT NOT NULL FOREIGN KEY REFERENCES Nastamba(ID_nastambe),
    tip NVARCHAR(50) NOT NULL,
    naziv NVARCHAR(100) NULL,
    opis NVARCHAR(MAX) NULL
);

CREATE TABLE Obrazovanje (
    ID_obrazovanja INT IDENTITY(1,1) PRIMARY KEY,
    naziv NVARCHAR(100) NOT NULL,
    trajno BIT DEFAULT 0
);

CREATE TABLE Radnik (
    ID_radnika INT IDENTITY(1,1) PRIMARY KEY,
    ime NVARCHAR(50) NOT NULL,
    prezime NVARCHAR(50) NOT NULL,
    kontakt_broj NVARCHAR(20) NOT NULL,
    ID_obrazovanja INT FOREIGN KEY REFERENCES Obrazovanje(ID_obrazovanja),
    tip_radnika NVARCHAR(50),
    ID_zoo INT NOT NULL FOREIGN KEY REFERENCES Zooloski(ID_zoo)
);

CREATE TABLE Obaveza (
    ID_obaveze INT IDENTITY(1,1) PRIMARY KEY,
    naziv NVARCHAR(100) NOT NULL,
    opis NVARCHAR(MAX),
    status NVARCHAR(20) CHECK (status IN ('planirana','obavljena','otkazana','u tijeku')),
    datum DATE NULL,
    periodicnost NVARCHAR(50),
    ID_radnika INT NULL FOREIGN KEY REFERENCES Radnik(ID_radnika),
    ID_jedinke INT NULL FOREIGN KEY REFERENCES Jedinka(ID_jedinke),
    ID_skupine INT NULL FOREIGN KEY REFERENCES Skupina(ID_skupine)
);

CREATE TABLE Raspored (
    ID_rasporeda INT IDENTITY(1,1) PRIMARY KEY,
    ID_radnika INT NOT NULL FOREIGN KEY REFERENCES Radnik(ID_radnika),
    datum DATE NOT NULL,
    smjena CHAR(2) CHECK (smjena IN ('I','II')),
    status NVARCHAR(20) CHECK (status IN ('radi','slobodan'))
);

CREATE TABLE Tura (
    ID_ture INT IDENTITY(1,1) PRIMARY KEY,
    datum DATE NOT NULL,
    broj_posjetitelja INT,
    ID_vodica INT FOREIGN KEY REFERENCES Radnik(ID_radnika),
    opis NVARCHAR(MAX)
);

CREATE TABLE Incident (
    ID_incidenta INT IDENTITY(1,1) PRIMARY KEY,
    datum DATE NOT NULL,
    opis NVARCHAR(MAX),
    razina_zivotinje INT CHECK (razina_zivotinje BETWEEN 1 AND 5),
    razina_nastambe INT CHECK (razina_nastambe BETWEEN 1 AND 5),
    trosak_popravka DECIMAL(10,2)
);

CREATE TABLE IncidentNastamba (
    ID_incidenta INT FOREIGN KEY REFERENCES Incident(ID_incidenta),
    ID_nastambe INT FOREIGN KEY REFERENCES Nastamba(ID_nastambe),
    PRIMARY KEY (ID_incidenta, ID_nastambe)
);

CREATE TABLE IncidentZivotinja (
    ID_incidenta INT FOREIGN KEY REFERENCES Incident(ID_incidenta),
    ID_vrste INT FOREIGN KEY REFERENCES Vrsta(ID_vrste)
);

CREATE TABLE Troskovi
(
    ID_troska INT IDENTITY(1,1) NOT NULL,
    
    ID_jedinke INT NULL,
    ID_skupine INT NULL,

    kategorija NVARCHAR(50) NOT NULL,
    iznos DECIMAL(10,2) NOT NULL,
    datum DATETIME2 NOT NULL,
    opis NVARCHAR(255) NULL,

    CONSTRAINT PK_Troskovi PRIMARY KEY (ID_troska),

    CONSTRAINT FK_Troskovi_Jedinka 
        FOREIGN KEY (ID_jedinke) REFERENCES Jedinka(ID_jedinke),

    CONSTRAINT FK_Troskovi_Skupina 
        FOREIGN KEY (ID_skupine) REFERENCES Skupina(ID_skupine),

    CONSTRAINT CK_Troskovi_Iznos_Positive 
        CHECK (iznos >= 0),

    CONSTRAINT CK_Troskovi_JedinkaIliSkupina 
        CHECK (
            (ID_jedinke IS NOT NULL AND ID_skupine IS NULL)
            OR
            (ID_jedinke IS NULL AND ID_skupine IS NOT NULL)
        )
);

ALTER TABLE dbo.Incident
ADD
    radovi_sanacije      NVARCHAR(MAX) NULL,
    ID_radnika_sanacije  INT NULL,
    datum_sanacije       DATE NULL,
    CONSTRAINT FK_Incident_RadnikSanacija
        FOREIGN KEY (ID_radnika_sanacije) REFERENCES dbo.Radnik(ID_radnika);


ALTER TABLE dbo.Skupina
ADD aktivna BIT NOT NULL CONSTRAINT DF_Skupina_aktivna DEFAULT(1);

ALTER TABLE dbo.Skupina
ADD primarna BIT NOT NULL CONSTRAINT DF_Skupina_primarna DEFAULT(1);

ALTER TABLE dbo.Skupina
ADD poveznica NVARCHAR(500) NULL,
    nacin_nabavke NVARCHAR(50) NULL;

ALTER TABLE dbo.Jedinka
ADD primarna BIT NOT NULL CONSTRAINT DF_Jedinka_primarna DEFAULT(1);

ALTER TABLE dbo.Jedinka
ADD broj NVARCHAR(50) NULL,
    poveznica NVARCHAR(500) NULL,
    nacin_nabavke NVARCHAR(50) NULL;

ALTER TABLE dbo.Jedinka ALTER COLUMN datum_nabavke DATETIME2 NULL;
ALTER TABLE dbo.Skupina ALTER COLUMN datum_nabavke DATETIME2 NULL;

ALTER TABLE dbo.Obaveza  ALTER COLUMN datum         DATETIME2 NULL;

ALTER TABLE dbo.Raspored ALTER COLUMN datum         DATETIME2 NOT NULL;
ALTER TABLE dbo.Tura     ALTER COLUMN datum         DATETIME2 NOT NULL;

ALTER TABLE dbo.Tura
ADD vrijeme_zavrsetka DATETIME2 NULL,
    potreban_vodic    BIT NOT NULL CONSTRAINT DF_Tura_potreban_vodic DEFAULT(1),
    status            NVARCHAR(50) NOT NULL CONSTRAINT DF_Tura_status DEFAULT(N'planirana');

ALTER TABLE dbo.Radnik
ADD kompetencije NVARCHAR(255) NULL;

ALTER TABLE dbo.Incident ALTER COLUMN datum         DATETIME2 NOT NULL;
ALTER TABLE dbo.Incident ALTER COLUMN datum_sanacije DATETIME2 NULL;

