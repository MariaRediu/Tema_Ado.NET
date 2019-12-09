using Models;
using System;
using System.Collections.Generic;
using DataAccess;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Web.Script.Serialization;

namespace SummaryPublisherApp
{
    class Program
    {
      public static void Main(string[] args)
        {
            RepositoryPublisher publisherRepository = new RepositoryPublisher();
           List<Publisher> publisher = publisherRepository.SelectTop10();

            //publisherRepository.CalculatePrice();
            //publisherRepository.NrOfRowsFromPublisher();
            //publisherRepository.SelectTop10();
            //publisherRepository.SelectNumberOfBook();

            Publisher publi = new Publisher();
            publi.Name = "EnglishDictionary";
            publi.PublisherId = 15;
            var json = new JavaScriptSerializer().Serialize(publi);

            string filePath = @"C:\Users\Maria\Desktop\Tema2.txt";
            File.WriteAllText(filePath, json);

            // Console.WriteLine(json);

            //XmlSerializer xsSubmit = new XmlSerializer(typeof(List<Publisher>));

            //using (var sww = new StringWriter())
            //{
            //    using (XmlWriter writer = XmlWriter.Create(sww))
            //    {
            //        xsSubmit.Serialize(writer, publisher);
            //        string xml = sww.ToString(); // Your XML

            //        string filePath = @"C:\Users\Maria\Desktop\Tema2.txt";


            //        File.WriteAllText(filePath, xml);
            //    }


            //}


            Console.ReadLine();
        }
    }
}
