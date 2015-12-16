using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XMLParsingExample.Models
{
    public class Student
    {
        public int id { get; set; }
        public string imię { get; set; }
        public int pkt { get; set; }
        public string rekrutacja { get; set; }
        public string opis { get; set; }
    }

    public class StudentsInformation
    {
        public string Szkoła { get; set; }
        public string Wydział { get; set; }
        public List<Student> Studentlist { get; set; }

        public StudentsInformation()
        {
            Szkoła = "N/A";
            Wydział = "N/A";
            Studentlist = new List<Student>();
        }
    }
}
