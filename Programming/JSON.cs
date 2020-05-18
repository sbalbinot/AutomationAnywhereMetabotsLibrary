using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Programming
{
    public class JSON
    {
        public string JsonToXml(string json, string root, string row)
        {
            string result;

            XmlDocument doc = JsonConvert.DeserializeXmlNode("{\"" + row + "\":" + json + "}", root);

            XmlNodeList nodes = doc.SelectNodes("//" + row);

            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                XmlAttribute att = doc.CreateAttribute("ID");


                att.InnerText = (i + 1).ToString();
                node.Attributes.Append(att);
            }

            using (StringWriter writer = new StringWriter())
            {
                doc.Save(writer);
                result = writer.ToString();
            }

            return result;
        }
    }
}
