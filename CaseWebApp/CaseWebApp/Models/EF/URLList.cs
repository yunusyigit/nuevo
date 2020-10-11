using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseWebApp.Models.EF
{
    public class URLList
    {
        [Key]
        public int Id { get; set; }

        public string URL{ get; set; }
        public string WebName{ get; set; }
        public int Time{ get; set; }
        public int CreateAddById{ get; set; }
        public DateTime CreateDate{ get; set; }
        public bool IsActive{ get; set; }
        public DateTime LastCheck { get; set; }
    }
}
