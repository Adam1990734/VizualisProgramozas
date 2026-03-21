USE IngatlanDb;

------------------------------------------------------------
-- Tábla: ingatlanok
------------------------------------------------------------

CREATE TABLE ingatlanok (
    ingatlan_ID INT NOT NULL DEFAULT 0,
    helyseg NVARCHAR(25) NOT NULL,
    kerulet INT NULL,
    fk_tipusID INT NOT NULL,
    terulet INT NOT NULL,
    szobaszam INT NOT NULL,
    ar FLOAT NOT NULL,
    garazs BIT NOT NULL,
    zoldovezet BIT NOT NULL,
    fk_ugynokID INT NOT NULL
);

------------------------------------------------------------
-- Adatok: ingatlanok
------------------------------------------------------------

INSERT INTO ingatlanok (ingatlan_ID, helyseg, kerulet, fk_tipusID, terulet, szobaszam, ar, garazs, zoldovezet, fk_ugynokID) VALUES
(1100, 'Budapest', 7, 1, 55, 2, 12.1, 1, 1, 1),
(1101, 'Kecskemét', NULL, 1, 24, 1, 25, 0, 0, 1),
(1102, 'Szeged', NULL, 1, 60, 2, 13.2, 0, 1, 1),
(1103, 'Budapest', 3, 1, 100, 5, 22, 1, 1, 1),
(1104, 'Budapest', 3, 1, 110, 6, 24.2, 1, 1, 1),
(1105, 'Budapest', 7, 1, 140, 8, 30.8, 0, 0, 1),
(1106, 'Kecskemét', NULL, 1, 80, 4, 17.6, 1, 1, 1),
(1107, 'Szeged', NULL, 1, 90, 4, 19.8, 0, 0, 2),
(1108, 'Kecskemét', NULL, 2, 75, 3, 14, 1, 1, 2),
(1109, 'Budapest', 7, 1, 54, 2, 11.9, 0, 1, 2),
(1110, 'Budapest', 7, 1, 34, 1, 7.5, 1, 0, 3),
(1111, 'Szeged', NULL, 1, 29, 1, 6.4, 1, 1, 3),
(1112, 'Budapest', 7, 1, 30, 1, 6.6, 0, 0, 2),
(1113, 'Szeged', NULL, 1, 60, 2, 13.2, 1, 1, 1),
(1114, 'Budapest', 7, 1, 70, 3, 15.4, 0, 1, 1),
(1115, 'Kecskemét', NULL, 1, 40, 1, 8.8, 1, 0, 1),
(1116, 'Budapest', 1, 1, 70, 3, 60, 0, 1, 1),
(1117, 'Budapest', 7, 1, 77, 4, 16.9, 0, 0, 2),
(1118, 'Budapest', 7, 1, 89, 4, 19.6, 1, 1, 2),
(1119, 'Szeged', NULL, 2, 102, 5, 17, 0, 1, 2),
(1120, 'Szeged', NULL, 1, 110, 6, 24.2, 0, 1, 2),
(1121, 'Budapest', 7, 1, 15, 1, 3.3, 1, 1, 3),
(1122, 'Kecskemét', NULL, 2, 300, 8, 48, 1, 1, 3),
(1123, 'Budapest', 7, 1, 45, 1, 9.9, 0, 0, 1),
(1124, 'Budapest', 7, 1, 48, 1, 10.6, 1, 1, 1),
(1125, 'Kecskemét', NULL, 1, 79, 2, 17.4, 1, 1, 1),
(1126, 'Budapest', 3, 1, 67, 3, 14.7, 0, 1, 2),
(1127, 'Budapest', 3, 1, 66, 3, 14.5, 0, 1, 1),
(1128, 'Budapest', 3, 1, 77, 2, 16.9, 1, 1, 2),
(1129, 'Kecskemét', NULL, 1, 45, 1, 9.9, 1, 1, 3),
(1130, 'Budapest', 7, 1, 49, 2, 10.8, 0, 1, 1),
(1131, 'Szeged', NULL, 1, 58, 2, 12.8, 1, 0, 2),
(1132, 'Budapest', 7, 1, 58, 2, 12.8, 1, 1, 2),
(1133, 'Budapest', 7, 1, 59, 2, 13, 1, 1, 2),
(1134, 'Budapest', 7, 1, 64, 3, 14.1, 1, 1, 2),
(1135, 'Budapest', 7, 1, 75, 3, 16.5, 1, 1, 2),
(1136, 'Kecskemét', NULL, 1, 78, 4, 17.2, 1, 0, 1),
(1137, 'Szeged', NULL, 1, 84, 4, 18.5, 0, 1, 1),
(1138, 'Szeged', NULL, 1, 81, 4, 17.8, 0, 1, 1),
(1139, 'Budapest', 6, 1, 80, 4, 17.6, 1, 0, 1),
(1140, 'Budapest', 6, 1, 90, 4, 19.8, 1, 1, 1),
(1141, 'Budapest', 6, 1, 95, 4, 20.9, 1, 1, 1),
(1142, 'Budapest', 5, 1, 94, 4, 20.7, 1, 1, 1),
(1143, 'Kecskemét', NULL, 1, 97, 3, 21.3, 1, 1, 1),
(1144, 'Kecskemét', NULL, 1, 105, 5, 23.1, 0, 1, 2),
(1145, 'Budapest', 18, 1, 120, 4, 19, 1, 1, 2),
(1146, 'Budapest', 18, 1, 125, 4, 18, 1, 1, 2),
(1147, 'Szeged', NULL, 1, 140, 5, 30.8, 1, 0, 1),
(1148, 'Budapest', 5, 1, 145, 5, 31.9, 1, 1, 1),
(1149, 'Szeged', NULL, 1, 130, 5, 28.6, 0, 0, 2),
(1150, 'Kecskemét', NULL, 1, 170, 6, 37.4, 1, 1, 1),
(1151, 'Budapest', 22, 1, 100, 5, 22, 0, 1, 2),
(1152, 'Budapest', 20, 2, 150, 9, 33, 1, 1, 1),
(1153, 'Budapest', 19, 2, 130, 7, 28.6, 1, 1, 3),
(1154, 'Budapest', 16, 3, 170, 5, 64, 1, 1, 2),
(1155, 'Szeged', NULL, 2, 190, 6, 70, 1, 0, 2),
(1156, 'Szeged', NULL, 1, 110, 4, 24.2, 1, 1, 3),
(1157, 'Kecskemét', NULL, 3, 40, 1, 8.8, 1, 1, 2),
(1158, 'Budapest', 17, 2, 140, 5, 30.8, 0, 1, 3),
(1159, 'Budapest', 14, 1, 150, 4, 33, 1, 1, 2),
(1160, 'Budapest', 13, 1, 49, 2, 10.8, 0, 1, 1),
(1161, 'Szeged', NULL, 1, 49, 2, 10.8, 1, 1, 1),
(1162, 'Budapest', 16, 1, 47, 1, 13, 1, 0, 1),
(1163, 'Kecskemét', NULL, 3, 74, 3, 16.3, 1, 0, 1),
(1164, 'Kecskemét', NULL, 2, 75, 3, 13, 1, 1, 1),
(1165, 'Szeged', NULL, 1, 68, 3, 11, 0, 1, 1),
(1166, 'Budapest', 3, 1, 50, 2, 11, 1, 1, 2),
(1167, 'Szeged', NULL, 1, 50, 2, 8, 1, 1, 2),
(1168, 'Budapest', 9, 1, 50, 2, 9, 1, 1, 3),
(1169, 'Budapest', 8, 1, 60, 2, 8.5, 1, 1, 2),
(1170, 'Budapest', 8, 1, 60, 2, 10, 1, 1, 1),
(1171, 'Kecskemét', NULL, 1, 62, 2, 9, 0, 0, 2),
(1172, 'Budapest', 7, 1, 98, 5, 21.6, 0, 1, 3),
(1173, 'Szeged', NULL, 1, 68, 3, 15, 1, 1, 3),
(1174, 'Szeged', NULL, 1, 68, 3, 15, 1, 1, 2),
(1175, 'Budapest', 6, 1, 69, 3, 15.2, 1, 0, 2),
(1176, 'Budapest', 5, 1, 100, 5, 22, 1, 1, 2),
(1177, 'Budapest', 4, 1, 100, 5, 22, 1, 1, 1),
(1178, 'Kecskemét', NULL, 1, 100, 4, 22, 1, 1, 1),
(1179, 'Szeged', NULL, 2, 170, 3, 17, 0, 0, 1),
(1180, 'Tatabánya', NULL, 2, 190, 4, 19, 1, 1, 2),
(1181, 'Tatabánya', NULL, 2, 110, 3, 11, 1, 1, 3),
(1182, 'Kecskemét', NULL, 2, 40, 2, 4, 0, 1, 2),
(1183, 'Szeged', NULL, 2, 140, 3, 14, 1, 1, 1),
(1184, 'Tatabánya', NULL, 2, 150, 3, 15, 1, 1, 3),
(1185, 'Kecskemét', NULL, 3, 49, 2, 4.9, 1, 1, 2),
(1186, 'Tatabánya', NULL, 2, 49, 2, 4.9, 0, 1, 1),
(1187, 'Tatabánya', NULL, 3, 47, 1, 4.7, 1, 0, 2),
(1188, 'Tatabánya', NULL, 2, 74, 3, 7.4, 1, 0, 3),
(1189, 'Tatabánya', NULL, 2, 170, 4, 11.9, 1, 1, 3),
(1190, 'Tatabánya', NULL, 2, 190, 4, 13.3, 1, 1, 2),
(1191, 'Tatabánya', NULL, 2, 110, 4, 7.7, 1, 1, 1),
(1192, 'Tatabánya', NULL, 1, 40, 3, 2.8, 1, 1, 2),
(1193, 'Tatabánya', NULL, 2, 140, 3, 14, 0, 1, 3),
(1194, 'Tatabánya', NULL, 2, 150, 4, 15, 1, 1, 2),
(1195, 'Dorog', NULL, 3, 90, 3, 5.2, 1, 0, 1),
(1196, 'Dorog', NULL, 2, 100, 4, 5.2, 1, 1, 2),
(1197, 'Dorog', NULL, 2, 120, 4, 7, 1, 1, 2),
(1198, 'Dorog', NULL, 3, 80, 3, 4.6, 1, 1, 2),
(1199, 'Dorog', NULL, 2, 90, 3, 5, 1, 1, 1);

------------------------------------------------------------
-- Tábla: tipusok
------------------------------------------------------------

CREATE TABLE tipusok (
    tipus_ID INT NOT NULL DEFAULT 0,
    tipus_nev NVARCHAR(25) NOT NULL
);

INSERT INTO tipusok (tipus_ID, tipus_nev) VALUES
(1, 'Lakás'),
(2, 'Ház'),
(3, 'Házrész'),
(4, 'Belső portás ház'),
(5, 'Zártkerti épület');

------------------------------------------------------------
-- Tábla: ugynokok
------------------------------------------------------------

CREATE TABLE ugynokok (
    ugynok_ID INT NOT NULL DEFAULT 0,
    ugynok_nev NVARCHAR(25) NOT NULL,
    telefon NVARCHAR(25) NULL,
    statusz BIT NOT NULL
);

INSERT INTO ugynokok (ugynok_ID, ugynok_nev, telefon, statusz) VALUES
(1, 'Dárdovits Róbert', '123456', 1),
(2, 'Gaál Éva', '234567', 1),
(3, 'Kiss Péter', '345678', 1),
(5, 'Nagy András', NULL, 1),
(6, 'Tóth Péter István', '1122334', 1);

------------------------------------------------------------
-- Indexek
------------------------------------------------------------

ALTER TABLE ingatlanok
ADD CONSTRAINT PK_ingatlanok PRIMARY KEY (ingatlan_ID);

ALTER TABLE tipusok
ADD CONSTRAINT PK_tipusok PRIMARY KEY (tipus_ID);

ALTER TABLE ugynokok
ADD CONSTRAINT PK_ugynokok PRIMARY KEY (ugynok_ID);

alter table ingatlanok add constraint FK_ugynokok foreign key (fk_ugynokID) references ugynokok(ugynok_ID);
alter table ingatlanok add constraint FK_tipusok foreign key (fk_tipusID) references tipusok(tipus_ID);

Go