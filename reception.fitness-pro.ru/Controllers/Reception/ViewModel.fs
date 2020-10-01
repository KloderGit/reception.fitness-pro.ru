namespace Reception.ViewModel

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
    }

[<CLIMutable>]   
type CreateReceptionViewModel =
    {
        Date : Option<DateTime>
        TeacherKey : Option<Guid>
        [<Required>]DisciplineKeys : Option<seq<Guid>>
        SubscribeLimit: SubscribeLimitViewModel
        Constraints: Option<ConstraintsViewModel>
    }