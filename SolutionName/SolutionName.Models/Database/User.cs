using Dapper;
using SolutionName.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionName.Models.Database
{
    [Table("users")]
    public class User : DatabaseModel
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("firebase_id")]
        public string FirebaseId { get; set; }

        [IgnoreInsert]
        [Column("last_login_date")]
        public DateTime LastLoginDate { get; set; }
    }
}
