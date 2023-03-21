using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace XmlToJsonConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: XmlToJsonConverter.exe input.xml output.json");
                return;
            }

            string inputFilePath = args[0];
            string outputFilePath = args[1];

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(inputFilePath);

                var json = new Newtonsoft.Json.Linq.JObject();

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.Name == "string")
                    {
                        var name = node.Attributes["name"].Value;
                        var value = node.InnerText;

                        json[name] = value;
                    }
                }

                File.WriteAllText(outputFilePath, json.ToString());
                Console.WriteLine($"JSON file saved to {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
