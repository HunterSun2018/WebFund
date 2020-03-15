using System;
using System.ComponentModel.DataAnnotations;

namespace WebFund
{
    public class Fund
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public double value { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
        public double ChangeInDay { get; set; }
        public double ChangeInWeek { get; set; }
        public double ChangeInMonth { get; set; }

    }
}