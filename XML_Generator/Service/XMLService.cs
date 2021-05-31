using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using XML_Generator.Model;

namespace XML_Generator.Service
{
    public class XMLService
    {
        public void CreateFile(string filePath, List<Person> people)
        {
            var writer =
                 new XmlSerializer(typeof(List<Person>));

            var file = System.IO.File.Create(filePath);

            writer.Serialize(file, people);
            file.Close();
        }

        public List<Person> ReadFile(string filePath)
        {
            XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(List<Person>));
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            var result = (List<Person>)reader.Deserialize(file);
            file.Close();

            return result;
        }
    }
}
