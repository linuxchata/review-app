namespace ReviewSystem.Core.Domain
{
    public sealed class Reviewer : RootModelBase
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }
    }
}
