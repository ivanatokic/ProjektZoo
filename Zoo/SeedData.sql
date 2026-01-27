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

-- 4-6) Nastamba (geometrija oblik + opcionalne koordinate)
INSERT INTO dbo.Nastamba (oznaka, opis, sijencenje, Oblik, Koordinate)
VALUES
(N'A1', N'Prostor za lava s drvenim platformama i stijenama', N'djelomično',
 geometry::STGeomFromText('POLYGON((0 0, 20 0, 20 10, 0 10, 0 0))', 0), NULL),
(N'B2', N'Prostor za vrane s granama i kavezom', N'puno',
 geometry::STGeomFromText('POLYGON((0 0, 15 0, 15 8, 0 8, 0 0))', 0), NULL),
(N'C1', N'Akvarij za zlatne ribice s filtracijom i biljkama', N'puno',
 geometry::STGeomFromText('POLYGON((0 0, 10 0, 10 5, 0 5, 0 0))', 0), NULL),
(N'D1', N'Bazen za žabe s malim jezerom i kamenjem', N'djelomično',
 geometry::STGeomFromText('POLYGON((0 0, 12 0, 12 6, 0 6, 0 0))', 0), NULL),
(N'E1', N'Otok za krokodile s bazenom i kopnenim dijelom', N'puno',
 geometry::STGeomFromText('POLYGON((0 0, 25 0, 25 15, 0 15, 0 0))', 0), NULL);
GO

-- 7) Radnik (pretpostavka: Obrazovanje ID 1..2 i Zooloski ID 1)
INSERT INTO dbo.Radnik (ime, prezime, kontakt_broj, ID_obrazovanja, tip_radnika, ID_zoo, kompetencije)
VALUES
(N'Marko', N'Marković', N'062888999', 1, N'Veterinar', 1, N'Sisavci, lavovi'),
(N'Marija', N'Marić', N'063444555', 2, N'Odgajatelj', 1, N'Ptice'),
(N'Ana', N'Anić', N'063111222', 2, N'Odgajatelj', 1, N'Vodozemci, ribe'),
(N'Petra', N'Petrović', N'063222333', 1, N'Veterinar', 1, N'Gmazovi, krokodili');
GO

-- 8) Jedinka (pretpostavka: Vrsta ID 1..5 i Nastamba ID 1..5)
INSERT INTO dbo.Jedinka (nadimak, broj, ID_vrste, ID_nastambe, opis, datum_nabavke, nacin_nabavke, trosak)
VALUES
(N'Leo', N'1', 1, 1, N'Mlad lav u nastambi A1', '2025-01-10', N'kupljen', 5000),
(N'Corvus', N'2', 2, 2, N'Pametna vrana u nastambi B2', '2025-02-05', N'kupljen', 1000),
(N'Goldie', N'3', 3, 3, N'Zlatna ribica u akvariju C1', '2025-03-01', N'kupljen', 50),
(N'Froggy', N'4', 4, 4, N'Obična žaba u bazenu D1', '2025-04-01', N'kupljen', 20),
(N'Croc', N'5', 5, 5, N'Krokodil u otoku E1', '2025-05-01', N'kupljen', 10000);
GO

-- 9) Skupina
INSERT INTO dbo.Skupina (naziv, ID_vrste, ID_nastambe, prosjecan_broj, opis, datum_nabavke, nacin_nabavke, trosak)
VALUES
(N'Čopor', 1, 1, 3, N'Tri lava u nastambi A1', '2025-01-10', N'kupljen', 15000),
(N'Krd', 2, 2, 5, N'Pet vrana u nastambi B2', '2025-02-05', N'kupljen', 2000),
(N'Jato', 3, 3, 50, N'Pedest zlatnih ribica u akvariju C1', '2025-03-01', N'kupljen', 100),
(N'Vojka', 4, 4, 10, N'Petnaest žaba u bazenu D1', '2025-04-01', N'kupljen', 50),
(N'Banda', 5, 5, 2, N'Dva krokodila u otoku E1', '2025-05-01', N'kupljen', 25000);
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
INSERT INTO dbo.Tura (datum, vrijeme_zavrsetka, broj_posjetitelja, ID_vodica, opis, potreban_vodic, status)
VALUES
('2025-11-16 10:00:00', '2025-11-16 12:00:00', 25, 1, N'Obilazak lavova i ptica', 1, N'planirana'),
('2025-11-16 14:00:00', '2025-11-16 16:00:00', 15, 3, N'Obilazak vodozemaca i ribica', 1, N'dodijeljena'),
('2025-11-17 09:00:00', '2025-11-17 11:00:00', 30, 4, N'Obilazak krokodila i drugih gmazova', 1, N'planirana');
GO

-- 12b) Dogadaj (kalendar događaja – show lavova, specijalne ture, radionice)
INSERT INTO dbo.Dogadaj (naziv, opis, pocetak, kraj, tip, ID_ture)
VALUES
(N'Show lavova', N'Prezentacija hranjenja lavova s vodičem', '2025-11-18 11:00:00', '2025-11-18 11:30:00', N'show', 1),
(N'Ptice grabljivice', N'Poseban show vrana i drugih ptica', '2025-11-19 15:00:00', '2025-11-19 15:45:00', N'show', 2),
(N'Noćni obilazak krokodila', N'Specijalna večernja tura s vodičem', '2025-11-20 20:00:00', '2025-11-20 21:30:00', N'specijalna tura', 3),
(N'Dječja radionica: upoznaj žabe', N'Radionica za djecu u bazenu s žabama', '2025-11-21 10:00:00', '2025-11-21 11:00:00', N'radionica', NULL);
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
(1, NULL, N'Radni sati osoblja', 40.00, '2025-11-16 12:00:00', N'Hranjenje i čišćenje'),
(1, NULL, N'Veterinarski pregled', 60.00, '2025-11-17 09:00:00', N'Redovni pregled'),
(1, NULL, N'Drugo', 25.00, '2025-11-18 08:00:00', N'Ostalo za jedinku 1'),
(NULL, 2, N'Odrzavanje', 100.00, '2025-11-16 10:00:00', N'Odrzavanje nastambe za vrane'),
(2, NULL, N'Hrana', 80.00, '2025-11-16 11:00:00', N'Hrana za jedinku 2'),
(2, NULL, N'Veterinarski pregled', 45.00, '2025-11-17 14:00:00', N'Pregled'),
(3, NULL, N'Hrana', 120.00, '2025-11-18 10:00:00', N'Hrana za jedinku 3'),
(3, NULL, N'Radni sati osoblja', 30.00, '2025-11-17 16:00:00', N'Njega'),
(3, NULL, N'Drugo', 15.00, '2025-11-19 09:00:00', N'Ostalo'),
(4, NULL, N'Hrana', 20.00, '2025-11-16 13:00:00', N'Hrana za jedinku 4'),
(4, NULL, N'Veterinarski pregled', 35.00, '2025-11-18 11:00:00', N'Redovni pregled'),
(5, NULL, N'Lijekovi', 75.50, '2025-11-17 11:30:00', N'Lijekovi za krokodila'),
(5, NULL, N'Hrana', 300.00, '2025-11-16 09:30:00', N'Hrana za krokodila'),
(5, NULL, N'Radni sati osoblja', 55.00, '2025-11-18 07:00:00', N'Čišćenje i hranjenje');
GO

-- 17) Predmet (predmeti u nastambama: stijene, biljke, konstrukcije itd.)
INSERT INTO dbo.Predmet (ID_nastambe, tip, naziv, opis)
VALUES
(1, N'stijena', N'Drvene platforme i stijene', N'Za penjanje i odmor'),
(1, N'konstrukcija', N'Platforme', NULL),
(2, N'biljka', N'Grane i grmovi', NULL),
(3, N'biljka', N'Vodene biljke', N'Filtracija i kisik'),
(4, N'stijena', N'Kamenje uz jezerce', NULL),
(5, N'konstrukcija', N'Ograda i kopneni dio', NULL);
GO
