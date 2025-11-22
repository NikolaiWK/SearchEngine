using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared;

namespace Indexer
{
    public class App
    {
        private readonly string _dbPath;
        private readonly string _inputFolder;

        public App(string dbPath, string inputFolder)
        {
            _dbPath = dbPath;
            _inputFolder = inputFolder;
        }

        public void Run()
        {
            Console.WriteLine("Indexer starting...");
            Console.WriteLine($"Using database: {_dbPath}");
            Console.WriteLine($"Using input folder: {_inputFolder}");

            // Opret database
            var db = new DatabaseSqlite(_dbPath);

            // Opret crawler
            var crawler = new Crawler(db);

            // Input mappe
            var root = new DirectoryInfo(_inputFolder);

            if (!root.Exists)
            {
                Console.WriteLine($"INPUT FOLDER DOES NOT EXIST: {_inputFolder}");
                return;
            }

            // Kør indexing
            DateTime start = DateTime.Now;

            crawler.IndexFilesIn(root, new List<string> { ".txt" });

            TimeSpan used = DateTime.Now - start;

            Console.WriteLine($"DONE! Used {used.TotalMilliseconds} ms");

            var all = db.GetAllWords();

            Console.WriteLine($"Indexed {db.DocumentCounts} documents");
            Console.WriteLine($"Number of different words: {all.Count}");

            // Top ord
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
