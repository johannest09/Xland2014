using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Xland.Models
{
    public class Project
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Resources.Resources))]
        public ProjectType? ProjectType { get; set; }

        [Display(Name = "ContactPerson", ResourceType = typeof(Resources.Resources))]
        public string ContactPerson { get; set; }

        [Display(Name = "ProjectStarted", ResourceType = typeof(Resources.Resources)), DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/yyyy}")]
        public DateTime ProjectBeginDate { get; set; }

        [Display(Name = "ProjectFinished", ResourceType = typeof(Resources.Resources)), DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/yyyy}")]
        public DateTime ProjectEndDate { get; set; }

        [Display(Name = "ProjectStatus", ResourceType = typeof(Resources.Resources))]
        public bool InProgress { get; set; }

        [Display(Name = "CapitalCost", ResourceType = typeof(Resources.Resources))]
        public string CapitalCost { get; set; }

        [Display(Name = "Designers", ResourceType = typeof(Resources.Resources))]
        public string Designers { get; set; }

        [Display(Name = "Affiliations", ResourceType = typeof(Resources.Resources))]
        public string Affiliates { get; set; }

        [Display(Name = "ProjectOwner", ResourceType = typeof(Resources.Resources))]
        public string ProjectOwner { get; set; }

        [Display(Name = "Contractor", ResourceType = typeof(Resources.Resources))]
        public string Contractor { get; set; }

        [Display(Name = "AreaSize", ResourceType = typeof(Resources.Resources))]
        public string AreaSize { get; set; }

        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(Resources.Resources))]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "DescriptionEnglish", ResourceType = typeof(Resources.Resources))]
        public string DescriptionEnglish { get; set; }

        [Display(Name = "Latitude", ResourceType = typeof(Resources.Resources))]
        public string Lat { get; set; }

        [Display(Name = "Longitude", ResourceType = typeof(Resources.Resources))]
        public string Long { get; set; }

        [Display(Name = "ProjectLocation", ResourceType = typeof(Resources.Resources))]
        public string ProjectLocation { get; set; }

        [Display(Name = "Locality", ResourceType = typeof(Resources.Resources))]
        public string Locality { get; set; }

        [Display(Name = "IsVisible", ResourceType = typeof(Resources.Resources))]
        public bool IsVisible { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual ICollection<Studio> Studios { get; set; }
    }
        
    public enum ProjectType
    {
        PublicSpaces, 
        Historical, 
        Competitions, 
        SpatialPlanning
    }

}