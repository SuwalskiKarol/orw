using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XMLParsingExample.Models
{
    public static class XMLParsers
    {
        private static string xmlUrl = "http://localhost:7467/Student.xml";

        // Parse the xml using XMLDocument class.
        public static StudentsInformation ParseByXMLDocument()
        {
            var students = new StudentsInformation();

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);

            XmlNode GeneralInformationNode =
                doc.SelectSingleNode("/StudentsInformation/GeneralInformation");
            students.Szkoła =
                GeneralInformationNode.SelectSingleNode("Szkoła").InnerText;
            students.Wydział =
                GeneralInformationNode.SelectSingleNode("Wydział").InnerText;

            XmlNode StudentListNode =
                doc.SelectSingleNode("/StudentsInformation/Studentlist");
            XmlNodeList StudentNodeList =
                StudentListNode.SelectNodes("Student");
            foreach (XmlNode node in StudentNodeList)
            {
                Student aStudent = new Student();
                aStudent.id = Convert.ToInt16(node.Attributes
                    .GetNamedItem("id").Value);
                aStudent.imię = node.InnerText;
                aStudent.pkt = Convert.ToInt16(node.Attributes
                    .GetNamedItem("pkt").Value);
                aStudent.rekrutacja =
                    node.Attributes.GetNamedItem("rekrutacja").Value;
                aStudent.opis =
                    node.Attributes.GetNamedItem("opis").Value;

                students.Studentlist.Add(aStudent);
            }

            return students;
        }

        // Parse the xml using XDocument class.
        public static StudentsInformation ParseByXDocument()
        {
            var students = new StudentsInformation();

            XDocument doc = XDocument.Load(xmlUrl);
            XElement generalElement = doc
                    .Element("StudentsInformation")
                    .Element("GeneralInformation");
            students.Szkoła = generalElement.Element("Szkoła").Value;
            students.Wydział = generalElement.Element("Wydział").Value;

            students.Studentlist = (from c in doc.Descendants("Student")
                           select new Student()
                           {
                               id = Convert.ToInt16(c.Attribute("id").Value),
                               imię = c.Value,
                               pkt = Convert.ToInt16(c.Attribute("pkt").Value),
                               rekrutacja = c.Attribute("rekrutacja").Value,
                               opis = c.Attribute("opis").Value
                           }).ToList<Student>();

            return students;
        }
    }
}
