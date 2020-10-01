namespace Reception.Logic

open System

module Dto =

    type SubscribeLimitDto =
        | Count of int
        | Free
        | Times of seq<DateTime>

    type ConstraintsDto =
        {
            ProgramKeys : Option<seq<Guid>>
            GroupKeys : Option<seq<Guid>>
            SubGroupKeys : Option<seq<Guid>>
            CheckContractExpired : Option<bool>
        }

    type ReceptionDto =
        {
            ResponsibleUserKey : Option<Guid>
            DisciplineKeys : Option<seq<Guid>>
            Date: Option<DateTime>
            SubscribeLimit: SubscribeLimitDto
            Constraints: Option<ConstraintsDto>
        }