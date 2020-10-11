using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseWebApp.Models.EF
{
    public class LogList
    {
        [Key]
        public int Id { get; set; }
        public int UrlListId { get; set; }
        public int ResponseCode{ get; set; }
        public DateTime CreateDate{ get; set; }
    }
}
