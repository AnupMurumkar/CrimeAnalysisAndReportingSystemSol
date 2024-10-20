namespace CrimeAnalysisAndReportingSystemSol.Entity
{
    public class LawEnforcementAgency
    {
        private int agencyId;
        private string agencyName;
        private string jurisdiction;
        private string contactInformation;

        // Default constructor
        public LawEnforcementAgency() { }

        // Parameterized constructor
        public LawEnforcementAgency(int agencyId, string agencyName, string jurisdiction, string contactInformation)
        {
            this.agencyId = agencyId;
            this.agencyName = agencyName;
            this.jurisdiction = jurisdiction;
            this.contactInformation = contactInformation;
        }

        // Getters and setters
        public int AgencyId { get => agencyId; set => agencyId = value; }
        public string AgencyName { get => agencyName; set => agencyName = value; }
        public string Jurisdiction { get => jurisdiction; set => jurisdiction = value; }
        public string ContactInformation { get => contactInformation; set => contactInformation = value; }
    }
}
