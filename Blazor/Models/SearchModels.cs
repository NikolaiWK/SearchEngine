namespace Blazor.Models
{
    public class Document
    {
        public int mId { get; set; }
        public string mUrl { get; set; } = "";
        public string mIdxTime { get; set; } = "";
        public string mCreationTime { get; set; } = "";
    }

    public class DocumentHit
    {
        public Document document { get; set; } = new Document();
        public int noOfHits { get; set; }
        public List<string> missing { get; set; } = new();
    }

    public class SearchResponse
    {
        public List<string> query { get; set; } = new();
        public int hits { get; set; }
        public List<DocumentHit> documentHits { get; set; } = new();
        public List<string> ignored { get; set; } = new();
        public string timeUsed { get; set; } = "";
    }
}
