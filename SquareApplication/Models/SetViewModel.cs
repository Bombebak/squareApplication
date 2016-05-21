using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SquareApplication.Models
{
    public class SetViewModel
    {
    }

    public class CreateSetViewModel
    {
        public int UserId { get; set; }

        public int SetId { get; set; }

        [Required]
        [Display(Name="Title")]
        public string SetTitle { get; set; }

        public string TileUrl { get; set; }

        public string Images { get; set; }

        [Required]
        [Display(Name = "Price for set")]
        [DataType(DataType.Currency)]
        public int SetPrice { get; set; }

        public DateTime Registered { get; set; }

    }
}