namespace Controllers

open Microsoft.AspNetCore.Mvc
open Logic
open Module.Reception
open ViewModel.Reception
open System

[<ApiController>]
[<Route("[controller]")>]
type ReceptionController () =
    inherit ControllerBase()

    [<HttpPost>]
    [<Route("Create")>]
    member this.Create(viewmodel)=
        let model = viewmodel |> Convert.ViewModelToModel 
        Logic.Reception.CreateReception model

        Reception.GetAll

    [<HttpGet>]
    [<Route("GetAll")>]
    member this.GetAl()=
        Reception.GetAll

    [<HttpGet>]
    [<Route("CreateVM")>]
    member this.CreateVM()=        
        let vm =
            {
                CreateReceptionViewModel.Date = Some (DateTime.Now)
                Employees = seq{Guid.NewGuid()}
                EventKeys = seq{Guid.NewGuid()}
                SubscribeLimit = SubscribeLimitViewModel.Count 45
                Constraints = None
            }
        vm