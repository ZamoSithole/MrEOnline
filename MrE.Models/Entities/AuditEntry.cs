using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace MrE.Models.Entities {
    [Table("AuditTrail")]
    public class AuditEntry {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("AuditEntryId")]
        public int Id { get; set; }
        public string UserName { get; set; }
        public Guid TransactionId { get; set; }
        public string TableName { get; set; }
        public string Operation { get; set; }
        public string NewData { get; set; }
        public string Column { get; set; }        
        public string OldData { get; set; }
        public DateTime DateCreated { get; set; }

        [NotMapped]
        public string SearchToken
        {
            get
            {
                var token = $"" +
                    $"{UserName}" +
                    $"{TableName}" +
                    $"{Operation}" +
                    $"{DateCreated}";

                return Regex.Replace(token, @"\s+", string.Empty);
            }
        }
    }
}
