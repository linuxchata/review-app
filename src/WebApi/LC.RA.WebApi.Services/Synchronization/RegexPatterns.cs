namespace LC.RA.WebApi.Services.Synchronization
{
    public static class RegexPattern
    {
        public const string TableRowSpanMatchPattern = @"\|\-(?:.*\r??\n??)(!(.*))";

        public const string TableHeaderMatchPattern = @"!(?:.*\r?\n?)";

        public const string TableTextHeaderMatchPattern = @"(?!!)(?!\s)(.*)";

        public const string TableRowMatchPattern = @"\|\-(?:.*\r?\n?){5}";

        public const string TableRowSeparatorMatchPattern = @"\|\-(\n?\r?)*";

        public const string LocationNameMatchPattern = @"\[\[(.*?)(\]\])";

        public const string LocationNameCorretionMatchPattern = @"\|(.*)";

        public const string LocationRegionMatchPattern = @"\[\[(.*?)(\||\]\])";

        public const string BracesReplacePattern = @"[\[\]]";
    }
}