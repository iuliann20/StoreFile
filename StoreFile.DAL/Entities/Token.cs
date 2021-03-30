using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreFile.DAL.Entities
{
    public class Token
    {
        [Key]
        public int TokenId { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        [ForeignKey("StoredFileUser")]
        public int UserId { get; set; }
        public StoredFileUser StoredFileUser { get; set; }
    }
}
