# MusicNPubs
Programų sistemų analizės ir projektavimo įrankiai projektinis darbas

Paleidimas:
Parsisiunčiat su *git bash* ar kokiu kitu *git interface'u* repozitorija.

Duombazės setup:
Atsidarot *SSMS* ir susikuriat lokaliam serve (*[PC Name]\SQLEXPRESS)* duombazę pavadinimu MuzikaIrBarai. Laikinai, kol nėr sugeneruoto viso *DB* kodo, pasileidžiat
```sql
CREATE TABLE Event (
    EventId int IDENTITY(1,1) PRIMARY KEY,
    Name varchar(255) NOT NULL,
    Date varchar(255),
    Price decimal,
    Description varchar(255),
    Poster varchar(255),
    BarId int NOT NULL
); 

Insert into Event(Name, Date, Price, Description, Poster, BarId)
values ('Quarantine', '2020-04-25', '20.01', '@home', 'www.plsstop.com', 1)
```

*Back-end* setup:
Atsidarot *solution'ą* per *Visual Studio* esantį *back-end* aplanke. Spaudžiat start. Prasitestuot metodus galit per atsidariusią naršyklę, url forma - *https://localhost:xxxx/api/[controller]/[action]/{id}*, o jei reikia metodo *request body* tai per *Postman*.

*Front-end* setup:
Atsidarot *front-end* aplanką per *VS Code*, į terminalą rašot ```npm install``` ir tada ```npm start``` ir per naršyklę einat į *http://localhost:8080*
