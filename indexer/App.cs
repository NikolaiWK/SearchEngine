using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared;

namespace Indexer
{
    public class App
    {
        public void Run()
        {
            DatabaseSqlite db = new DatabaseSqlite(Paths.DATABASE);
            Crawler crawler = new Crawler(db);

            var root = new DirectoryInfo(Config.FOLDER);

            DateTime start = DateTime.Now;
            crawler.IndexFilesIn(root, new List<string> { ".txt" });
            TimeSpan used = DateTime.Now - start;

            Console.WriteLine($"DONE! used {used.TotalMilliseconds} ms");

            var all = db.GetAllWords(); // Dictionary<string,int> med ord -> antal forekomster
            
            Console.WriteLine($"Indexed {db.DocumentCounts} documents");
            Console.WriteLine($"Number of different words: {all.Count}");

            
            // Spørg brugeren hvor mange ord der skal vises
            Console.Write("How many of the most frequent words do you want to see? ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.WriteLine("Invalid input. Showing top 10 by default.");
                count = 10;
            }

                    
            foreach (var p in all.OrderBy(x => x.Value).Take(count))
            {
                Console.WriteLine($"<{p.Key}> - {p.Value} - {db.CountTotalWords(p.Value)}");
                
            }
        }
    }
}
