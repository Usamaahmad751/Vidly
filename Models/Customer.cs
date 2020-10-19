using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.ViewModel;
using System.ComponentModel.DataAnnotations;
namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name="Date Of Birth")]
        [DataType("Date")]
        public DateTime? BirthDate{get; set;} 

        public bool IsSubscribedToNewsLetter { get; set; }
        
        public MemberShipType MemberShipType { get; set; }

        [Display(Name="MemberShip Type")]
        public byte MemberShipTypeId { get; set; }
    }
}