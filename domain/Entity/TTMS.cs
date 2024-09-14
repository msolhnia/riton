using domain.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entity
{
    public class TTMS : BaseEntity<long>
    {
        public Nullable<bool> Sarjam { get; set; }
        public Nullable<bool> IsHagholAmalKari { get; set; }
        public Nullable<int> KalaType { get; set; }
        public string KalaKhadamatName { get; set; }
        public Nullable<int> KalaCode { get; set; }
        public Nullable<bool> BargashtType { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string MaliatArzeshAfzoodeh { get; set; }
        public string AvarezArzeshAfzoodeh { get; set; }
        public decimal SayerAvarez { get; set; }
        public decimal TakhfifPrice { get; set; }
        public Nullable<int> HCKharidarTypeCode { get; set; }
        public string FactorNo { get; set; }
        public string FactorDate { get; set; }
        public Nullable<long> SanadNO { get; set; }
        public string SanadDate { get; set; }
    }
}
