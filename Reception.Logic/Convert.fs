namespace Reception.Logic

open Reception.Domain
open System
open Dto

module Convert =

    let DatetimeToPosition (item:DateTime) : Position =
        {
            Key = Guid.NewGuid()
            IsActive = true
            DataVersion = DateTime.UtcNow.Ticks
            StudentKey = Guid.Empty
            DisciplineKey = Guid.Empty
            Result = Result.Missing
            Comment = String.Empty
            History = Seq.empty
            Time = Some (item.Hour, item.Minute)
        }

    let WithDate dt x =
        let a = match x with
                | Count cnt ->SubscribeVariant.Number (cnt, Seq.empty)
                | Free -> SubscribeVariant.Free Seq.empty
                | Times tms -> SubscribeVariant.Seating (Seq.map DatetimeToPosition tms)
        Schedule.Date (dt, a)

    let WithOutDate x = 
        let a = match x with
                | Count cnt -> NumberOrFree.Number (cnt, Seq.empty)
                | Free -> NumberOrFree.Free Seq.empty
                | Times tms -> invalidArg "Попытка создания времени без даты" "Unsupported"
        Schedule.Dateless a

    let ConstraintsDtoToModel item : Constraits = 
        {
            ProgramKeys = match item.ProgramKeys  with | Some x -> x | None -> Seq.empty
            GroupKeys = match item.GroupKeys  with | Some x -> x | None -> Seq.empty
            SubGroupKeys = match item.SubGroupKeys  with | Some x -> x | None -> Seq.empty
            CheckContractExpired = match item.CheckContractExpired with | Some x -> x | _-> false
        }

    let ReceptionDtoToModel x = 
        {
            Key = Guid.NewGuid()
            IsActive = true
            DataVersion = DateTime.UtcNow.Ticks
            CreatedAt = DateTime.UtcNow
            CreatedBy = Guid.Empty
    
            ResponsibleUserKey = match x.ResponsibleUserKey with | Some x -> x | _-> Guid.Empty 
            DisciplineKeys = match x.DisciplineKeys with | Some x -> x | _-> Seq.empty
            Constraints = 
                match x.Constraints with
                | Some x -> Some (ConstraintsDtoToModel x)
                | _ -> None
            History = Seq.empty
            Schedule =
                match x.Date with
                | Some dt -> WithDate dt.Date x.SubscribeLimit
                | None -> WithOutDate x.SubscribeLimit
        }