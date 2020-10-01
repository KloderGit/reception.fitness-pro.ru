namespace Reception.Domain

open System;

type Scores =
        | Five of int
        | Hundred of int
        | Passed of bool
    
type Result = 
        | Missing
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

type Constraits =
    {   
        ProgramKeys: seq<Guid>
        GroupKeys: seq<Guid>
        SubGroupKeys: seq<Guid>
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


type NumberLimitation = int * seq<Position> // Generated
type SeatingLimitation = seq<Position>  // Predefine
type NoLimitation = seq<Position>

type NumberOrFree =
    | Number of NumberLimitation
    | Free of NoLimitation

type SubscribeVariant =
    | Seating of SeatingLimitation
    | Number of NumberLimitation
    | Free of NoLimitation

type Schedule =
    |   Date of DateTime * SubscribeVariant
    |   Dateless of NumberOrFree

type Reception =
        {
            Key:Guid
            IsActive: bool
            DataVersion: int64
            CreatedAt: DateTime
            CreatedBy: Guid
            Schedule : Schedule      
            ResponsibleUserKey: Guid
            DisciplineKeys: seq<Guid>
            Constraints : Option<Constraits>
            History: seq<History>
        }