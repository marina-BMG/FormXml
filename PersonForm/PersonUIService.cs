using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EntityServices;
using System.Configuration;

namespace PersonForm
{
    public class PersonUIService : IEntityUIService<Person>
    {
        private XmlDocument xDoc = new XmlDocument();
        string path = ConfigurationManager.AppSettings["PersonFilePath"];

        public PersonUIService()
        {
            xDoc.Load(path);
        }

        private Person GetPersonFromXml(XmlNode personNode)
        {
            var newpers = new Person();
            newpers.ID = int.Parse(personNode.ChildNodes[0].InnerText);
            newpers.FirstName = personNode.ChildNodes[1].InnerText;

            return newpers;
        }

        public void Add(Person entity)
        {
            var pers = xDoc.CreateElement("Person");

            var id = xDoc.CreateElement("id");
            var firstname = xDoc.CreateElement("firstname");
            id.InnerText = entity.ID.ToString();
            firstname.InnerText = entity.FirstName;

            pers.AppendChild(id);
            pers.AppendChild(firstname);
            xDoc.DocumentElement.AppendChild(pers);
            xDoc.Save(path);
        }

        public void Delete(int id)
        {
            foreach (XmlNode item in xDoc.DocumentElement.ChildNodes)
            {
                int xmlId = int.Parse(item.ChildNodes[0].InnerText);
                if (xmlId == id)
                {
                    xDoc.DocumentElement.RemoveChild(item);
                    xDoc.Save(path);
                }
            }
        }

        public Person Find(int id)
        {
            foreach (XmlNode item in xDoc.DocumentElement.ChildNodes)
            {
                var newpers = new Person();
                int xmlId = int.Parse(item.ChildNodes[0].InnerText);
                if (xmlId == id) 
                {
                    return GetPersonFromXml(item);
                } 
            }
            return null;
        }

        public List<Person> ListAll()
        {
            var perslist = new List<Person>();
            foreach (XmlNode item in xDoc.DocumentElement.ChildNodes)
            {
                var newpers = GetPersonFromXml(item);
                perslist.Add(newpers);
            }
            return perslist;
        }

        public void Modify(Person entity)
        {
            foreach (XmlNode item in xDoc.DocumentElement.ChildNodes)
            {
                 int xmlId = int.Parse(item.ChildNodes[0].InnerText);
                if (xmlId == entity.ID)
                {
                    item.ChildNodes[1].InnerText = entity.FirstName;
                    xDoc.Save(path);
                    return;
                }
            }
        }
    }
}
