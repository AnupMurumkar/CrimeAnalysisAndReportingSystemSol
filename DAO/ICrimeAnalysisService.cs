// ICrimeAnalysisService.cs
using CrimeAnalysisAndReportingSystemSol.Entity;

namespace CrimeAnalysisAndReportingSystemSol.Dao
{
    public interface ICrimeAnalysisService
    {
        bool CreateIncident(Incident incident);
        bool UpdateIncidentStatus(int incidentId , string status);
        List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate);
        List<Incident> SearchIncidents(string IncidentType);
        Report GenerateIncidentReport(Incident incident);
        Case CreateCase(string caseDescription, ICollection<Incident> incidents);
        Case GetCaseDetails(int caseId);
        bool UpdateCaseDetails(Case caseDetails);
        List<Case> GetAllCases();
    }
}
