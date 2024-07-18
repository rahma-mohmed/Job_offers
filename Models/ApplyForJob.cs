using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_offers.Models
{
    // m:m job , users
    public class ApplyForJob
    {
        public int Id { get; set; }
        [DisplayName("نص الرسالة")]
        public string Message { get; set; }
        [DisplayName("تاريخ التقديم")]
        public DateTime ApplyDate { get; set; }
        //lazy loading
        [ForeignKey("job")]
        public int JobId { get; set; }
        public virtual Job job { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual Account User { get; set; }

        public string CVFileName { get; set; }

        [DisplayName("السيرة الذاتية")]
        [NotMapped]
        public IFormFile CVFile { get; set; }
    }
}
