using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCSolutionsTest.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime? Time { get; set; }
        [StringLength(2000)]
        public string Text { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }

    }
}
