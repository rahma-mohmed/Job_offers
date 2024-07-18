using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_offers.Models
{
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("اسم الوظيفه")]
        [Required(ErrorMessage = "رجاء قم بادخال اسم الوظيفه")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "رجاء قم بادخال وصف الوظيفه")]
        [DisplayName("وصف الوظيفه")]
        public string JobContent { get; set; }
        [Required(ErrorMessage = "رجاء قم بادخال صورة الوظيفه")]
        [DisplayName("صورة الوظيفه")]
        public string JobImage { get; set; }
        [ForeignKey("Category")]
        [Required(ErrorMessage = "رجاء قم بادخال نوع الوظيفه")]
        [DisplayName("نوع الوظيفه")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string UserId { get; set; }  
        public virtual Account User {  get; set; }
    }
}
