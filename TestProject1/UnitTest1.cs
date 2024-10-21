using NUnit.Framework;
using CrimeAnalysisAndReportingSystemSol.Dao;
using CrimeAnalysisAndReportingSystemSol.Entity;
using System;
using System.Collections.Generic;
using NUnit.Framework.Legacy;

namespace CrimeAnalysisAndReportingSystemSol.Test
{
    [TestFixture]
    public class CrimeAnalysisServiceTests
    {
        private CrimeAnalysisServiceImpl crimeService;

        [SetUp]
        public void Setup()
        {
            // Initialize the service before each test
            crimeService = new CrimeAnalysisServiceImpl();
        }

        [Test]
        public void TestCreateIncident_ValidIncident_ShouldReturnTrue()
        {
            // Arrange
            Incident newIncident = new Incident
            {
                IncidentType = "Robbery",
                IncidentDate = DateTime.Now,
                Location = "123 Main Street",
                Description = "A robbery at a local store.",
                Status = "Open",
                VictimId = 1,
                SuspectId = 1
            };

            // Act
            bool result = crimeService.CreateIncident(newIncident);

            // Assert
            ClassicAssert.IsTrue(result, "Incident creation should return true for a valid incident.");
        }

        [Test]
        public void TestUpdateIncidentStatus_ValidIncidentId_ShouldReturnTrue()
        {
            // Arrange
            int incidentId = 1; // Assume this incident ID exists in the database
            string newStatus = "Closed";

            // Act
            bool result = crimeService.UpdateIncidentStatus(incidentId, newStatus);

            // Assert
            ClassicAssert.IsTrue(result, "Incident status should be updated for a valid incident ID.");
        }

        [Test]
        public void TestGetIncidentsInDateRange_ShouldReturnIncidents()
        {
            // Arrange
            DateTime startDate = DateTime.Parse("2023-01-01");
            DateTime endDate = DateTime.Parse("2023-12-31");

            // Act
            List<Incident> incidents = crimeService.GetIncidentsInDateRange(startDate, endDate);

            // Assert
            ClassicAssert.IsNotNull(incidents, "The method should return a list of incidents.");
            ClassicAssert.IsTrue(incidents.Count > 0, "The list of incidents should not be empty.");
        }

        [Test]
        public void TestGenerateIncidentReport_ValidIncident_ShouldReturnReport()
        {
            // Arrange
            int incidentId = 1; // Assume this incident exists in the database
            Incident incident = new Incident { IncidentId = incidentId };

            // Act
            Report report = crimeService.GenerateIncidentReport(incident);

            // Assert
            ClassicAssert.IsNotNull(report, "The method should generate a report.");
            ClassicAssert.AreEqual(incidentId, report.IncidentId, "The report's incident ID should match the input incident ID.");
        }

        [Test]
        public void TestCreateCase_ValidCase_ShouldReturnNewCase()
        {
            // Arrange
            List<Incident> incidents = new List<Incident>
        {
            new Incident { IncidentId = 1, IncidentType = "Robbery" }
        };
            string caseDescription = "Robbery Case";

            // Act
            Case newCase = crimeService.CreateCase(caseDescription, incidents);

            // Assert
            ClassicAssert.IsNotNull(newCase, "The method should return a newly created case.");
            ClassicAssert.AreEqual(caseDescription, newCase.CaseDescription, "The case description should match the input description.");
        }

        [Test]
        public void TestGetCaseDetails_ValidCaseId_ShouldReturnCase()
        {
            // Arrange
            int caseId = 1; // Assume this case ID exists in the database

            // Act
            Case caseDetails = crimeService.GetCaseDetails(caseId);

            // Assert
            ClassicAssert.IsNotNull(caseDetails, "The method should return a case.");
            ClassicAssert.AreEqual(caseId, caseDetails.CaseId, "The case ID should match the input case ID.");
        }
    }
}