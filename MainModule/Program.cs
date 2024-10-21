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
            CrimeAnalysisServiceImpl crimeService = new CrimeAnalysisServiceImpl();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Create a new incident");
                Console.WriteLine("2. Update incident status");
                Console.WriteLine("3. Get incidents within a date range");
                Console.WriteLine("4. Search incidents by type");
                Console.WriteLine("5. Generate an incident report");
                Console.WriteLine("6. Create a new case");
                Console.WriteLine("7. Get details of a specific case");
                Console.WriteLine("8. Update case details");
                Console.WriteLine("9. Get all cases");
                Console.WriteLine("10. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        RunTask1(crimeService);
                        break;
                    case 2:
                        RunTask2(crimeService);
                        break;
                    case 3:
                        RunTask3(crimeService);
                        break;
                    case 4:
                        RunTask4(crimeService);
                        break;
                    case 5:
                        RunTask5(crimeService);
                        break;
                    case 6:
                        RunTask6(crimeService);
                        break;
                    case 7:
                        RunTask7(crimeService);
                        break;
                    case 8:
                        RunTask8(crimeService);
                        break;
                    case 9:
                        RunTask9(crimeService);
                        break;
                    case 10:
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        // 1. Create a new incident
        public static void RunTask1(CrimeAnalysisServiceImpl crimeService)
        {
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
        }

        // 2. Update the status of an incident
        public static void RunTask2(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
                Console.WriteLine("Enter Incident ID to update status:");
                int incidentIdToUpdate = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter new status for the incident:");
                string newStatus = Console.ReadLine();

                bool statusUpdated = crimeService.UpdateIncidentStatus(incidentIdToUpdate, newStatus);
                Console.WriteLine(statusUpdated ? "Incident status updated successfully." : "Failed to update incident status.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input format: {ex.Message}");
            }
        }

        // 3. Get incidents within a date range
        public static void RunTask3(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
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
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input format: {ex.Message}");
            }
        }

        // 4. Search incidents by type
        public static void RunTask4(CrimeAnalysisServiceImpl crimeService)
        {
            Console.WriteLine("Enter incident type to search:");
            string incidentType = Console.ReadLine();

            List<Incident> searchedIncidents = crimeService.SearchIncidents(incidentType);
            Console.WriteLine($"Incidents of type {incidentType}:");
            foreach (var incident in searchedIncidents)
            {
                Console.WriteLine($"{incident.IncidentId}: {incident.Description}");
            }
        }

        // 5. Generate an incident report
        public static void RunTask5(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
                Console.WriteLine("Enter Incident ID to generate a report:");
                int incidentIdForReport = int.Parse(Console.ReadLine());

                Incident incidentForReport = new Incident { IncidentId = incidentIdForReport };
                Report incidentReport = crimeService.GenerateIncidentReport(incidentForReport);
                Console.WriteLine($"Generated report for incident {incidentIdForReport}:\n{incidentReport.ReportDetails}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input format: {ex.Message}");
            }
        }

        // 6. Create a new case and associate it with incidents (Updated)
        public static void RunTask6(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
                // Input case description
                Console.WriteLine("Enter Case Description:");
                string caseDescription = Console.ReadLine();

                // Input incidents (only stored in memory, not in the database)
                Console.WriteLine("Enter Incident IDs to associate with the case (comma-separated):");
                string incidentIdsInput = Console.ReadLine();
                string[] incidentIds = incidentIdsInput.Split(',');

                // Retrieve incidents by their IDs (stored in memory only)
                List<Incident> caseIncidents = new List<Incident>();
                foreach (string incidentIdStr in incidentIds)
                {
                    if (int.TryParse(incidentIdStr.Trim(), out int incidentId))
                    {
                        Incident incident = crimeService.GetIncidentById(incidentId); // Assuming GetIncidentById method exists
                        if (incident != null)
                        {
                            caseIncidents.Add(incident);
                        }
                        else
                        {
                            Console.WriteLine($"Incident with ID {incidentId} not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Incident ID: {incidentIdStr}");
                    }
                }

                // Create case with incidents (incidents are not stored in the database)
                if (caseIncidents.Count > 0)
                {
                    Case newCase = crimeService.CreateCase(caseDescription, caseIncidents);
                    Console.WriteLine($"Case created with ID: {newCase.CaseId}");
                }
                else
                {
                    Console.WriteLine("No valid incidents were provided. Case created without incident association.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // 7. Get details of a specific case (Updated)
        public static void RunTask7(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
                Console.WriteLine("Enter Case ID to get details:");
                int caseIdToGetDetails = int.Parse(Console.ReadLine());

                Case caseDetails = crimeService.GetCaseDetails(caseIdToGetDetails);
                if (caseDetails != null)
                {
                    Console.WriteLine($"Case {caseDetails.CaseId} details: {caseDetails.CaseDescription}");
                    Console.WriteLine($"Created on: {caseDetails.CreatedDate}");
                    Console.WriteLine($"Status: {caseDetails.Status}");
                    Console.WriteLine("Associated Incidents:");

                    foreach (Incident incident in caseDetails.Incidents)
                    {
                        Console.WriteLine($"- Incident {incident.IncidentId}: {incident.Description}");
                    }
                }
                else
                {
                    Console.WriteLine($"No case found with CaseID {caseIdToGetDetails}.");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input format: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving case details: {ex.Message}");
            }
        }

        // 8. Update case details
        public static void RunTask8(CrimeAnalysisServiceImpl crimeService)
        {
            try
            {
                Console.WriteLine("Enter new case description for the case:");
                string newCaseDescription = Console.ReadLine();
                Console.WriteLine("Enter Case ID to update:");
                int caseIdToUpdate = int.Parse(Console.ReadLine());

                Case updatedCase = crimeService.GetCaseDetails(caseIdToUpdate); // Fetch existing case
                updatedCase.CaseDescription = newCaseDescription; // Update case description

                bool caseUpdated = crimeService.UpdateCaseDetails(updatedCase);
                Console.WriteLine(caseUpdated ? "Case details updated successfully." : "Failed to update case details.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid input format: {ex.Message}");
            }
        }

        // 9. Get all cases
        public static void RunTask9(CrimeAnalysisServiceImpl crimeService)
        {
            List<Case> allCases = crimeService.GetAllCases();
            Console.WriteLine("All cases:");
            foreach (var caseItem in allCases)
            {
                Console.WriteLine($"{caseItem.CaseId}: {caseItem.CaseDescription}");
            }
        }
    }
}
