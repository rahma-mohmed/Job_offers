using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Job_offers.Models
{
    public class Account
    {
        public int Id { get; set; }

   
        [Required(ErrorMessage = "Name is required")]
        [DisplayName("أسم المستخدم")]
        public string Name {  get; set; }

      
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [DisplayName("كلمه المرور")]
        public int Password { get; set; }

        [Required(ErrorMessage = "this failed is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, try again")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\w+\@gmail.com", ErrorMessage = "invalid email, must contain @")]
        public string Email { get; set; }
        public string Account_Type { get; set; }
    }
}
