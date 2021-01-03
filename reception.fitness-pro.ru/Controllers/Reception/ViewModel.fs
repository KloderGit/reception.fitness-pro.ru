namespace ViewModel.Reception

open System
open System.Text.Json.Serialization
open System.ComponentModel.DataAnnotations


[<JsonFSharpConverter(JsonUnionEncoding.InternalTag ||| JsonUnionEncoding.NamedFields)>]
type SubscribeLimitViewModel =
    | Count of int
    | Free
    | Times of seq<DateTime>

[<CLIMutable>]
type ConstraintsViewModel =
    {
        ProgramKeys : Option<seq<Guid>>
        GroupKeys : Option<seq<Guid>>
        SubGroupKeys : Option<seq<Guid>>
        CheckContractExpired : Option<bool>        
        AttemptsCount: Option<int>
        SubscribeBefore: Option<DateTime>
        UnsubscribeBefore: Option<DateTime>
    }

[<CLIMutable>]   
type CreateReceptionViewModel =
    {       
        Date : Option<DateTime>
        [<Required>]
        Employees : seq<Guid>
        [<Required>]
        EventKeys : seq<Guid>
        SubscribeLimit: SubscribeLimitViewModel
        Constraints: Option<ConstraintsViewModel>
    }
    
  