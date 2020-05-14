--@(#) sql_dump.sql

CREATE TABLE Naudotojai
(
	Prisijungimo_Vardas varchar (255),
	Slaptazodis varchar (255),
	El_Pastas varchar (255),
	Administratorius bit,
	id integer,
	PRIMARY KEY(id)
);

CREATE TABLE Zymes
(
	Autorius varchar (255),
	Isleidimo_data varchar (255),
	Albumas varchar (255),
	Zanras varchar (255),
	id integer,
	PRIMARY KEY(id)
);

CREATE TABLE Ivertinimai
(
	id integer,
	name char (13) NOT NULL,
	PRIMARY KEY(id)
);
INSERT INTO Ivertinimai(id, name) VALUES(1, 'Labai prastas')
INSERT INTO Ivertinimai(id, name) VALUES(2, 'Prastas')
INSERT INTO Ivertinimai(id, name) VALUES(3, 'Patenkinamas')
INSERT INTO Ivertinimai(id, name) VALUES(4, 'Vidutiniðkas')
INSERT INTO Ivertinimai(id, name) VALUES(5, 'Geras')
INSERT INTO Ivertinimai(id, name) VALUES(6, 'Labai geras')

CREATE TABLE RenginiuBusenos
(
	id integer,
	name char (11) NOT NULL,
	PRIMARY KEY(id)
);
INSERT INTO RenginiuBusenos(id, name) VALUES(1, 'Suplanuotas')
INSERT INTO RenginiuBusenos(id, name) VALUES(2, 'Atðauktas')
INSERT INTO RenginiuBusenos(id, name) VALUES(3, 'Vykstantis')
INSERT INTO RenginiuBusenos(id, name) VALUES(4, 'Ávykæs')

CREATE TABLE Administratorius
(
	id integer,
	PRIMARY KEY(id),
	FOREIGN KEY(id) REFERENCES Naudotojai (id)
);

CREATE TABLE Dainos
(
	Pavadinimas varchar (255),
	Atlikejas varchar (255),
	Klausymu_kiekis int,
	Ivertinimas integer,
	id integer,
	PRIMARY KEY(id),
	FOREIGN KEY(Ivertinimas) REFERENCES Ivertinimai (id)
);

CREATE TABLE Lankytojai
(
	Laukimo_laikas integer,
	Blokuotas bit,
	id integer,
	PRIMARY KEY(id),
	FOREIGN KEY(id) REFERENCES Naudotojai (id)
);

CREATE TABLE Barai
(
	Pavadinimas varchar (255),
	Platuma varchar (255),
	Ilguma varchar (255),
	Banko_saskaita varchar (255),
	Ivertinimas integer,
	id integer,
	Administratorius1 integer NOT NULL,
	PRIMARY KEY(id),
	UNIQUE(Administratorius1),
	FOREIGN KEY(Ivertinimas) REFERENCES Ivertinimai (id),
	CONSTRAINT Dirba FOREIGN KEY(Administratorius1) REFERENCES Administratorius (id)
);

CREATE TABLE DainosZymes
(
	Zymes integer,
	Daina integer,
	PRIMARY KEY(Zymes, Daina),
	CONSTRAINT Turi FOREIGN KEY(Zymes) REFERENCES Zymes (id)
);

CREATE TABLE Atsiliepimai
(
	Atsiliepimas varchar (255),
	Data date,
	Ivertinimas integer,
	id integer,
	Lankytojas integer NOT NULL,
	Baras integer NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY(Ivertinimas) REFERENCES Ivertinimai (id),
	CONSTRAINT Palieka FOREIGN KEY(Lankytojas) REFERENCES Lankytojai (id),
	CONSTRAINT Paliekami_apie FOREIGN KEY(Baras) REFERENCES Barai (id)
);

CREATE TABLE BlokuotiLankytojai
(
	Lankytojas integer,
	Baras integer,
	PRIMARY KEY(Lankytojas, Baras),
	CONSTRAINT Blokuoja FOREIGN KEY(Lankytojas) REFERENCES Lankytojai (id)
);

CREATE TABLE BlokuotosDainos
(
	Daina integer,
	Baras integer,
	PRIMARY KEY(Daina, Baras),
	CONSTRAINT Blokuotos FOREIGN KEY(Daina) REFERENCES Dainos (id)
);

CREATE TABLE DainuPasiulymai
(
	Data date,
	id integer,
	Lankytojas integer NOT NULL,
	Baras integer NOT NULL,
	Daina integer NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT Siulo FOREIGN KEY(Lankytojas) REFERENCES Lankytojai (id),
	CONSTRAINT Gauna FOREIGN KEY(Baras) REFERENCES Barai (id),
	CONSTRAINT Itraukiama_i FOREIGN KEY(Daina) REFERENCES Dainos (id)
);

CREATE TABLE Grojarasciai
(
	Pavadinimas varchar (255),
	id integer,
	Baras integer NOT NULL,
	Daina integer,
	PRIMARY KEY(id),
	CONSTRAINT Sudaro FOREIGN KEY(Baras) REFERENCES Barai (id),
	CONSTRAINT Turi_d FOREIGN KEY(Daina) REFERENCES Dainos (id)
);

CREATE TABLE LaikuIntervalai
(
	Pradzia date,
	Pabaiga date,
	id integer,
	Baras integer NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT Nustato FOREIGN KEY(Baras) REFERENCES Barai (id)
);

CREATE TABLE Renginiai
(
	Pavadinimas varchar (255),
	Data date,
	Kaina int,
	Aprasymas varchar (255),
	Plakatas varchar (255),
	Busena integer,
	id integer,
	Baras integer NOT NULL,
	PRIMARY KEY(id, Baras),
	FOREIGN KEY(Busena) REFERENCES RenginiuBusenos (id),
	FOREIGN KEY(Baras) REFERENCES Barai (id)
);

CREATE TABLE Saskaitos
(
	Suma double precision,
	Staliuko_nr int,
	Apmokejimo_data date,
	Pateikimo_data date,
	Apmoketa bit,
	id integer,
	Lankytojas integer NOT NULL,
	Baras integer NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT Apmoka FOREIGN KEY(Lankytojas) REFERENCES Lankytojai (id),
	CONSTRAINT Iveda FOREIGN KEY(Baras) REFERENCES Barai (id)
);

CREATE TABLE DainosGrojarasciai
(
	Daina integer,
	Grojarastis integer,
	PRIMARY KEY(Daina, Grojarastis),
	CONSTRAINT Susidaro_is FOREIGN KEY(Daina) REFERENCES Dainos (id)
);
