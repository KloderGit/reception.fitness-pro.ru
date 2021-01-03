using System;
using System.Collections.Generic;
using System.Text;

namespace reception.database.mongodb.Dto
{
    public enum ResultVariant
    {
        NoResult,
        Rate,
        IsApply,
        IsVisit
    }

    public enum ResultType
    {
        Five,
        Hundred,
        Passed
    }

    public class PositionDto
    {
        public Guid Key { get; set; }
        public bool IsActive { get; set; }
        public Int64 DataVersion { get; set; }
        public Time Time { get; set; }
        public Guid StudentKey { get; set; }
        public Guid DisciplineKey { get; set; }
        public Result Result { get; set; }
        public string Comment { get; set; }
        public IEnumerable<HistoryDto> History { get; set; }
    }

    public class Result
    {
        public ResultVariant ResultVariant { get; set; }

        public Result(ResultVariant resultVariant)
        {
            ResultVariant = resultVariant;
        }
    }

    public class Rate : Result
    {
        public ResultType ResultTypeType { get; set; }

        public Rate(ResultType resultType)
            : base(ResultVariant.Rate)
        {
            ResultTypeType = resultType;
        }
    }

    public class Five : Rate
    {
        public int Value { get; set; }
        public Five() :base(Dto.ResultType.Five)
        {}
    }

    public class Hundred : Rate
    {
        public int Value { get; set; }
        public Hundred() :base(Dto.ResultType.Hundred)
        {}
    }

    public class Passed : Rate
    {
        public bool Value { get; set; }
        public Passed() :base(Dto.ResultType.Passed)
        {}
    }
    
    public class IsApply : Result
    {
        public bool Value { get; set; }
        public IsApply() 
            : base(ResultVariant.IsApply)
        {}
    }
    
    public class IsVisit : Result
    {
        public bool Value { get; set; }
        public IsVisit() 
            : base(ResultVariant.IsVisit)
        {}
    }
    
    public class NoResult : Result
    {
        public NoResult() 
            : base(ResultVariant.NoResult)
        {}
    }

    public class Time
    {
        public int Hour { get; set; }
        public int Minutes { get; set; }
    }
}