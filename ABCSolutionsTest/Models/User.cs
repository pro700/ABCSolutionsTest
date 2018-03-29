using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCSolutionsTest.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "ФИО")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ФИО не должно быть пустым")]
        public string Name { get; set; }

        [Display(Name = "Логин")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Логин не должен быть пустым")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Задайте минимум 3 символа")]
        public string Password { get; set; }

        [Display(Name = "EMail")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> AuthorMessages { get; set; }
    }
}
