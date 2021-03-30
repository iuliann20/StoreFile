using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreFile.DAL.Entities
{
    public class StoredFileUser
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public virtual ICollection<StoredFile> SharedFiles { get; set; }
        public virtual ICollection<Token> AccessTokens { get; set; }




    }
}
