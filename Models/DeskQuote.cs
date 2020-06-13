using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{

    public class DeskQuote
    {
            public DeskQuote()
        {
        }

        private int[,] _rushOrderPrices;

        //200 is the starting amount for a desk
        private const decimal BASE_PRICE = 200.00M;

        //Basic Costs
        private const decimal SURFACE_AREA_COST = 1.00M;
        private const decimal DRAWER_COST = 50.00M;

        // Material Costs
        private const decimal OAK_COST = 200.00M;
        private const decimal LAMINATE_COST = 100.00M;
        private const decimal PINE_COST = 50.00M;
        private const decimal ROSEWOOD_COST = 300.00M;
        private const decimal VENEER_COST = 125.00M;
        //constants
        
        

        public int DeskQuoteId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name ="Quote date")]
        public DateTime QuoteDate { get; set; }

        [Display(Name = "Quote Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal QuotePrice { get; set; }

        public int DeskId { get; set; }

        [Display(Name ="Delivery Type")]
        public int DeliveryTypeId { get; set; }

        //Navigation
        public Desk Desk { get; set; }

        public Delivery DeliveryType { get; set; }


        public decimal GetQuote(MegaDeskRazorContext context)
        {
            var surfaceArea = this.Desk.Depth * this.Desk.Width;
            decimal quotePrice = BASE_PRICE;
            decimal surfacePrice = 0;

            if (surfaceArea > 1000)
            {
                surfacePrice = surfaceArea * SURFACE_AREA_COST;
            }

            decimal drawerPrice = this.Desk.Drawers * DRAWER_COST;

            decimal surfaceMaterialPrice = 0.00M;

            var surfaceMaterialPrices = context.DesktopMaterial
                .Where(d => d.DesktopMaterialId == this.Desk.DesktopMaterialId).FirstOrDefault();

            surfaceMaterialPrice = surfaceMaterialPrices.Cost;
            /*switch (this.Desk.DesktopMaterial)
            {
                case "Laminate":
                    surfaceMaterialPrice = LAMINATE_COST;
                    break;
                case DesktopMaterial.Oak:
                    surfaceMaterialPrice = OAK_COST;
                    break;
                case DesktopMaterial.Pine:
                    surfaceMaterialPrice = PINE_COST;
                    break;
                case DesktopMaterial.Rosewood:
                    surfaceMaterialPrice = ROSEWOOD_COST;
                    break;
                case DesktopMaterial.Veneer:
                    surfaceMaterialPrice = VENEER_COST;
                    break;

            }*/
            decimal deliveryPrice = 0.00M;

            var deliveryPrices = context.Delivery
                .Where(d => d.DeliveryId == this.DeliveryTypeId).FirstOrDefault();

            var deliveryType = deliveryPrices.DeliveryName;

            switch (deliveryType)
            {
                case "No Rush":
                    if (surfaceArea < 1000)
                    {
                        deliveryPrice = deliveryPrices.SurfaceUnder1000;
                    }
                    else if (surfaceArea <= 1000)
                    {
                        deliveryPrice = deliveryPrices.SurfaceBetween1000And2000;
                    }
                    else
                    {
                        deliveryPrice = deliveryPrices.SurfaceOver2000;
                    }
                    break;

                case "Rush 3 Day":
                    if (surfaceArea < 1000)
                    {
                        deliveryPrice = deliveryPrices.SurfaceUnder1000;
                    }
                    else if (surfaceArea <= 1000)
                    {
                        deliveryPrice = deliveryPrices.SurfaceBetween1000And2000;
                    }
                    else
                    {
                        deliveryPrice = deliveryPrices.SurfaceOver2000;
                    }
                    break;

                case "Rush 5 Day":
                    if (surfaceArea < 1000)
                    {
                        deliveryPrice = deliveryPrices.SurfaceUnder1000;
                    }
                    else if (surfaceArea <= 1000)
                    {
                        deliveryPrice = deliveryPrices.SurfaceBetween1000And2000;
                    }
                    else
                    {
                        deliveryPrice = deliveryPrices.SurfaceOver2000;
                    }
                    break;
                case "Rush 7 Day":
                    if (surfaceArea < 1000)
                    {
                        deliveryPrice = deliveryPrices.SurfaceUnder1000;
                    }
                    else if (surfaceArea <= 1000)
                    {
                        deliveryPrice = deliveryPrices.SurfaceBetween1000And2000;
                    }
                    else
                    {
                        deliveryPrice = deliveryPrices.SurfaceOver2000;
                    }
                    break;




            }
            quotePrice = quotePrice + surfacePrice + drawerPrice + surfaceMaterialPrice + deliveryPrice;

            return quotePrice;
        }
        private void getRushOrderPrices()
        {
            _rushOrderPrices = new int[3, 3];
            var pricesFile = @"_rushOrderPrices.txt";

            string[] prices = File.ReadAllLines(pricesFile);
            int i = 0, j = 0;

            foreach (string price in prices)
            {
                _rushOrderPrices[i, j] = int.Parse(price);

                if (j == 2)
                {
                    i++;
                    j = 0;
                }
                else
                {
                    j++;
                }
            }
        }
    }
}
