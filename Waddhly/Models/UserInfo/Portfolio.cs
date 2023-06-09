﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Waddhly.Models
{
    public class Portfolio
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] image{ get; set; }
        public string ProjectUrl { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("user")]
        public string userid { get; set; }
        public virtual User user { get; set; }
    }
}
