using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABCSolutionsTest.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime? Time { get; set; }

        [StringLength(2000)]
        [Display(Name = "Текст сообщения")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Адресат")]
        public int UserID { get; set; }

        [Display(Name = "Автор")]
        public int AuthorID { get; set; }

        [Display(Name = "Адресат")]
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [Display(Name = "Автор")]
        [ForeignKey("AuthorID")]
        public virtual User Author { get; set; }

    }
}
