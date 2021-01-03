namespace Module.Reception

open System
open Domain
open Domain.Domain
open ViewModel.Reception

module Convert =

    let private DatetimeToPosition (item:DateTime) : Position =
        {
            Key = Guid.NewGuid()
            IsActive = true
            DataVersion = DateTime.UtcNow.Ticks
            StudentKey = Guid.Empty
            DisciplineKey = Guid.Empty
            Result = Result.NoResult
            Comment = String.Empty
            History = Seq.empty
            Time = Some (item.Hour, item.Minute)
        }            
            
    

    let ViewModelToModel (viewmodel:CreateReceptionViewModel) =
        {
            Key = Guid.NewGuid()
            IsActive = true
            DataVersion = DateTime.UtcNow.Ticks
            CreatedAt = DateTime.Now
            CreatedBy = Guid.Empty
            ResponsibleUserKeys = viewmodel.Employees
            Date =viewmodel.Date
            Limits = Domain.Limit.Free (Seq.empty)
            Positions = list.Empty
            DisciplineKeys = viewmodel.EventKeys
            Constraints = None
            History = Seq.empty
        }