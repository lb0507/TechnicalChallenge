using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class ReleaseNotesModel
    {
        public string Notes_GUID { get; set; }
        public string Application_Name { get; set; }
        public string Note_Content { get; set; }
        public string Created_by { get; set; }
        public DateTime Created_date{ get; set; }
    }
}
