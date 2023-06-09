﻿namespace Waddhly.Models.UserServices
{
    public class Session
    {
        public int ID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? exactCost { get; set; }
        public bool? isPaid { get; set; }
        public virtual Proposal proposal { get; set; }
    }
}
