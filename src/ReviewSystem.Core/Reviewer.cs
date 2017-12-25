namespace ReviewSystem.Core
{
    public sealed class Reviewer : CompleteModelBase
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }
    }
}
