using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }

        [Display(Name ="Delivery")]
        public string DeliveryName { get; set; }

        public decimal SurfaceUnder1000 { get; set; }

        public decimal SurfaceBetween1000And2000 { get; set; }

        public decimal SurfaceOver2000 { get; set; }
    }
}
