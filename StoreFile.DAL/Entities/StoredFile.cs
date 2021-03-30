using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreFile.DAL.Entities
{
    public class StoredFile
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public DateTime? UploadDate { get; set; }
        [ForeignKey("StoredFileUser")]
        public int UserId { get; set; }
        public StoredFileUser StoredFileUser { get; set; }
    }
}
