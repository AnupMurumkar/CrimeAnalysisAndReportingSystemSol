namespace CrimeAnalysisAndReportingSystemSol.Entity
{
    public class Officer
    {
        private int officerId;
        private string firstName;
        private string lastName;
        private string badgeNumber;
        private string rank;
        private string contactInformation;
        private int agencyId;

        // Default constructor
        public Officer() { }

        // Parameterized constructor
        public Officer(int officerId, string firstName, string lastName, string badgeNumber, string rank, string contactInformation, int agencyId)
        {
            this.officerId = officerId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.badgeNumber = badgeNumber;
            this.rank = rank;
            this.contactInformation = contactInformation;
            this.agencyId = agencyId;
        }

        // Getters and setters
        public int OfficerId { get => officerId; set => officerId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string BadgeNumber { get => badgeNumber; set => badgeNumber = value; }
        public string Rank { get => rank; set => rank = value; }
        public string ContactInformation { get => contactInformation; set => contactInformation = value; }
        public int AgencyId { get => agencyId; set => agencyId = value; }
    }
}
