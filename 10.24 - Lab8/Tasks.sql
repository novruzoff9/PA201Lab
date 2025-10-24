CREATE DATABASE Hospital;
USE Hospital;

CREATE TABLE Patients (
FirstName NVARCHAR(20),
LastName NVARCHAR(25),
BirthDate DATE,
Email NVARCHAR(30) UNIQUE,
CONSTRAINT CheckDate CHECK (BirthDate <= GETDATE())
);

ALTER TABLE Patients ADD Id INT IDENTITY (1,1) PRIMARY KEY

CREATE TABLE Departments
(
Id INT IDENTITY (1,1) PRIMARY KEY,
[Name] NVARCHAR(50) UNIQUE 
)

CREATE TABLE Doctors 
(
Id INT IDENTITY (1,1) PRIMARY KEY,
FirstName NVARCHAR(20),
LastName NVARCHAR(25),
Experience INT,
CONSTRAINT CheckExperience CHECK (Experience > 0)
)

ALTER TABLE Doctors ADD
DepartmentId INT FOREIGN KEY REFERENCES Departments(Id)

CREATE TABLE Appointments
(
Id INT IDENTITY PRIMARY KEY,
PatientId INT FOREIGN KEY REFERENCES Patients(Id),
DoctorId INT FOREIGN KEY REFERENCES Doctors(Id),
Result NVARCHAR(100),
[Date] DATETIME2
)

INSERT INTO Departments ([Name]) VALUES
('Cardiology'),
('Neurology'),
('Pediatrics'),
('Orthopedics'),
('Dermatology');

INSERT INTO Doctors (FirstName, LastName, Experience, DepartmentId) VALUES
('Araz', 'Mammadov', 10, 1),   -- Cardiology
('Lale', 'Aliyeva', 8, 2),     -- Neurology
('Rauf', 'Huseynov', 5, 3),    -- Pediatrics
('Nigar', 'Rahimova', 12, 4),  -- Orthopedics
('Elvin', 'Karimov', 7, 5);    -- Dermatology

INSERT INTO Patients (FirstName, LastName, BirthDate, Email) VALUES
('Kamran', 'Ahmadov', '1990-03-15', 'kamran90@mail.com'),
('Sevinc', 'Aliyeva', '1985-07-22', 'sevinc85@mail.com'),
('Murad', 'Ibrahimov', '2001-11-02', 'murad01@mail.com'),
('Aysel', 'Huseynli', '1995-04-18', 'aysel95@mail.com'),
('Rashad', 'Tagiyev', '1978-09-30', 'rashad78@mail.com');

INSERT INTO Appointments (PatientId, DoctorId, Result, [Date]) VALUES
(2, 3, 'Routine check-up. No issues found.', '2025-10-24 10:00'),
(1, 1, 'Routine check-up. No issues found.', '2025-10-01 10:00'),
(2, 2, 'MRI scheduled for next week.', '2025-09-20 14:30'),
(3, 3, 'Diagnosed with seasonal flu.', '2025-10-10 09:00'),
(4, 4, 'Recovery after knee surgery progressing well.', '2025-08-15 16:00'),
(5, 5, 'Skin allergy treated successfully.', '2025-10-05 11:15');



SELECT * FROM Doctors d
JOIN Departments dep
ON d.DepartmentId = dep.Id

SELECT p.FirstName Xeste, d.FirstName Hekim, a.Result Netice FROM Patients p
JOIN Appointments a 
ON p.Id = a.PatientId
JOIN Doctors d
ON a.DoctorId = d.Id

CREATE PROCEDURE AddAppointment 
@PatientId INT 
AS 
BEGIN
SELECT a.Date Tarix, d.FirstName Hekim, a.Result FROM Patients p 
JOIN Appointments a 
ON p.Id = a.PatientId
JOIN Doctors d 
ON a.DoctorId = d.Id
WHERE p.Id = @PatientId 
END

EXEC AddAppointment 1;


CREATE PROCEDURE TodaysAppointments @DoctorId INT
AS
BEGIN
SELECT p.FirstName + ' ' + p.LastName Pasient FROM Doctors d
JOIN Appointments a
ON d.Id = a.DoctorId
JOIN Patients p
ON a.PatientId = p.Id
WHERE CAST([Date] AS Date) = CAST(GETDATE() AS Date) AND d.Id = @DoctorId
END

EXEC TodaysAppointments 3

CREATE TABLE Medicine
(
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(30) NOT NULL
)

CREATE TABLE AppoinmentMedicines
(
Id INT PRIMARY KEY IDENTITY,
AppointmentId INT FOREIGN KEY REFERENCES Appointments(Id),
MedicineId INT FOREIGN KEY REFERENCES Medicine(Id)
)


INSERT INTO Medicine ([Name]) VALUES
('Paracetamol'),
('Amoxicillin'),
('Ibuprofen'),
('Vitamin C'),
('Aspirin'),
('Cough Syrup'),
('Antihistamine'),
('Omeprazole');

INSERT INTO AppoinmentMedicines (AppointmentId, MedicineId) VALUES
-- Appointment 1 (Kamran Ahmadov, Araz Mammadov - Cardiology)
(1, 1),  -- Paracetamol
(1, 4),  -- Vitamin C
(1, 8),  -- Omeprazole

-- Appointment 2 (Sevinc Aliyeva, Lale Aliyeva - Neurology)
(2, 3),  -- Ibuprofen
(2, 5),  -- Aspirin
(2, 7),  -- Antihistamine

-- Appointment 3 (Murad Ibrahimov, Rauf Huseynov - Pediatrics)
(3, 2),  -- Amoxicillin
(3, 4),  -- Vitamin C
(3, 6),  -- Cough Syrup

-- Appointment 4 (Aysel Huseynli, Nigar Rahimova - Orthopedics)
(4, 1),  -- Paracetamol
(4, 3),  -- Ibuprofen
(4, 5),  -- Aspirin
(4, 7),  -- Antihistamine

-- Appointment 5 (Rashad Tagiyev, Elvin Karimov - Dermatology)
(5, 7),  -- Antihistamine
(5, 4),  -- Vitamin C
(5, 8),  -- Omeprazole

-- Appointment 6 (Rashad Tagiyev, Elvin Karimov - Dermatology, earlier date)
(6, 1),  -- Paracetamol
(6, 2),  -- Amoxicillin
(6, 6),  -- Cough Syrup
(6, 5);  -- Aspirin



select d.FirstName doctor, p.FirstName patient, m.Name from Patients p
join Appointments a
on p.Id = a.PatientId
join AppoinmentMedicines am
on am.AppointmentId = a.Id
join Medicine m
on am.MedicineId = m.Id
join Doctors d
on d.Id = a.DoctorId




