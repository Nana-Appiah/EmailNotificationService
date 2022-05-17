
namespace Marathon.Model
{
    public class Message
    {
        public string? to { get; set; }
        public string? subject { get; set; }
        public string? body { get; set; }
        public string? cc { get; set; }
        public string? bcc { get; set; }
        public Attachment[] attachment { get; set; }
    }

    public class Attachment
    {
        public string fileName { get; set; }
        public string fileData { get; set; }
    }

}
