CREATE DATABASE InscripcionDb
GO
USE InscripcionDb
GO
CREATE TABLE Personas
(
	PersonaId int primary key identity,
	Nombre varchar(30),
	Telefono varchar(13),
	Cedula varchar(13),
	Direccion varchar(max),
	FechaNacimiento date
)
Go
CREATE TABLE Inscripciones
(
	InscripcionId int primary key identity,
	Fecha date,
	PersonaId int,
	Comentarios varchar(40),
	Balance int,
	Monto int

)