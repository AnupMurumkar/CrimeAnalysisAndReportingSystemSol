namespace CrimeAnalysisAndReportingSystemSol.Entity
{
    public class Victim
    {
        private int victimId;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string gender;
        private string contactInformation;

        // Default constructor
        public Victim() { }

        // Parameterized constructor
        public Victim(int victimId, string firstName, string lastName, DateTime dateOfBirth, string gender, string contactInformation)
        {
            this.victimId = victimId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.contactInformation = contactInformation;
        }

        // Getters and setters
        public int VictimId { get => victimId; set => victimId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Gender { get => gender; set => gender = value; }
        public string ContactInformation { get => contactInformation; set => contactInformation = value; }
    }
}
