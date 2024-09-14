using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entity.BaseEntity
{
    public class BaseEntity<TID>
    {
        [Key]
        public TID Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "";
        public bool IsDelete { get; set; }=false;
    }
}
