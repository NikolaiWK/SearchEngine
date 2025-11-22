using System;
namespace Shared
{
    public static class Paths
    {
        public static string DATABASE =>
            Environment.GetEnvironmentVariable("DATABASE_PATH")
            ?? throw new Exception("DATABASE_PATH not set");
    }
}
