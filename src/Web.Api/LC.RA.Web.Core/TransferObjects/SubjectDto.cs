namespace ReviewApp.Web.Core.TransferObjects
{
    public abstract class SubjectDto : RootModelBaseDto
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public decimal GeneralRaiting { get; set; }
    }
}