CREATE DATABASE medicine;

USE medicine;

CREATE TABLE doctor(
	num int PRIMARY KEY, 
	name varchar(255) NOT NULL, 
	department varchar(255) NOT NULL
);

CREATE TABLE patient(
	id int IDENTITY(1,1) PRIMARY KEY, 
	name varchar(255) NOT NULL, 
	doctorNum int FOREIGN KEY REFERENCES doctor(num)
);

CREATE TABLE medicine(
	id int IDENTITY(1,1) PRIMARY KEY, 
	name varchar(255) NOT NULL UNIQUE, 
	symptom varchar(255) NOT NULL, 
	effect varchar(255) NOT NULL, 
	sideEffect varchar(255), 
	dose varchar(255) NOT NULL
);

CREATE TABLE record(
	id int IDENTITY(1,1) PRIMARY KEY, 
	time varchar(255) NOT NULL, 
	quantity int NOT NULL, 
	patientId int FOREIGN KEY REFERENCES patient(id), 
	medicineId int FOREIGN KEY REFERENCES medicine(id),
	docNum int FOREIGN KEY REFERENCES doctor(num), 
	getMedTime varchar(255)
);
