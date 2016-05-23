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

        public int TagId { get; set; }

        [Required]
        [Display(Name="Title")]
        public string SetTitle { get; set; }

        public string TileUrl { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Images { get; set; }

        [Required]
        [Display(Name = "Add tags")]
        public string TagTitle { get; set; }

        [Required]
        [Display(Name = "Price for set")]
        [DataType(DataType.Currency)]
        public int SetPrice { get; set; }

        public DateTime Registered { get; set; }

    }

    public class DetailsSetViewModel
    {
        public int SetId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Title")]
        public string SetTitle { get; set; }

        [Display(Name = "Tags")]
        public string TagTitle { get; set; }

        [Display(Name = "Tags")]
        public List<TagsViewModel> SetTagsList { get; set; }

        public DateTime UploadedTime { get; set; }

        [Display(Name = "Price for set")]
        [DataType(DataType.Currency)]
        public int SetCost { get; set; }

        public IEnumerable<DetailsSetViewModel> Tiles { get; set; } 
    }

    public class EditSetViewModel
    {
        public int SetId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Title")]
        public string SetTitle { get; set; }

        [Display(Name = "Tags")]
        public string TagTitle { get; set; }

        [Display(Name = "Tags")]
        public List<TagsViewModel> SetTagsList { get; set; }

        public DateTime UploadedTime { get; set; }

        [Display(Name = "Price for set")]
        [DataType(DataType.Currency)]
        public int SetCost { get; set; }
    }

    public class DeleteSetViewModel
    {
        public int SetId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Title")]
        public string SetTitle { get; set; }

        public int TileCount { get; set; }
    }
}