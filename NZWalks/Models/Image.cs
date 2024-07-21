namespace MWalks.API.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Extension { get; set; }
        public long SizeInBytes { get; set; }
        public string Path { get; set; }
    }

}
