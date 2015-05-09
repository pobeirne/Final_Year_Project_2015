namespace LuckyMe.CMS.Common.Models
{
    public class BlobFile
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
        public string FileMime { get; set; }
        public string Source { get; set; }
        public string FileId { get; set; }
    }
}