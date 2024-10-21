using System;

namespace CrimeAnalysisAndReportingSystemSol.Exceptions
{
    public class IncidentNumberNotFoundException : Exception
    {
        public IncidentNumberNotFoundException(string message) : base(message) { }

    }
}
