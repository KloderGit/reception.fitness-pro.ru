namespace Domain

open System;

module Domain =
    type Scores =
            | Five of int
            | Hundred of int
            | Passed of bool
        
    type Result = 
            | NoResult
            | Rate of Scores
            | IsApply of bool
            | IsVisit of bool
        
    type History =
        {
            Object: Guid
            Action: string
            Subject: Guid
            DateTime: DateTime
        }

    type Constraints =
        {   
            ProgramKeys: seq<Guid>
            GroupKeys: seq<Guid>
            SubGroupKeys: seq<Guid>
            AttemptsCount: Option<int>
            SubscribeBefore: Option<DateTime>
            UnsubscribeBefore: Option<DateTime>
            CheckContractExpired:bool
        }

    type Position =
        {
            Key:Guid
            IsActive: bool
            DataVersion: int64
            Time: Option<int * int>
            StudentKey: Guid
            DisciplineKey: Guid
            Result: Result
            Comment: string
            History: seq<History>
        }


    type NumberLimitation = int * seq<Position> // Generated???
    type SeatingLimitation = seq<Position>  // Predefine
    type NoLimitation = seq<Position> // Generated

    type Limit =
        | Seating of SeatingLimitation
        | Number of NumberLimitation
        | Free of NoLimitation
    
    type Reception =
        {
            Key:Guid
            IsActive: bool
            DataVersion: int64
            CreatedAt: DateTime
            CreatedBy: Guid
            ResponsibleUserKeys: seq<Guid>
            DisciplineKeys: seq<Guid>
            Constraints : Option<Constraints>
            Limits: Limit
            Date: Option<DateTime>
            Positions : list<Position>
            History: seq<History>
        }
        member this.LimitIsNumber =
            match this.Limits with
            | Number _ -> true
            | _ -> false
                               
        member this.AddPosition (item:Position) =
            let arr = this.Positions @ [item]
            { this with Positions =arr }