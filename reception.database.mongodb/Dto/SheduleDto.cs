using System;
using System.Collections.Generic;
using System.Text;

namespace reception.database.mongodb.Dto
{
    public abstract class SheduleDto
    {
    }

    public class Data 
        : SheduleDto
    {
        public DateTime Date { get;set;}
        public Limitation Limitation { get; set; }
    }

    public class DataLess
    : SheduleDto
    {
        public Limitation Limitation { get; set; }
    }

    public abstract class Limitation
    {
        public IEnumerable<PositionDto> Positions { get; set; }
    }

    public class Seating : Limitation
    {}

    public class Number : Limitation
    {
        public int Count { get;set; }
    }

    public class NoLimit : Limitation
    {
    }
}
