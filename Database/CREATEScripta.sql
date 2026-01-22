
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

CREATE TABLE Oblik (
    ID_oblika INT PRIMARY KEY IDENTITY(1,1),
    naziv VARCHAR(50) NOT NULL,         
    broj_stranica INT NULL              
);

CREATE TABLE DimenzijaOblika (
    ID_dimenzije INT PRIMARY KEY IDENTITY(1,1),
    ID_oblika INT NOT NULL FOREIGN KEY REFERENCES Oblik(ID_oblika),
    redni_broj INT NOT NULL,            
    duljina FLOAT NOT NULL              
);

CREATE TABLE Nastamba (
    ID_nastambe INT IDENTITY(1,1) PRIMARY KEY,
    oznaka NVARCHAR(20) NOT NULL,
    opis NVARCHAR(MAX),
    sjenčenje NVARCHAR(50),
    ID_oblika INT NULL FOREIGN KEY REFERENCES Oblik(ID_oblika)
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


ALTER TABLE DimenzijaOblika
ADD CONSTRAINT CHK_Dimenzija_PozitivnaDuljina
CHECK (duljina > 0);

ALTER TABLE Oblik
ADD CONSTRAINT CHK_Oblik_BrojStranica
CHECK (broj_stranica IS NULL OR broj_stranica >= 3);

ALTER TABLE DimenzijaOblika
ADD CONSTRAINT CHK_Dimenzija_RedniBroj
CHECK (redni_broj >= 1);
