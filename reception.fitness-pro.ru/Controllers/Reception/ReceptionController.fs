namespace Reception.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Reception
open Reception.Logic
open Reception.Module

[<ApiController>]
[<Route("[controller]")>]
type ReceptionController () =
    inherit ControllerBase()

    [<HttpPost>]
    [<Route("Create")>]
    member this.Create(viewmodel)=
         //let asd = System.Text.Json.JsonSerializer.Serialize(x)
         //let dds = System.Text.Json.JsonSerializer.Deserialize<ReceptionDes>(asd)
 
        let dto = viewmodel |> Convert.ViewmodelToDto 
        Reception.CreateReception dto

        Reception.GetAll

    [<HttpGet>]
    [<Route("GetAll")>]
    member this.GetAl()=
        Reception.GetAll