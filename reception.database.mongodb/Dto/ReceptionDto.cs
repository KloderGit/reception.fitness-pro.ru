using System;
using System.Collections.Generic;

namespace reception.database.mongodb.Dto
{
    public class ReceptionDto
    {
        public Guid Key { get; set; }
        public bool IsActive { get; set; }
        public Int64 DataVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public ScheduleDto Schedule { get; set; }
        public IEnumerable<Guid> ResponsibleUserKeys { get; set; }
        public IEnumerable<Guid> DisciplineKeys { get; set; }
        public ConstraintsDto Constraints { get; set; }
        public IEnumerable<HistoryDto> History { get; set; }
    }
}