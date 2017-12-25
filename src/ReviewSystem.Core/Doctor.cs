using System.Collections.Generic;

namespace ReviewSystem.Core
{
    public sealed class Doctor : Subject
    {
        public string CertificateNumber { get; set; }

        public List<string> Schools { get; set; }

        public List<string> Internships { get; set; }

        public List<string> Degrees { get; set; }

        public List<string> Specializations { get; set; }

        public List<string> Publications { get; set; }

        public List<string> Diseases { get; set; }

        public List<string> Languages { get; set; }

        public Facility Facility { get; set; }
    }
}