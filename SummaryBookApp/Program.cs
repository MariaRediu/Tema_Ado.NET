using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SummaryBookApp
{
    class Program
    {
      public  static void Main(string[] args)
        {
            BookRepository bookRepository = new BookRepository();
            List<Book> books = bookRepository.GetAllBooks();
            //foreach (var book in books)
            //{
            //    Console.WriteLine($"{book.Title}---{book.Price}");
            //}
            // bookRepository.SelectTop10();
            // bookRepository.SelectYear();
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<Book>));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, books);
                    string xml = sww.ToString(); // Your XML

                    string filePath = @"C:\Users\Maria\Desktop\Tema.txt"; 
              

                    File.WriteAllText(filePath, xml);
                }

                //      File.WriteAllText();
            }
            
            Console.ReadLine();
        }
    }
}
