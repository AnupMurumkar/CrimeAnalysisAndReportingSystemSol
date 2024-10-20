namespace CrimeAnalysisAndReportingSystemSol.Entity
{
    public class Evidence
    {
        private int evidenceId;
        private string description;
        private string locationFound;
        private int incidentId;

        // Default constructor
        public Evidence() { }

        // Parameterized constructor
        public Evidence(int evidenceId, string description, string locationFound, int incidentId)
        {
            this.evidenceId = evidenceId;
            this.description = description;
            this.locationFound = locationFound;
            this.incidentId = incidentId;
        }

        // Getters and setters
        public int EvidenceId { get => evidenceId; set => evidenceId = value; }
        public string Description { get => description; set => description = value; }
        public string LocationFound { get => locationFound; set => locationFound = value; }
        public int IncidentId { get => incidentId; set => incidentId = value; }
    }
}
