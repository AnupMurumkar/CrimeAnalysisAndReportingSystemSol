namespace CrimeAnalysisAndReportingSystemSol.Entity
{
    public class Report
    {
        private int reportId;
        private int incidentId;
        private int reportingOfficerId;
        private DateTime reportDate;
        private string reportDetails;
        private string status;

        // Default constructor
        public Report() { }

        // Parameterized constructor
        public Report(int reportId, int incidentId, int reportingOfficerId, DateTime reportDate, string reportDetails, string status)
        {
            this.reportId = reportId;
            this.incidentId = incidentId;
            this.reportingOfficerId = reportingOfficerId;
            this.reportDate = reportDate;
            this.reportDetails = reportDetails;
            this.status = status;
        }

        // Getters and setters
        public int ReportId { get => reportId; set => reportId = value; }
        public int IncidentId { get => incidentId; set => incidentId = value; }
        public int ReportingOfficerId { get => reportingOfficerId; set => reportingOfficerId = value; }
        public DateTime ReportDate { get => reportDate; set => reportDate = value; }
        public string ReportDetails { get => reportDetails; set => reportDetails = value; }
        public string Status { get => status; set => status = value; }
    }
}
