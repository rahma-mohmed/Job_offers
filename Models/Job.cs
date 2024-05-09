using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_offers.Models
{
    public class Job
    {
        public int Id { get; set; }
        [DisplayName("اسم الوظيفه")]
        public string JobTitle { get; set; }
        [DisplayName("وصف الوظيفه")]
        public string JobContent { get; set; }
        [DisplayName("صورة الوظيفه")]
        public string JobImage { get; set; }
        [ForeignKey("Category")]
        [DisplayName("نوع الوظيفه")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
