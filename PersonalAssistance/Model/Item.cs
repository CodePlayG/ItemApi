using System;
using System.ComponentModel.DataAnnotations;
using PersonalAssistance.Enum;

namespace PersonalAssistance.Model
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Description  { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public int Amount { get; set; }
        [Required]
        public Status Status { get; set; }

    }
}
