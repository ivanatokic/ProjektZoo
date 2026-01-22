-- 1) Vrsta
INSERT INTO dbo.Vrsta (hr_naziv, lat_naziv)
VALUES 
(N'Lav', N'Panthera leo'),
(N'Vrana', N'Corvus corax'),
(N'Zlatna ribica', N'Carassius auratus'),
(N'Žaba', N'Rana temporaria'),
(N'Krokodil', N'Crocodylus niloticus');
GO

-- 2) Zooloski
INSERT INTO dbo.Zooloski (naziv, adresa, radno_vrijeme)
VALUES
(N'ZOO Zagreb', N'Maksimirska 1', 8);
GO

-- 3) Obrazovanje
INSERT INTO dbo.Obrazovanje (naziv, trajno)
VALUES
(N'Veterinar', 1),
(N'Odgajatelj životinja', 0);
GO

-- 4) Oblik
INSERT INTO dbo.Oblik (naziv, broj_stranica)
VALUES
('Pravokutnik', 4),
('Kvadrat', 4);
GO

-- 5) DimenzijaOblika
-- Oblik 1: Pravokutnik (2 dimenzije)
INSERT INTO dbo.DimenzijaOblika (ID_oblika, redni_broj, duljina)
VALUES
(1, 1, 100),
(1, 2, 50);
GO

-- Oblik 2: Kvadrat (1 dimenzija)
INSERT INTO dbo.DimenzijaOblika (ID_oblika, redni_broj, duljina)
VALUES
(2, 1, 50);
GO

-- 6) Nastamba (sad koristi ID_oblika, jer ti Nastamba nema geometrija kolonu)
INSERT INTO dbo.Nastamba (oznaka, opis, sjenčenje, ID_oblika)
VALUES
(N'A1', N'Prostor za lava s drvenim platformama i stijenama', N'djelomično', 1),
(N'B2', N'Prostor za vrane s granama i kavezom', N'puno', 2),
(N'C1', N'Akvarij za zlatne ribice s filtracijom i biljkama', N'puno', 2),
(N'D1', N'Bazen za žabe s malim jezerom i kamenjem', N'djelomično', 2),
(N'E1', N'Otok za krokodile s bazenom i kopnenim dijelom', N'puno', 1);
GO

-- 7) Radnik (pretpostavka: Obrazovanje ID 1..2 i Zooloski ID 1)
INSERT INTO dbo.Radnik (ime, prezime, kontakt_broj, ID_obrazovanja, tip_radnika, ID_zoo)
VALUES
(N'Marko', N'Marković', N'062888999', 1, N'Veterinar', 1),
(N'Marija', N'Marić', N'063444555', 2, N'Odgajatelj', 1),
(N'Ana', N'Anić', N'063111222', 2, N'Odgajatelj', 1),
(N'Petra', N'Petrović', N'063222333', 1, N'Veterinar', 1);
GO

-- 8) Jedinka (pretpostavka: Vrsta ID 1..5 i Nastamba ID 1..5)
INSERT INTO dbo.Jedinka (nadimak, ID_vrste, ID_nastambe, opis, datum_nabavke, trosak)
VALUES
(N'Leo', 1, 1, N'Mlad lav u nastambi A1', '2025-01-10', 5000),
(N'Corvus', 2, 2, N'Pametna vrana u nastambi B2', '2025-02-05', 1000),
(N'Goldie', 3, 3, N'Zlatna ribica u akvariju C1', '2025-03-01', 50),
(N'Froggy', 4, 4, N'Obična žaba u bazenu D1', '2025-04-01', 20),
(N'Croc', 5, 5, N'Krokodil u otoku E1', '2025-05-01', 10000);
GO

-- 9) Skupina
INSERT INTO dbo.Skupina (naziv, ID_vrste, ID_nastambe, prosjecan_broj, opis, datum_nabavke, trosak)
VALUES
(N'Čopor', 1, 1, 3, N'Tri lava u nastambi A1', '2025-01-10', 15000),
(N'Krd', 2, 2, 5, N'Pet vrana u nastambi B2', '2025-02-05', 2000),
(N'Jato', 3, 3, 50, N'Pedest zlatnih ribica u akvariju C1', '2025-03-01', 100),
(N'Vojka', 4, 4, 10, N'Petnaest žaba u bazenu D1', '2025-04-01', 50),
(N'Banda', 5, 5, 2, N'Dva krokodila u otoku E1', '2025-05-01', 25000);
GO

-- 10) Obaveza
INSERT INTO dbo.Obaveza (naziv, opis, status, datum, periodicnost, ID_radnika, ID_jedinke, ID_skupine)
VALUES
(N'Hranjenje lava', N'Davanje hrane lavu u nastambi A1', N'planirana', '2025-11-17', N'dnevno', 1, 1, NULL),
(N'Istraživanje skupine vrana', N'Praćenje ponašanja vrana u nastambi B2', N'planirana', '2025-11-17', N'tjedno', 2, NULL, 2),
(N'Čišćenje akvarija', N'Čišćenje i filtriranje vode za zlatne ribice', N'u tijeku', '2025-11-16', N'tjedno', 2, 3, NULL),
(N'Provjera bazena žaba', N'Provjera čistoće i vlažnosti bazena D1', N'planirana', '2025-11-18', N'dnevno', 3, 4, NULL),
(N'Kontrola krokodila', N'Provjera zdravlja i sigurnosti krokodila', N'obavljena', '2025-11-15', N'dnevno', 4, 5, NULL);
GO

-- 11) Raspored (napomena: moze puknuti zbog CHAR(1) + 'II', ali si rekla da ignoriram)
INSERT INTO dbo.Raspored (ID_radnika, datum, smjena, status)
VALUES
(1, '2025-11-16', 'I',  N'radi'),
(2, '2025-11-16', 'II', N'slobodan'),
(3, '2025-11-16', 'I',  N'radi'),
(4, '2025-11-16', 'II', N'radi');
GO

-- 12) Tura
INSERT INTO dbo.Tura (datum, broj_posjetitelja, ID_vodica, opis)
VALUES
('2025-11-16', 25, 1, N'Obilazak lavova i ptica'),
('2025-11-16', 15, 3, N'Obilazak vodozemaca i ribica'),
('2025-11-17', 30, 4, N'Obilazak krokodila i drugih gmazova');
GO

-- 13) Incident
INSERT INTO dbo.Incident (datum, opis, razina_zivotinje, razina_nastambe, trosak_popravka)
VALUES
('2025-11-15', N'Lav se ogrebao o drvenu platformu u nastambi A1', 2, 1, 150),
('2025-11-16', N'Vrana uništila mali ukras u nastambi B2', 1, 1, 20),
('2025-11-16', N'Akvarij za zlatne ribice C1 djelomično zamagljen zbog previše algi', 1, 1, 50),
('2025-11-17', N'Žaba se malo ozlijedila kod skakanja na kamenčiće u bazenu D1', 1, 1, 10),
('2025-11-17', N'Krokodil je razbio manji dio ograde na otoku E1 dok se sunčao', 2, 2, 300);
GO

-- 14) IncidentNastamba
INSERT INTO dbo.IncidentNastamba (ID_incidenta, ID_nastambe)
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);
GO

-- 15) IncidentZivotinja
INSERT INTO dbo.IncidentZivotinja (ID_incidenta, ID_vrste)
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);
GO

-- 16) Troskovi (mora biti ili Jedinka ili Skupina, ne oboje)
INSERT INTO dbo.Troskovi (ID_jedinke, ID_skupine, kategorija, iznos, datum, opis)
VALUES
(1, NULL, N'Hrana', 250.00, '2025-11-16 09:00:00', N'Hrana za lava'),
(NULL, 2, N'Odrzavanje', 100.00, '2025-11-16 10:00:00', N'Odrzavanje nastambe za vrane'),
(5, NULL, N'Lijekovi', 75.50, '2025-11-17 11:30:00', N'Lijekovi za krokodila');
GO
