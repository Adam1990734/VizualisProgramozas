USE IngatlanDb;

------------------------------------------------------------
-- Tábla: ingatlanok
------------------------------------------------------------

CREATE TABLE ingatlanok (
    ingatlan_ID INT NOT NULL IDENTITY(1000, 10),
    helyseg NVARCHAR(25) NOT NULL,
    kerulet INT NULL,
    fk_tipusID INT NOT NULL,
    terulet INT NOT NULL,
    szobaszam INT NOT NULL,
    ar FLOAT NOT NULL,
    garazs BIT NOT NULL,
    zoldovezet BIT NOT NULL,
    fk_ugynokID INT
);

------------------------------------------------------------
-- Adatok: ingatlanok
------------------------------------------------------------

INSERT INTO ingatlanok
(helyseg, kerulet, fk_tipusID, terulet, szobaszam, ar, garazs, zoldovezet, fk_ugynokID)
VALUES
('Budapest', 7, 1, 55, 2, 12.1, 1, 1, 1),
('Kecskemét', NULL, 1, 24, 1, 25, 0, 0, 1),
('Szeged', NULL, 1, 60, 2, 13.2, 0, 1, 1),
('Budapest', 3, 1, 100, 5, 22, 1, 1, 1),
('Budapest', 3, 1, 110, 6, 24.2, 1, 1, 1),
('Budapest', 7, 1, 140, 8, 30.8, 0, 0, 1),
('Kecskemét', NULL, 1, 80, 4, 17.6, 1, 1, 1),
('Szeged', NULL, 1, 90, 4, 19.8, 0, 0, 2),
('Kecskemét', NULL, 2, 75, 3, 14, 1, 1, 2),
('Budapest', 7, 1, 54, 2, 11.9, 0, 1, 2),
('Budapest', 7, 1, 34, 1, 7.5, 1, 0, 3),
('Szeged', NULL, 1, 29, 1, 6.4, 1, 1, 3),
('Budapest', 7, 1, 30, 1, 6.6, 0, 0, 2),
('Szeged', NULL, 1, 60, 2, 13.2, 1, 1, 1),
('Budapest', 7, 1, 70, 3, 15.4, 0, 1, 1),
('Kecskemét', NULL, 1, 40, 1, 8.8, 1, 0, 1),
('Budapest', 1, 1, 70, 3, 60, 0, 1, 1),
('Budapest', 7, 1, 77, 4, 16.9, 0, 0, 2),
('Budapest', 7, 1, 89, 4, 19.6, 1, 1, 2),
('Szeged', NULL, 2, 102, 5, 17, 0, 1, 2),
('Szeged', NULL, 1, 110, 6, 24.2, 0, 1, 2),
('Budapest', 7, 1, 15, 1, 3.3, 1, 1, 3),
('Kecskemét', NULL, 2, 300, 8, 48, 1, 1, 3),
('Budapest', 7, 1, 45, 1, 9.9, 0, 0, 1),
('Budapest', 7, 1, 48, 1, 10.6, 1, 1, 1),
('Kecskemét', NULL, 1, 79, 2, 17.4, 1, 1, 1),
('Budapest', 3, 1, 67, 3, 14.7, 0, 1, 2),
('Budapest', 3, 1, 66, 3, 14.5, 0, 1, 1),
('Budapest', 3, 1, 77, 2, 16.9, 1, 1, 2),
('Kecskemét', NULL, 1, 45, 1, 9.9, 1, 1, 3),
('Budapest', 7, 1, 49, 2, 10.8, 0, 1, 1),
('Szeged', NULL, 1, 58, 2, 12.8, 1, 0, 2),
('Budapest', 7, 1, 58, 2, 12.8, 1, 1, 2),
('Budapest', 7, 1, 59, 2, 13, 1, 1, 2),
('Budapest', 7, 1, 64, 3, 14.1, 1, 1, 2),
('Budapest', 7, 1, 75, 3, 16.5, 1, 1, 2),
('Kecskemét', NULL, 1, 78, 4, 17.2, 1, 0, 1),
('Szeged', NULL, 1, 84, 4, 18.5, 0, 1, 1),
('Szeged', NULL, 1, 81, 4, 17.8, 0, 1, 1),
('Budapest', 6, 1, 80, 4, 17.6, 1, 0, 1),
('Budapest', 6, 1, 90, 4, 19.8, 1, 1, 1),
('Budapest', 6, 1, 95, 4, 20.9, 1, 1, 1),
('Budapest', 5, 1, 94, 4, 20.7, 1, 1, 1),
('Kecskemét', NULL, 1, 97, 3, 21.3, 1, 1, 1),
('Kecskemét', NULL, 1, 105, 5, 23.1, 0, 1, 2),
('Budapest', 18, 1, 120, 4, 19, 1, 1, 2),
('Budapest', 18, 1, 125, 4, 18, 1, 1, 2),
('Szeged', NULL, 1, 140, 5, 30.8, 1, 0, 1),
('Budapest', 5, 1, 145, 5, 31.9, 1, 1, 1),
('Szeged', NULL, 1, 130, 5, 28.6, 0, 0, 2),
('Kecskemét', NULL, 1, 170, 6, 37.4, 1, 1, 1),
('Budapest', 22, 1, 100, 5, 22, 0, 1, 2),
('Budapest', 20, 2, 150, 9, 33, 1, 1, 1),
('Budapest', 19, 2, 130, 7, 28.6, 1, 1, 3),
('Budapest', 16, 3, 170, 5, 64, 1, 1, 2),
('Szeged', NULL, 2, 190, 6, 70, 1, 0, 2),
('Szeged', NULL, 1, 110, 4, 24.2, 1, 1, 3),
('Kecskemét', NULL, 3, 40, 1, 8.8, 1, 1, 2),
('Budapest', 17, 2, 140, 5, 30.8, 0, 1, 3),
('Budapest', 14, 1, 150, 4, 33, 1, 1, 2),
('Budapest', 13, 1, 49, 2, 10.8, 0, 1, 1),
('Szeged', NULL, 1, 49, 2, 10.8, 1, 1, 1),
('Budapest', 16, 1, 47, 1, 13, 1, 0, 1),
('Kecskemét', NULL, 3, 74, 3, 16.3, 1, 0, 1),
('Kecskemét', NULL, 2, 75, 3, 13, 1, 1, 1),
('Szeged', NULL, 1, 68, 3, 11, 0, 1, 1),
('Budapest', 3, 1, 50, 2, 11, 1, 1, 2),
('Szeged', NULL, 1, 50, 2, 8, 1, 1, 2),
('Budapest', 9, 1, 50, 2, 9, 1, 1, 3),
('Budapest', 8, 1, 60, 2, 8.5, 1, 1, 2),
('Budapest', 8, 1, 60, 2, 10, 1, 1, 1),
('Kecskemét', NULL, 1, 62, 2, 9, 0, 0, 2),
('Budapest', 7, 1, 98, 5, 21.6, 0, 1, 3),
('Szeged', NULL, 1, 68, 3, 15, 1, 1, 3),
('Szeged', NULL, 1, 68, 3, 15, 1, 1, 2),
('Budapest', 6, 1, 69, 3, 15.2, 1, 0, 2),
('Budapest', 5, 1, 100, 5, 22, 1, 1, 2),
('Budapest', 4, 1, 100, 5, 22, 1, 1, 1),
('Kecskemét', NULL, 1, 100, 4, 22, 1, 1, 1),
('Szeged', NULL, 2, 170, 3, 17, 0, 0, 1),
('Tatabánya', NULL, 2, 190, 4, 19, 1, 1, 2),
('Tatabánya', NULL, 2, 110, 3, 11, 1, 1, 3),
('Kecskemét', NULL, 2, 40, 2, 4, 0, 1, 2),
('Szeged', NULL, 2, 140, 3, 14, 1, 1, 1),
('Tatabánya', NULL, 2, 150, 3, 15, 1, 1, 3),
('Kecskemét', NULL, 3, 49, 2, 4.9, 1, 1, 2),
('Tatabánya', NULL, 2, 49, 2, 4.9, 0, 1, 1),
('Tatabánya', NULL, 3, 47, 1, 4.7, 1, 0, 2),
('Tatabánya', NULL, 2, 74, 3, 7.4, 1, 0, 3),
('Tatabánya', NULL, 2, 170, 4, 11.9, 1, 1, 3),
('Tatabánya', NULL, 2, 190, 4, 13.3, 1, 1, 2),
('Tatabánya', NULL, 2, 110, 4, 7.7, 1, 1, 1),
('Tatabánya', NULL, 1, 40, 3, 2.8, 1, 1, 2),
('Tatabánya', NULL, 2, 140, 3, 14, 0, 1, 3),
('Tatabánya', NULL, 2, 150, 4, 15, 1, 1, 2),
('Dorog', NULL, 3, 90, 3, 5.2, 1, 0, 1),
('Dorog', NULL, 2, 100, 4, 5.2, 1, 1, 2),
('Dorog', NULL, 2, 120, 4, 7, 1, 1, 2),
('Dorog', NULL, 3, 80, 3, 4.6, 1, 1, 2),
('Dorog', NULL, 2, 90, 3, 5, 1, 1, 1);

------------------------------------------------------------
-- Tábla: tipusok
------------------------------------------------------------

CREATE TABLE tipusok (
    tipus_ID INT NOT NULL IDENTITY(1, 1),
    tipus_nev NVARCHAR(25) NOT NULL
);

INSERT INTO tipusok (tipus_nev) VALUES
('Lakás'),
('Ház'),
('Házrész'),
('Belső portás ház'),
('Zártkerti épület');

------------------------------------------------------------
-- Tábla: ugynokok
------------------------------------------------------------

CREATE TABLE ugynokok (
    ugynok_ID INT NOT NULL IDENTITY(1, 1),
    ugynok_nev NVARCHAR(25) NOT NULL,
    telefon NVARCHAR(25) NULL,
    statusz BIT NOT NULL
);

INSERT INTO ugynokok (ugynok_nev, telefon, statusz) VALUES
('Dárdovits Róbert', '123456', 1),
('Gaál Éva', '234567', 1),
('Kiss Péter', '345678', 1),
('Nagy András', NULL, 1),
('Tóth Péter István', '1122334', 1);

------------------------------------------------------------
-- Indexek
------------------------------------------------------------

ALTER TABLE ingatlanok
ADD CONSTRAINT PK_ingatlanok PRIMARY KEY (ingatlan_ID);

ALTER TABLE tipusok
ADD CONSTRAINT PK_tipusok PRIMARY KEY (tipus_ID);

ALTER TABLE ugynokok
ADD CONSTRAINT PK_ugynokok PRIMARY KEY (ugynok_ID);

alter table ingatlanok add constraint FK_ugynokok foreign key (fk_ugynokID) references ugynokok(ugynok_ID) ON DELETE SET NULL;
alter table ingatlanok add constraint FK_tipusok foreign key (fk_tipusID) references tipusok(tipus_ID) ON DELETE CASCADE;

Go