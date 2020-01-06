using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionName.Models.Common
{
    public class DatabaseModel : IModel
    {
        [Column("last_modified_date")]
        public DateTime LastModifiedDate { get; set; }

        [ReadOnly(true)]
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
    }
}
