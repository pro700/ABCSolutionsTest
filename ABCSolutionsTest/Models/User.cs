using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCSolutionsTest.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
