//using NUnit.Framework;
//using CrimeAnalysisAndReportingSystemSol.Entity;
//using CrimeAnalysisAndReportingSystemSol.Dao;
//using System;
//using CrimeAnalysisAndReportingSystemSol.Exceptions;

//namespace CrimeAnalysisAndReportingSystemTests
//{
//    [TestFixture]
//    public class CrimeAnalysisServiceTests
//    {
//        private CrimeAnalysisServiceImpl _crimeService;

//        [SetUp]
//        public void Setup()
//        {
//            // Initialize the service object before each test
//            _crimeService = new CrimeAnalysisServiceImpl();
//        }

//        // Test case for creating a new incident
//        [Test]
//        public void CreateIncident_CorrectAttributes_ReturnsTrue()
//        {
//            // Arrange
//            Incident testIncident = new Incident
//            {
//                IncidentId = 123,
//                IncidentType = "Robbery",
//                IncidentDate = DateTime.Now,
//                Location = "Test Location",
//                Description = "Test Description",
//                Status = "Open",
//                VictimId = 1,
//                SuspectId = 1
//            };

//            // Act
//            bool result = _crimeService.CreateIncident(testIncident);

//            // Assert
//            Assert.IsTrue(result, "The incident should be created successfully.");
//        }

//        // Test case for checking if incident attributes are accurate
//        [Test]
//        public void CreateIncident_VerifyAttributes_Correct()
//        {
//            // Arrange
//            Incident testIncident = new Incident
//            {
//                IncidentId = 456,
//                IncidentType = "Assault",
//                IncidentDate = DateTime.Now,
//                Location = "Test Street",
//                Description = "An assault case.",
//                Status = "Closed",
//                VictimId = 2,
//                SuspectId = 2
//            };

//            // Act
//            _crimeService.CreateIncident(testIncident);

//            // Assert
//            Assert.AreEqual("Assault", testIncident.IncidentType);
//            Assert.AreEqual("Test Street", testIncident.Location);
//            Assert.AreEqual("Closed", testIncident.Status);
//        }

//        // Test case for updating the status of an incident
//        [Test]
//        public void UpdateIncidentStatus_CorrectStatus_ReturnsTrue()
//        {
//            // Arrange
//            int incidentId = 123;
//            string newStatus = "Closed";

//            // Act
//            bool result = _crimeService.UpdateIncidentStatus(incidentId, newStatus);

//            // Assert
//            Assert.IsTrue(result, "The incident status should be updated successfully.");
//        }

//        // Test case for handling invalid status updates
//        [Test]
//        public void UpdateIncidentStatus_InvalidIncidentId_ThrowsException()
//        {
//            // Arrange
//            int invalidIncidentId = -1; // Non-existent incident ID
//            string newStatus = "Under Investigation";

//            // Act & Assert
//            var ex = Assert.Throws<IncidentNumberNotFoundException>(() => _crimeService.UpdateIncidentStatus(invalidIncidentId, newStatus));
//            Assert.AreEqual($"Incident with ID {invalidIncidentId} not found.", ex.Message);
//        }
//    }
//}
