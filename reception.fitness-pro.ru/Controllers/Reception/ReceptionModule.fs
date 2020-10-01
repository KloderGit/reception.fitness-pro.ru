namespace Reception.Module

open Reception.ViewModel
open Reception.Logic.Dto

module Convert =

    let private ConvertConstrait (x : ConstraintsViewModel) =
            {
                ProgramKeys = x.ProgramKeys
                GroupKeys = x.GroupKeys
                SubGroupKeys = x.SubGroupKeys
                CheckContractExpired =  x.CheckContractExpired
            }

    let ViewmodelToDto (item) =
            {
                ResponsibleUserKey = item.TeacherKey
                DisciplineKeys = Some item.DisciplineKeys.Value
                Date = item.Date
                SubscribeLimit = 
                    match item.SubscribeLimit with
                    | SubscribeLimitViewModel.Count cnt -> SubscribeLimitDto.Count cnt
                    | SubscribeLimitViewModel.Free -> SubscribeLimitDto.Free
                    | SubscribeLimitViewModel.Times tms -> SubscribeLimitDto.Times tms
                Constraints = 
                    match item.Constraints with
                    | None -> None
                    | Some x -> Some (ConvertConstrait x)
            }