using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Job_offers.Models
{
    public class EditAccountViewModel
    {
            public string Id { get; set; }

            [DisplayName("أسم المستخدم")]
            [Required(ErrorMessage = "رجاء قم بادخال الاسم")]
            public string Name { get; set; }

            [DataType(DataType.Password)]
            [DisplayName("كلمه المرور الحالية ")]
            [Required(ErrorMessage = "رجاء قم بادخال كلمه المرور")]
            public string currentPassword { get; set; }

            [DataType(DataType.Password)]
            [DisplayName("كلمه المرور الجديدة ")]
            [Required(ErrorMessage = "رجاء قم بادخال كلمه المرور")]
            public string NewPassword { get; set; }


            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "غير مطابقه لكلمه المرور حاول مره اخرى")]
            [Required(ErrorMessage = "رجاء قم بادخال تاكيد كلمه المرور الجديدة")]
            [DisplayName("تأكيد كلمه المرور")]
            public string RePassword { get; set; }


            [DataType(DataType.EmailAddress, ErrorMessage = "@ خطأ فى الايميل يجب ان يحتوي ")]
            [Required(ErrorMessage = "رجاء قم بادخال الايميل")]
            public string Email { get; set; }

    }
}
