
Insert into Naudotojai
	(Prisijungimo_Vardas, Slaptazodis, El_Pastas, Administratorius, id)
Values
	('test3', 'test3', '@', 0, 3),
	('test4', 'test4', '@', 0, 4),
	('test5', 'test5', '@', 0, 5),
	('test6', 'test6', '@', 0, 6),
	('test7', 'test7', '@', 0, 7),
	('test8', 'test8', '@', 0, 8),
	('test9', 'test9', '@', 0, 9),
	('test10', 'test10', '@', 0, 10),
	('test11', 'test11', '@', 0, 11),
	('test12', 'test12', '@', 0, 12),
	('test13', 'test13', '@', 0, 13),
	('test14', 'test14', '@', 0, 14)

Insert into Lankytojai
	(Laukimo_laikas, Blokuotas, id)
Values
	(0, 0,1),
	(1, 0,2),
	(2, 0,3),
	(2, 0,4),
	(2, 0,5),
	(2, 0,6),
	(2, 0,7),
	(2, 0,8),
	(2, 0,9),
	(2, 0,10),
	(2, 0,11),
	(2, 0,12)

Insert into Administratorius(id)
Values(3), (4), (5), (6)


Insert into Barai
	(Pavadinimas, Platuma, Ilguma, Banko_saskaita, Ivertinimas, id, Administratorius1, Aprasymas)
Values
	('barbarbar', '20.20', '30.30', 874845, 5, 3, 3, 'Test'),
	('barbarbar', '20.20', '30.30', 874845, 5, 4, 4, 'Test'),
	('barbarbar', '20.20', '30.30', 874845, 5, 5, 5, 'Test'),
	('barbarbar', '20.20', '30.30', 874845, 5, 6, 6, 'Test')

Insert into Renginiai
	(Pavadinimas, Data, Kaina, Aprasymas, Busena, Baras)
Values
	('pav', GETDATE(), 99, 'ouh', 1, 1),
	('pav', GETDATE(), 99, 'ouh', 1, 1),
	('pav', GETDATE(), 99, 'ouh', 1, 1),
	('pav', GETDATE(), 99, 'ouh', 1, 1),
	('pav', GETDATE(), 99, 'ouh', 1, 1),
	('pav', GETDATE(), 99, 'ouh', 1,  1),
	('pav', GETDATE(), 99, 'ouh', 1,  2),
	('pav', GETDATE(), 99, 'ouh', 1,  2),
	('pav', GETDATE(), 99, 'ouh', 1,  2),
	('pav', GETDATE(), 99, 'ouh', 1,  3),
	('pav', GETDATE(), 99, 'ouh', 1,  4),
	('pav', GETDATE(), 99, 'ouh', 1,  5)

Insert into Atsiliepimai
	(Atsiliepimas, Data, Ivertinimas, Lankytojas, Baras)
Values
	('neblogai', GETDATE(), 4, 2, 1),
	('neblogai', GETDATE(), 4, 3, 1),
	('neblogai', GETDATE(), 4, 4, 1),
	('neblogai', GETDATE(), 4, 5, 1),
	('neblogai', GETDATE(), 4, 6, 5),
	('neblogai', GETDATE(), 4, 7, 4),
	('neblogai', GETDATE(), 4, 8, 3),
	('neblogai', GETDATE(), 4, 9, 3),
	('neblogai', GETDATE(), 4, 2, 2),
	('neblogai', GETDATE(), 4, 3, 2),
	('neblogai', GETDATE(), 4, 4, 2),
	('neblogai', GETDATE(), 4, 5, 2),
	('neblogai', GETDATE(), 4, 6, 2),
	('neblogai', GETDATE(), 4, 7, 2)

--RenginiuBusenos

insert into Saskaitos
	(Suma, Staliuko_nr, Apmokejimo_data, Pateikimo_data, id, Lankytojas, Baras, Apmoketa)
values
	(66.96, 1, DATEADD(DAY, 1, GETDATE()), GETDATE(), 1, 3, 1,1),
	(66.96, 1, DATEADD(DAY, 1, GETDATE()), GETDATE(), 2, 3, 1,1),
	(66.96, 1, DATEADD(DAY, 1, GETDATE()), GETDATE(), 3, 3, 1,1),
	(66.96, 1, DATEADD(DAY, 1, GETDATE()), GETDATE(), 4, 3, 1,1),
	(66.96, 1, DATEADD(DAY, 1, GETDATE()), GETDATE(), 5, 3, 1,1),
	(66.96, 1, DATEADD(DAY, 1, GETDATE()), GETDATE(), 6, 3, 2,1),
	(66.96, 1, DATEADD(DAY, 1, GETDATE()), GETDATE(), 7, 3, 3,1)

--Ivertinimai

Insert into Zymes
Values
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 1),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 2),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 3),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 4),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 5),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 6),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 7),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 8),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 9),
	('Pranas', DATEADD(DAY, -10, GETDATE()), 'Be pavadinimo', 'Country', 10)


Insert into LaikuIntervalai
values
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 1, 1),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 2, 1),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 3, 1),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 4, 1),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 5, 2),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 6, 2),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 7, 4),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 8, 5),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 9, 5),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 10, 4),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 11, 5),
	(GETDATE(), DATEADD(HOUR, 6, GETDATE()), 12, 3)

Insert into BlokuotiLankytojai
Values
	(5,1),
	(3,2),
	(3,3),
	(4,1),
	(6,1),
	(7,1),
	(9,1)

Insert into Dainos
Values
	('Maria hy', 'Maria hu', 999, 5, 1),
	('Maria ha', 'Maria ha', 999, 5, 2),
	('sdf', 'fdgfdg bbbbhu', 999, 5, 3),
	('Msdf', 'dfg hvvvvvvu', 999, 5, 4),
	('sdfqqq hy', 'wqqe hu', 999, 5, 5),
	('sdf hy', 'tr hqqjqqu', 999, 5, 6),
	('Maria sdf', 'hh jjhu', 999, 5, 7),
	('Maria cxv', 'vvgj hu', 999, 5, 8)

Insert into Grojarasciai
Values
	('Pirmas', 1, 1, 1),
	('Antras', 2, 1, 1),
	('Nebe Antras', 3, 1, 1),
	('Nebe Antras', 4, 2, 1),
	('Nebe Antras', 5, 2, 1),
	('Nebe Antras', 6, 3, 1),
	('Nebe Antras', 7, 4, 1),
	('Nebe Antras', 8,5, 1),
	('Nebe Antras', 9, 6, 1)

Insert into BlokuotosDainos
Values
	(8, 1),
	(8, 2),
	(8, 3),
	(8, 4),
	(8, 5),
	(8, 6),
	(7, 1),
	(6, 1)

Insert into DainosGrojarasciai
values
	(1, 1),
	(2, 1),
	(3, 1),
	(4, 1),
	(5, 1),
	(1, 2),
	(2, 2),
	(1, 3),
	(2, 3)

Insert into DainosZymes
values
	(1, 1),
	(1, 2),
	(1, 3),
	(1, 4),
	(1, 5),
	(2, 6),
	(3, 7),
	(4, 8),
	(5, 9)

Insert into DainuPasiulymai
values 
	(GETDATE(), 1, 3, 1, 4),
	(GETDATE(), 2, 3, 1, 6),
	(GETDATE(), 3, 3, 1, 5),
	(GETDATE(), 4, 4, 1, 5),
	(GETDATE(), 5, 5, 1, 4),
	(GETDATE(), 6, 6, 1, 5),
	(GETDATE(), 7, 7, 2, 4),
	(GETDATE(), 8, 8, 2, 5),
	(GETDATE(), 9, 8, 2, 5),
	(GETDATE(), 10, 5, 2, 4)