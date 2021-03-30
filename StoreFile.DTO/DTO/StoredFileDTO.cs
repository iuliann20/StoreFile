using System;

namespace StoreFile.TL.DTO
{
    public class StoredFileDTO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public DateTime? UploadDate { get; set; }
        public int UserId { get; set; }
    }
}
