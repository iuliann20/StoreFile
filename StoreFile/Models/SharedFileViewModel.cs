using System;

namespace StoreFile.Models
{
    public class SharedFileViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}
