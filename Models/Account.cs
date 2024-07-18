using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Job_offers.Models
{
    public class Account
    {
        public string Id { get; set; }

        [DisplayName("أسم المستخدم")]
        [Required(ErrorMessage = "رجاء قم بادخال الاسم")]
        public string Name {  get; set; }


        [DataType(DataType.Password)]
        [DisplayName("كلمه المرور")]
        [Required(ErrorMessage = "رجاء قم بادخال كلمه المرور")]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [DisplayName("تأكيد كلمه المرور")]
        [Compare("Password", ErrorMessage = "غير مطابقه لكلمه المرور حاول مره اخرى")]
        [Required(ErrorMessage = "رجاء قم بادخال تاكيد كلمه المرور")]
        public string RePassword { get; set; }


        [DataType(DataType.EmailAddress , ErrorMessage = "@ خطأ فى الايميل يجب ان يحتوي ")]
        [Required(ErrorMessage = "رجاء قم بادخال الايميل")]
        public string Email { get; set; }

        public string Account_Type { get; set; }

        [DisplayName("تذكرني")]
        public bool IsPresistent { get; set; }

        public virtual ICollection<Job> jobs { get; set; }
    }
}
