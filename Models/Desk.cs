using MegaDeskRazor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{ 
    public class Desk
    {

       public int DeskId { get; set; }

        public decimal Width { get; set; }

        public decimal Depth { get; set; }

        [Display(Name ="Number of Drawers")]
        public int Drawers { get; set; }

        [Display(Name = "Desktop Material")]
        public int DesktopMaterialId { get; set; }

        //Navigation
        public DesktopMaterial DesktopMaterial { get; set; }
    }
}
