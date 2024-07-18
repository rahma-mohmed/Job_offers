using System.Collections;
using System.Collections.Generic;

namespace Job_offers.Models
{
    public class JobsViewModel
    {
        public string JobTitle {  get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}
