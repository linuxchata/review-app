﻿using System.Collections.Generic;

using ReviewApp.Web.Core.Domain;

namespace ReviewApp.Web.Core.TransferObjects
{
    public sealed class DoctorDto : SubjectDto
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