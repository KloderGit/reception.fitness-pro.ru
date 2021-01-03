using System;
using System.Collections.Generic;
using System.Text;

namespace reception.database.mongodb.Dto
{
    public class PositionDto
    {
        public Result Result { get; set; }
    }

    public abstract class Result
    {
        public virtual string Type { get; set; }
    }

    public abstract class Rate : Result
    { }

    public class Five : Rate {  }
    public class Hundred : Rate { }
    public class Passed : Rate { }
}
