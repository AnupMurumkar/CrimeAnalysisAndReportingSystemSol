using System;
using System.Collections.Generic;
using CrimeAnalysisAndReportingSystemSol.Dao;
using CrimeAnalysisAndReportingSystemSol.Entity;
using CrimeAnalysisAndReportingSystemSol.Exceptions;

namespace CrimeAnalysisAndReportingSystemSol.Main
{
    public class MainModule
    {
        static void Main(string[] args)
        {
            // Create an instance of the service implementation
            CrimeAnalysisServiceImpl crimeService = new CrimeAnalysisServiceImpl();


            try
            {
                // 1. Create a new incident
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

                bool incidentCreated = crimeService.CreateIncident(newIncident);
                Console.WriteLine(incidentCreated ? "Incident created successfully." : "Failed to create incident.");

                // 2. Update the status of an incident
                Console.WriteLine("Enter Incident ID to update status:");
                int incidentIdToUpdate = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter new status for the incident:");
                string newStatus = Console.ReadLine();

                bool statusUpdated = crimeService.UpdateIncidentStatus(incidentIdToUpdate, newStatus);
                Console.WriteLine(statusUpdated ? "Incident status updated successfully." : "Failed to update incident status.");

                // 3. Get incidents within a date range
                Console.WriteLine("Enter start date (yyyy-mm-dd):");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter end date (yyyy-mm-dd):");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                List<Incident> incidentsInRange = crimeService.GetIncidentsInDateRange(startDate, endDate);
                Console.WriteLine($"Incidents between {startDate} and {endDate}:");
                foreach (var incident in incidentsInRange)
                {
                    Console.WriteLine($"{incident.IncidentId}: {incident.Description}");
                }

                // 4. Search incidents by type
                Console.WriteLine("Enter incident type to search:");
                string incidentType = Console.ReadLine();

                List<Incident> searchedIncidents = crimeService.SearchIncidents(incidentType);
                Console.WriteLine($"Incidents of type {incidentType}:");
                foreach (var incident in searchedIncidents)
                {
                    Console.WriteLine($"{incident.IncidentId}: {incident.Description}");
                }

                // 5. Generate an incident report
                Console.WriteLine("Enter Incident ID to generate a report:");
                int incidentIdForReport = int.Parse(Console.ReadLine());

                Incident incidentForReport = new Incident { IncidentId = incidentIdForReport }; // Assume an incident with this ID exists
                Report incidentReport = crimeService.GenerateIncidentReport(incidentForReport);
                Console.WriteLine($"Generated report for incident {incidentIdForReport}:\n{incidentReport.ReportDetails}");

                // 6. Create a new case and associate it with incidents
                Console.WriteLine("Creating a new case with the incidents...");
                List<Incident> caseIncidents = new List<Incident>
                {
                    newIncident // Associating the earlier created incident
                };

                Case newCase = crimeService.CreateCase("Robbery Case", caseIncidents);
                Console.WriteLine($"Case created with ID: {newCase.CaseId}");

                // 7. Get details of a specific case
                Console.WriteLine("Enter Case ID to get details:");
                int caseIdToGetDetails = int.Parse(Console.ReadLine());
                Case caseDetails = crimeService.GetCaseDetails(caseIdToGetDetails);
                Console.WriteLine($"Case {caseDetails.CaseId} details: {caseDetails.CaseDescription}");

                // 8. Update case details
                Console.WriteLine("Enter new case description for the case:");
                string newCaseDescription = Console.ReadLine();
                newCase.CaseDescription = newCaseDescription;

                bool caseUpdated = crimeService.UpdateCaseDetails(newCase);
                Console.WriteLine(caseUpdated ? "Case details updated successfully." : "Failed to update case details.");

                // 9. Get all cases
                List<Case> allCases = crimeService.GetAllCases();
                Console.WriteLine("All cases:");
                foreach (var caseItem in allCases)
                {
                    Console.WriteLine($"{caseItem.CaseId}: {caseItem.CaseDescription}");
                }
            }
            catch (IncidentNumberNotFoundException ex)
            {
                Console.WriteLine($"Custom error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input format: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
