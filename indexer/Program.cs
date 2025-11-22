using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace Indexer
{
    class Program
{
    static void Main(string[] args)
    {
        var dbPath = Environment.GetEnvironmentVariable("DATABASE_PATH");
        var folder = Environment.GetEnvironmentVariable("INPUT_FOLDER");

        if (string.IsNullOrEmpty(dbPath))
        {
            Console.WriteLine("DATABASE_PATH is not set!");
            return;
        }

        if (string.IsNullOrEmpty(folder))
        {
            Console.WriteLine("INPUT_FOLDER is not set!");
            return;
        }

        new App(dbPath, folder).Run();
    }
}
}
        
        
