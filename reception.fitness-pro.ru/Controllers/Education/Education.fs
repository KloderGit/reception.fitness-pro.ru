namespace Controllers

open Microsoft.AspNetCore.Mvc
open Logic


[<ApiController>]
[<Route("[controller]")>]
type EducationController () =
    inherit ControllerBase()

    let summaries = [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]

    [<HttpGet>]
    [<Route("GetAttestation")>]
    member this.GetAl()=
        Education.Get