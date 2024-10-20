// Incident.cs
namespace CrimeAnalysisAndReportingSystemSol.Entity
{
    public class Incident
    {
        private int incidentId;
        private string incidentType;
        private DateTime incidentDate;
        private string location;
        private string description;
        private string status;
        private int victimId;
        private int suspectId;

        // Default constructor
        public Incident() { }

        // Parameterized constructor
        public Incident(int incidentId, string incidentType, DateTime incidentDate, string location, string description, string status, int victimId, int suspectId)
        {
            this.incidentId = incidentId;
            this.incidentType = incidentType;
            this.incidentDate = incidentDate;
            this.location = location;
            this.description = description;
            this.status = status;
            this.victimId = victimId;
            this.suspectId = suspectId;
        }

        // Getters and setters
        public int IncidentId { get => incidentId; set => incidentId = value; }
        public string IncidentType { get => incidentType; set => incidentType = value; }
        public DateTime IncidentDate { get => incidentDate; set => incidentDate = value; }
        public string Location { get => location; set => location = value; }
        public string Description { get => description; set => description = value; }
        public string Status { get => status; set => status = value; }
        public int VictimId { get => victimId; set => victimId = value; }
        public int SuspectId { get => suspectId; set => suspectId = value; }
    }
}
