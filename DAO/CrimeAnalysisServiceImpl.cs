using CrimeAnalysisAndReportingSystemSol.Entity;
using CrimeAnalysisAndReportingSystemSol.Exceptions;
using CrimeAnalysisAndReportingSystemSol.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CrimeAnalysisAndReportingSystemSol.Dao
{
    public class CrimeAnalysisServiceImpl : ICrimeAnalysisService
    {
        private string conn;
        

        public CrimeAnalysisServiceImpl()
        {
            conn = DBConnection.ReturnCn("CrimeAnalysisAndReportingSystemCn");
            SqlConnection connection = new SqlConnection(conn);
        }

        // Create a new incident
        public bool CreateIncident(Incident incident)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Incidents (IncidentId , IncidentType, IncidentDate, Location, Description, Status, VictimID, SuspectID) VALUES (@id,@type, @date, @location, @description, @status, @victimId, @suspectId)", connection))
                {
                    cmd.Parameters.AddWithValue("@id", incident.IncidentId);
                    cmd.Parameters.AddWithValue("@type", incident.IncidentType);
                    cmd.Parameters.AddWithValue("@date", incident.IncidentDate);
                    cmd.Parameters.AddWithValue("@location", incident.Location);
                    cmd.Parameters.AddWithValue("@description", incident.Description);
                    cmd.Parameters.AddWithValue("@status", incident.Status);
                    cmd.Parameters.AddWithValue("@victimId", incident.VictimId);
                    cmd.Parameters.AddWithValue("@suspectId", incident.SuspectId);

                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    connection.Close();
                    
                    return rows > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Error creating incident: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error creating incident: " + ex.Message);
                return false;
            }
        }

        // Update the status of an incident
        public bool UpdateIncidentStatus(int incidentId, string status)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Incidents SET Status = @status WHERE IncidentID = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", incidentId);

                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    connection.Close();

                    if (rows == 0)
                    {
                        throw new IncidentNumberNotFoundException($"Incident with ID {incidentId} not found.");
                    }

                    return rows > 0;
                }
            }
            catch (IncidentNumberNotFoundException ex)
            {
                Console.WriteLine("Custom Error: " + ex.Message);
                return false;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Error updating incident status: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error updating incident status: " + ex.Message);
                return false;
            }
        }

        // Get a list of incidents within a date range
        public List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate)
        {
            List<Incident> incidents = new List<Incident>();
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Incidents WHERE IncidentDate BETWEEN @start AND @end", connection))
                {
                    cmd.Parameters.AddWithValue("@start", startDate);
                    cmd.Parameters.AddWithValue("@end", endDate);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Incident incident = new Incident
                            {
                                IncidentId = (int)reader["IncidentID"],
                                IncidentType = reader["IncidentType"].ToString(),
                                IncidentDate = (DateTime)reader["IncidentDate"],
                                Location = reader["Location"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                                VictimId = (int)reader["VictimID"],
                                SuspectId = (int)reader["SuspectID"]
                            };
                            incidents.Add(incident);
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Error retrieving incidents: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error retrieving incidents: " + ex.Message);
            }

            return incidents;
        }

        // Search for incidents based on incident type
        public List<Incident> SearchIncidents(string incidentType)
        {
            SqlConnection connection = new SqlConnection(conn);
            List<Incident> incidents = new List<Incident>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Incidents WHERE IncidentType = @type", connection))
                {
                    cmd.Parameters.AddWithValue("@type", incidentType);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            throw new IncidentNumberNotFoundException($"No incidents found with type {incidentType}.");
                        }

                        while (reader.Read())
                        {
                            Incident incident = new Incident
                            {
                                IncidentId = (int)reader["IncidentID"],
                                IncidentType = reader["IncidentType"].ToString(),
                                IncidentDate = (DateTime)reader["IncidentDate"],
                                Location = reader["Location"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                                VictimId = (int)reader["VictimID"],
                                SuspectId = (int)reader["SuspectID"]
                            };
                            incidents.Add(incident);
                        }
                    }
                    connection.Close();
                }
            }
            catch (IncidentNumberNotFoundException ex)
            {
                Console.WriteLine("Custom Error: " + ex.Message);
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("SQL Error searching incidents: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error searching incidents: " + ex.Message);
            }

            return incidents;
        }

        // Generate an incident report
        public Report GenerateIncidentReport(Incident incident)
        {
            SqlConnection connection = new SqlConnection(conn);
            Report report = new Report();
            try
            {
                report.ReportId = new Random().Next(1000, 9999);
                report.IncidentId = incident.IncidentId;
                report.ReportDetails = $"Report for Incident: {incident.Description}\nLocation: {incident.Location}\nDate: {incident.IncidentDate}\nStatus: {incident.Status}";
                report.ReportDate = DateTime.Now;
                report.Status = "Draft";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating report: " + ex.Message);
            }
            return report;
        }
        // Create a new case and associate it with incidents
        public Case CreateCase(string caseDescription, ICollection<Incident> incidents)
        {
            SqlConnection connection = new SqlConnection(conn);
            Case newCase = new Case();

            try
            {
                // Generate a random Case ID
                newCase.CaseId = new Random().Next(1000, 9999);
                newCase.CaseDescription = caseDescription;
                newCase.Incidents = incidents;
                newCase.CreatedDate = DateTime.Now; // Add created date
                newCase.Status = "Open"; // Set initial status as 'Open'

                // Open database connection
                connection.Open();

                // 1. Insert the case into the Cases table
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Cases (CaseID, CaseDescription, CreatedDate, Status) VALUES (@id, @description, @createdDate, @status)", connection))
                {
                    cmd.Parameters.AddWithValue("@id", newCase.CaseId);
                    cmd.Parameters.AddWithValue("@description", newCase.CaseDescription);
                    cmd.Parameters.AddWithValue("@createdDate", newCase.CreatedDate);
                    cmd.Parameters.AddWithValue("@status", newCase.Status);

                    int caseRows = cmd.ExecuteNonQuery();

                    if (caseRows > 0)
                    {
                        Console.WriteLine("Case created successfully.");
                    }
                    else
                    {
                        throw new Exception("Failed to create case.");
                    }
                }

                // 2. Insert each incident associated with the case into the Case_Incidents table
                foreach (var incident in incidents)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Case_Incidents (CaseID, IncidentID) VALUES (@caseId, @incidentId)", connection))
                    {
                        cmd.Parameters.AddWithValue("@caseId", newCase.CaseId);
                        cmd.Parameters.AddWithValue("@incidentId", incident.IncidentId);

                        int caseIncidentRows = cmd.ExecuteNonQuery();

                        if (caseIncidentRows > 0)
                        {
                            Console.WriteLine($"Incident {incident.IncidentId} associated with case {newCase.CaseId}.");
                        }
                        else
                        {
                            throw new Exception($"Failed to associate incident {incident.IncidentId} with case {newCase.CaseId}.");
                        }
                    }
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating case: " + ex.Message);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return newCase;
        }

        // Get details of a specific case
        public Case GetCaseDetails(int caseId)
        {
            SqlConnection connection = new SqlConnection(conn);
            Case caseDetails = null;

            try
            {
                connection.Open();

                // 1. Retrieve case details from the Cases table
                using (SqlCommand cmd = new SqlCommand("SELECT CaseID, CaseDescription, CreatedDate, Status FROM Cases WHERE CaseID = @caseId", connection))
                {
                    cmd.Parameters.AddWithValue("@caseId", caseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Initialize the caseDetails object if data is found
                            caseDetails = new Case
                            {
                                CaseId = (int)reader["CaseID"],
                                CaseDescription = reader["CaseDescription"].ToString(),
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                Status = reader["Status"].ToString(),
                                Incidents = new List<Incident>() // Initialize the incidents list
                            };
                        }
                        else
                        {
                            throw new Exception($"No case found with CaseID {caseId}.");
                        }
                    }
                }

                // 2. Retrieve associated incidents from Case_Incidents and Incidents tables
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT i.IncidentID, i.IncidentType, i.IncidentDate, i.Location, i.Description, i.Status 
            FROM Case_Incidents ci 
            JOIN Incidents i ON ci.IncidentID = i.IncidentID
            WHERE ci.CaseID = @caseId", connection))
                {
                    cmd.Parameters.AddWithValue("@caseId", caseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Add each related incident to the caseDetails object's Incidents list
                            Incident incident = new Incident
                            {
                                IncidentId = (int)reader["IncidentID"],
                                IncidentType = reader["IncidentType"].ToString(),
                                IncidentDate = (DateTime)reader["IncidentDate"],
                                Location = reader["Location"].ToString(),
                                Description = reader["Description"].ToString(),
                                Status = reader["Status"].ToString()
                            };

                            caseDetails.Incidents.Add(incident);
                        }
                    }
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving case details: " + ex.Message);
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return caseDetails;
        }


        // Update case details
        public bool UpdateCaseDetails(Case updatedCase)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Cases SET CaseDescription = @description, Status = @status WHERE CaseID = @id", connection))
                {
                    cmd.Parameters.AddWithValue("@description", updatedCase.CaseDescription);
                    cmd.Parameters.AddWithValue("@status", updatedCase.status);
                    cmd.Parameters.AddWithValue("@id", updatedCase.CaseId);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected > 0; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating case details: " + ex.Message);
                return false;
            }
        }

        public List<Case> GetAllCases()
        {
            SqlConnection connection = new SqlConnection(conn);
            List<Case> cases = new List<Case>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cases", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Case caseItem = new Case
                            {
                                CaseId = (int)reader["CaseID"],
                                CaseDescription = reader["CaseDescription"].ToString(),
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                Status = reader["Status"].ToString()
                            };
                            cases.Add(caseItem);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving cases: " + ex.Message);
            }

            return cases;
        }

        public Incident GetIncidentById(int incidentId)
        {

            Incident incident = new Incident();
            Console.WriteLine($"Incident ID: {incident.IncidentId}");

            if (incidentId == incident.IncidentId)
            {
                return incident;
            }
            else
            {
                throw new IncidentNumberNotFoundException("enter correct incident ID");
            }
        }
    }
}
