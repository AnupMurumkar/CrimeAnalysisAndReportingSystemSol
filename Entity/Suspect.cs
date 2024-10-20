namespace CrimeAnalysisAndReportingSystemSol.Entity
{
    public class Suspect
    {
        private int suspectId;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string gender;
        private string contactInformation;

        // Default constructor
        public Suspect() { }

        // Parameterized constructor
        public Suspect(int suspectId, string firstName, string lastName, DateTime dateOfBirth, string gender, string contactInformation)
        {
            this.suspectId = suspectId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.contactInformation = contactInformation;
        }

        // Getters and setters
        public int SuspectId { get => suspectId; set => suspectId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Gender { get => gender; set => gender = value; }
        public string ContactInformation { get => contactInformation; set => contactInformation = value; }
    }
}
