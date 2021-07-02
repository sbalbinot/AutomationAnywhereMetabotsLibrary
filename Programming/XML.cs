using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

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

        public void fileRemoveNode(string xmlFile, string node)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement element = doc.XPathSelectElement(node);

            element.Remove();

            File.WriteAllText(xmlFile, doc.ToString());
        }

        public string getNodeByAttribute(string xml, string node, string attributeName, string identifierValue)
        {
            XDocument doc = XDocument.Parse(xml);

            XElement result = (from xml2 in doc.Descendants() where xml2.Name == node && xml2.Attribute(attributeName).Value == identifierValue select xml2).FirstOrDefault();

            return result.ToString();
        }

        public string getValueByNode(string xml, string node)
        {
            XDocument doc = XDocument.Parse(xml);

            XElement result = (from xml2 in doc.Descendants() where xml2.Name == node select xml2).FirstOrDefault();

            return result.Value.ToString();
        }

        public string fileGetValueByNode(string xmlFile, string node)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement result = (from xml2 in doc.Descendants() where xml2.Name == node select xml2).FirstOrDefault();

            return result.Value.ToString();
        }

        public string removeRootElement(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            XElement firstChild = doc.Root.Elements().First();

            XDocument output = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), firstChild);

            return output.ToString();
        }

        public string removeRootElementAttributes(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            doc.Root.RemoveAttributes();

            return doc.ToString();
        }

        public string addNodeAttribute(string xml, string node, string attributeName, string attributeValue)
        {
            XDocument doc = XDocument.Parse(xml);

            XElement element = doc.XPathSelectElement(node);

            XAttribute attribute = new XAttribute(attributeName, attributeValue);

            element.Add(attribute);

            return doc.ToString();
        }

        public void addNodeAttributeFile(string xmlFile, string node, string attributeName, string attributeValue)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement element = doc.XPathSelectElement(node);

            XAttribute attribute = new XAttribute(attributeName, attributeValue);

            element.Add(attribute);

            File.WriteAllText(xmlFile, doc.ToString());
        }

        public string updateNodeAttribute(string xml, string node, string attributeName, string attributeValue)
        {
            XDocument doc = XDocument.Parse(xml);

            XElement element = doc.XPathSelectElement(node);

            XAttribute attribute = element.Attribute(attributeName);

            attribute.SetValue(attributeValue);

            return doc.ToString();
        }

        public void updateNodeAttributeFile(string xmlFile, string node, string attributeName, string attributeValue)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement element = doc.XPathSelectElement(node);

            XAttribute attribute = element.Attribute(attributeName);

            attribute.SetValue(attributeValue);

            File.WriteAllText(xmlFile, doc.ToString());
        }

        public string removeNodeAttribute(string xml, string node, string attribute)
        {
            XDocument doc = XDocument.Parse(xml);

            XElement element = doc.XPathSelectElement(node);

            XAttribute result = (from att in element.Attributes() where att.Name == attribute select att).FirstOrDefault();

            result.Remove();

            return doc.ToString();
        }

        public void removeNodeAttributeFile(string xmlFile, string node, string attribute)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement element = doc.XPathSelectElement(node);

            XAttribute result = (from att in element.Attributes() where att.Name == attribute select att).FirstOrDefault();

            result.Remove();

            File.WriteAllText(xmlFile, doc.ToString());
        }

        public string removeNodeAttributes(string xml, string node)
        {
            XDocument doc = XDocument.Parse(xml);

            IEnumerable<XElement> elements = doc.XPathSelectElements(node);

            foreach (XElement element in elements)
            {
                element.RemoveAttributes();
            }

            return doc.ToString();
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

        public string fileFindAllXPathValues(string xmlFile, string xpath)
        {
            string retorno = "";

            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            foreach (XElement x in doc.XPathSelectElements(xpath).ToList())
            {
                retorno = retorno.Length == 0 ? x.Value.ToString() : retorno + "|" + x.Value.ToString();
            }

            return retorno == "" ? "Nenhum resultado encontrado" : retorno;
        }

        public string fileFindAllXPathValuesSubstring(string xmlFile, string xpath, int startIndex, int length)
        {
            string retorno = "";

            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            foreach (XElement x in doc.XPathSelectElements(xpath).ToList())
            {
                retorno = retorno.Length == 0 ? x.Value.ToString().Substring(startIndex, length) : retorno + "|" + x.Value.ToString().Substring(startIndex, length);
            }

            return retorno == "" ? "Nenhum resultado encontrado" : retorno;
        }

        public string fileGetXPathAttributeValue(string xmlFile, string xpath, string attributeName)
        {
            string retorno = "";

            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement x = doc.XPathSelectElements(xpath).FirstOrDefault();

            if (x == null)
                return "Nenhum resultado encontrado";

            retorno = x.Attribute(attributeName) == null ? "" : x.Attribute(attributeName).Value.ToString();

            return retorno == "" ? "Nenhum resultado encontrado" : retorno;
        }

        public void fileInsertNodeWhereXPath(string xmlFile, string xpath, string node, string nodeValue)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement x = new XElement(node, nodeValue);

            //Console.WriteLine(x.ToString());

            IEnumerable<XElement> xElements = doc.XPathSelectElements(xpath);

            foreach (XElement xElement in xElements)
            {
                //Console.WriteLine(xElement.ToString());

                xElement.Add(x);

                //Console.WriteLine(xElement.Parent.ToString());
            }

            doc.Save(xmlFile);
        }

        public void fileInsertNodeIntoParentWhereXPath(string xmlFile, string xpath, string node, string nodeValue)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XElement x = new XElement(node, nodeValue);

            //Console.WriteLine(x.ToString());

            IEnumerable<XElement> xElements = doc.XPathSelectElements(xpath);

            foreach(XElement xElement in xElements)
            {
                //Console.WriteLine(xElement.ToString());

                xElement.Parent.Add(x);

                //Console.WriteLine(xElement.Parent.ToString());
            }

            doc.Save(xmlFile);
        }

        public void fileInsertXMLStringIntoNode(string xmlString, string xmlFile, string node)
        {
            string xml = File.ReadAllText(xmlFile);

            XDocument doc = XDocument.Parse(xml);

            XDocument childDoc = XDocument.Parse(xmlString);

            doc.XPathSelectElement(node).Add(childDoc.Root);

            doc.Save(xmlFile);
        }

        public string ReadXMLFile(string xmlFile)
        {
            return System.IO.File.ReadAllText(xmlFile);
        }

        //Retornar todos os movimentos que tenham o tipo = "P"
        //public string fileFindAllXPathValuesConditional(string xmlFile, string xpath, string conditionalXPath, string conditionalValue)
        //{
        //    string retorno = "";

        //    string xml = File.ReadAllText(xmlFile);

        //    XDocument doc = XDocument.Parse(xml);

        //    List<XElement> elements = doc.Descendants("")


        //    XElement result = (from xml2 in doc.Descendants() where xml2.Name == node select xml2).FirstOrDefault();

        //    foreach (XElement x in doc.XPathSelectElements(xpath).ToList())
        //    {
        //        retorno = retorno.Length == 0 ? x.Value.ToString() : retorno + "|" + x.Value.ToString();
        //    }

        //    return retorno;
        //}
    }
}
