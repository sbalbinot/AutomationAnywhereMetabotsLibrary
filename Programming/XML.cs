using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Programming
{
    public class XML
    {
        public string fileGetNodeByAttribute(string xmlFile, string node, string attributeName, string identifierValue)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement result = (from xml2 in doc.Descendants() where xml2.Name == node && xml2.Attribute(attributeName).Value == identifierValue select xml2).FirstOrDefault();

            return result.ToString();
        }

        public string getNodeByAttribute(string xml, string node, string attributeName, string identifierValue)
        {
            XDocument doc = XDocument.Parse(xml);

            XElement result = (from xml2 in doc.Descendants() where xml2.Name == node && xml2.Attribute(attributeName).Value == identifierValue select xml2).FirstOrDefault();

            return result.ToString();
        }

        public string removeRootElement(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            XElement firstChild = doc.Root.Elements().First();

            XDocument output = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), firstChild);

            return output.ToString();
        }

        public string changeElementNameRoot(string xml, string root)
        {
            XDocument doc = XDocument.Parse(xml);

            doc.Root.Name = root;

            return doc.ToString();
        }

        public string changeElementNameAndAddAttributeId(string xml, string from, string to, string attributeName)
        {
            int count = 1;

            XDocument doc = XDocument.Parse(xml);       

            IEnumerable<XElement> elements = doc.Root.Descendants(from);

            foreach (XElement element in elements)
            {
                XAttribute attribute = new XAttribute(attributeName, count);

                element.Name = to;
                element.Add(attribute);

                count++;
            }

            return doc.ToString();
        }

        public string addChildElementAttributeId(string xml, string parent, string child, string attributeName)
        {
            XDocument doc = XDocument.Parse(xml);

            IEnumerable<XElement> parentElements = doc.Descendants(parent);

            foreach (XElement parentElement in parentElements)
            {
                int count = 1;

                IEnumerable<XElement> childElements = parentElement.Elements(child);

                foreach (XElement childElement in childElements)
                {
                    XAttribute attribute = new XAttribute(attributeName, count);

                    childElement.Add(attribute);

                    count++;
                }
            }

            return doc.ToString();
        }

        public void saveStringToXmlFileUtf8(string xmlString, string xmlFile)
        {
            File.WriteAllText(xmlFile, xmlString, Encoding.UTF8);
        }

        public string findParentNodeByChildValueIncludes(string xml, string childAttributeName, string value) {
            XDocument doc = XDocument.Parse(xml);

            XElement result = (from xml2 in doc.Descendants() where xml2.Name == childAttributeName && xml2.Value.Contains(value) select xml2).FirstOrDefault();

            return result == null ? "Nenhum resultado encontrado" : result.Parent.ToString();
        }

        public string fileFindParentNodeByChildValueIncludes(string xmlFile, string childAttributeName, string value)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement result = (from xml2 in doc.Descendants() where xml2.Name == childAttributeName && xml2.Value.Contains(value) select xml2).FirstOrDefault();

            return result == null ? "Nenhum resultado encontrado" : result.Parent.ToString();
        }
    }
}
