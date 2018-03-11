using System.Diagnostics;

namespace LC.RA.Web.Core.Domain
{
    [DebuggerDisplay("Subject: {FirstName} {LastName}")]
    public abstract class Subject : RootModelBase
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Name
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }

        public decimal GeneralRaiting { get; set; }
    }
}