
CREATE DATABASE "Test-db";

create table students
(
    id        serial primary key,
    Firstname varchar(50) not null,
    lastname  varchar(50),
    address   varchar(100),
    birthday  date,
    level     int
);
create table teachers
(
    id        serial primary key,
    Firstname varchar(50) not null,
    lastname  varchar(50),
    address   varchar(100),
    birthday  date,
    salary     decimal(10,2)
);