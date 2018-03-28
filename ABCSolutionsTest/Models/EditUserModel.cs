using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ABCSolutionsTest.Models
{
    public class EditUserModel
    {
        public EditUserModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Login = user.Login;
            EMail = user.EMail;
            IsAdmin = user.IsAdmin;
        }

        public int Id { get; set; }

        [Display(Name = "ФИО")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ФИО не должно быть пустым")]
        public string Name { get; set; }

        [Display(Name = "Логин")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Логин не должен быть пустым")]
        public string Login { get; set; }

        [Display(Name = "EMail")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        public bool IsAdmin { get; set; }
    }
}
