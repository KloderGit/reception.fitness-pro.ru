using System;
using System.Collections.Generic;
using System.Text;

namespace reception.database.mongodb.Dto
{
    public enum LimitType
    {
        Seating,
        Number,
        Free
    }

    public enum VariantType
    {
        Date,
        Dateless
    }

    public class ScheduleDto
    {
        public LimitType LimitType { get; set; }
        public VariantType VariantType { get; set; }
        public Limit Limit { get; set; }

        public IEnumerable<PositionDto> Positions { get; set; }

        public ScheduleDto(VariantType variant, LimitType limit)
        {
            LimitType = limit;
            VariantType = variant;
        }
    }

    public class DataVariant : ScheduleDto
    {
        public DateTime Date { get; set; }

        public DataVariant(DateTime date, Limit limitValue)
            : base(VariantType.Date, limitValue.LimitType)
        {
            Date = date;
            Limit = limitValue;
        }
    }
    public class DataLessVariant : ScheduleDto
    {
        public DataLessVariant(Limit limitValue)
            : base(VariantType.Dateless, limitValue.LimitType)
        {
            Limit = limitValue;
        }
    }

    public class Limit
    {
        public virtual LimitType LimitType { get; set; }
    }

    public class NumberLmt : Limit
    {
        public override LimitType LimitType { get; set; } = Dto.LimitType.Number;
        public int Count { get; set; }
    }

    public class SeatingLmt: Limit
    {
        public override LimitType LimitType { get; set; } = Dto.LimitType.Seating;
    }

    public class FreeLmt: Limit
    {
        public override LimitType LimitType { get; set; } = Dto.LimitType.Free;
    }
}