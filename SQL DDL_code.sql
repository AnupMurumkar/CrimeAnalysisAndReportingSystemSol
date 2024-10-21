create database CrimeReportingSystem
go
use CrimeReportingSystem
go

CREATE TABLE Victims (
    VictimID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10),
    ContactInformation NVARCHAR(255)
);

CREATE TABLE Suspects (
    SuspectID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10),
    ContactInformation NVARCHAR(255)
);

CREATE TABLE LawEnforcementAgencies (
    AgencyID INT PRIMARY KEY IDENTITY,
    AgencyName NVARCHAR(100) NOT NULL,
    Jurisdiction NVARCHAR(100),
    ContactInformation NVARCHAR(255)
);

CREATE TABLE Officers (
    OfficerID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    BadgeNumber NVARCHAR(50) UNIQUE,
    Rank NVARCHAR(50),
    ContactInformation NVARCHAR(255),
    AgencyID INT FOREIGN KEY REFERENCES LawEnforcementAgencies(AgencyID)
);

CREATE TABLE Incidents (
    IncidentID INT PRIMARY KEY IDENTITY,
    IncidentType NVARCHAR(50),
    IncidentDate DATETIME NOT NULL,
    Location GEOGRAPHY,
    Description NVARCHAR(255),
    Status NVARCHAR(50),
    VictimID INT FOREIGN KEY REFERENCES Victims(VictimID),
    SuspectID INT FOREIGN KEY REFERENCES Suspects(SuspectID),
    AgencyID INT FOREIGN KEY REFERENCES LawEnforcementAgencies(AgencyID)
);

CREATE TABLE Evidence (
    EvidenceID INT PRIMARY KEY IDENTITY,
    Description NVARCHAR(255),
    LocationFound NVARCHAR(255),
    IncidentID INT FOREIGN KEY REFERENCES Incidents(IncidentID)
);

CREATE TABLE Reports (
    ReportID INT PRIMARY KEY IDENTITY,
    IncidentID INT FOREIGN KEY REFERENCES Incidents(IncidentID),
    ReportingOfficer INT FOREIGN KEY REFERENCES Officers(OfficerID),
    ReportDate DATETIME,
    ReportDetails NVARCHAR(MAX),
    Status NVARCHAR(50)
);

CREATE TABLE Cases (
    CaseID INT PRIMARY KEY,                      
    CaseDescription VARCHAR(255) NOT NULL,       
    CreatedDate DATETIME NOT NULL,              
    Status VARCHAR(50) NOT NULL               
);

-- Junction table to relate cases with incidents
CREATE TABLE Case_Incidents (
    CaseID INT,                                  
    IncidentID INT,                              
    PRIMARY KEY (CaseID, IncidentID),
    FOREIGN KEY (CaseID) REFERENCES Cases(CaseID),         -- Reference to Cases table
    FOREIGN KEY (IncidentID) REFERENCES Incidents(IncidentID)  -- Reference to Incidents table
);

-- Insert into Victims
INSERT INTO Victims (FirstName, LastName, DateOfBirth, Gender, ContactInformation)
VALUES 
('John', 'Doe', '1985-05-20', 'Male', '123 Elm Street, City, 12345'),
('Jane', 'Smith', '1990-08-15', 'Female', '456 Oak Avenue, City, 67890');

-- Insert into Suspects
INSERT INTO Suspects (FirstName, LastName, DateOfBirth, Gender, ContactInformation)
VALUES 
('Mark', 'Johnson', '1983-11-10', 'Male', '789 Maple Lane, City, 11122'),
('Emily', 'Brown', '1992-04-25', 'Female', '321 Pine Street, City, 33344');

-- Insert into LawEnforcementAgencies
INSERT INTO LawEnforcementAgencies (AgencyName, Jurisdiction, ContactInformation)
VALUES 
('City Police Department', 'Citywide', '987 Main Street, City, 55566'),
('State Patrol', 'Statewide', '654 State Avenue, City, 77788');

-- Insert into Officers
INSERT INTO Officers (FirstName, LastName, BadgeNumber, Rank, ContactInformation, AgencyID)
VALUES 
('Michael', 'Scott', 'A123', 'Sergeant', '234 Birch Street, City, 88899', 1),
('Dwight', 'Schrute', 'B456', 'Lieutenant', '345 Cedar Road, City, 99900', 2);

-- Insert into Incidents
INSERT INTO Incidents (IncidentType, IncidentDate, Location, Description, Status, VictimID, SuspectID, AgencyID)
VALUES 
('Robbery', '2024-10-01', NULL, 'Bank robbery at Main Street', 'Open', 1, 1, 1),
('Assault', '2024-10-02', NULL, 'Physical assault at Oak Avenue', 'Under Investigation', 2, 2, 2);

-- Insert into Evidence
INSERT INTO Evidence (Description, LocationFound, IncidentID)
VALUES 
('Knife with fingerprints', 'Near the victim’s house', 1),
('CCTV footage from nearby shop', 'Main Street', 1);

-- Insert into Reports
INSERT INTO Reports (IncidentID, ReportingOfficer, ReportDate, ReportDetails, Status)
VALUES 
(1, 1, '2024-10-03', 'Initial report of robbery incident with evidence collection', 'Draft'),
(2, 2, '2024-10-04', 'Preliminary investigation of assault', 'Finalized');

-- Insert into Cases
INSERT INTO Cases (CaseID, CaseDescription, CreatedDate, Status)
VALUES 
(1001, 'Robbery Case involving multiple incidents', '2024-10-05', 'Open'),
(1002, 'Assault Case', '2024-10-06', 'Closed');

-- Insert into Case_Incidents (Junction table)
INSERT INTO Case_Incidents (CaseID, IncidentID)
VALUES 
(1001, 1),
(1002, 2);

