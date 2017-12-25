using System.Diagnostics;

namespace ReviewSystem.Core
{
    [DebuggerDisplay("Subject: {FirstName} {LastName}")]
    public abstract class Subject : RootModelBase
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public decimal GeneralRaiting { get; set; }
    }
}