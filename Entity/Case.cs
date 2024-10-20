namespace CrimeAnalysisAndReportingSystemSol.Entity
{
    public class Case
    {
        private int caseId;
        private string caseDescription;
        private ICollection<Incident> incidents;
        public DateTime createdDate { get; set; } 
        public string status { get; set; }

        // Default constructor
        public Case() { }

        // Parameterized constructor
        public Case(int caseId, string caseDescription, ICollection<Incident> incidents , DateTime createdDate , string status)
        {
            this.caseId = caseId;
            this.caseDescription = caseDescription;
            this.incidents = incidents;
            this.createdDate = createdDate;
            this.status = status;   
        }

        // Getters and setters
        public int CaseId { get => caseId; set => caseId = value; }
        public string CaseDescription { get => caseDescription; set => caseDescription = value; }
        public ICollection<Incident> Incidents { get => incidents; set => incidents = value; }
        public DateTime CreatedDate { get => createdDate;   set => createdDate = value; }   
        public string Status { get => status; set => status = value; }
    }
}
