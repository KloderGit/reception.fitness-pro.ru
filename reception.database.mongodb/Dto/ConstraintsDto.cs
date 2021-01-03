using System;
using System.Collections.Generic;

namespace reception.database.mongodb.Dto
{
    public class ConstraintsDto
    {
        public IEnumerable<Guid> ProgramKeys { get; set; }
        public IEnumerable<Guid> GroupKeys { get; set; }
        public IEnumerable<Guid> SubGroupKeys { get; set; }
        public DateTime? SubscribeBefore { get; set; }
        public DateTime? UnsubscribeBefore { get; set; }
        public bool CheckContractExpired { get; set; }
    }
}