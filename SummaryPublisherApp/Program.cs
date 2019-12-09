using Models;
using System;
using System.Collections.Generic;
using DataAccess;

namespace SummaryPublisherApp
{
    class Program
    {
      public static void Main(string[] args)
        {
            RepositoryPublisher publisherRepository = new RepositoryPublisher();
            publisherRepository.CalculatePrice();
            


            Console.ReadLine();
        }
    }
}
