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

       
    }
}