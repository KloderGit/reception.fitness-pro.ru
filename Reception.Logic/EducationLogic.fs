namespace Reception.Logic

open Reception.Domain
open Reception.Mongo.Model.Dto
open System
open Reception.Logic
open lc.fitnesspro.library
open System.Linq

module Education =

    let client = new Manager("Kloder", "Kaligula2") 

    let GetAttestation = async {
            return! client.Program
                       .Filter(fun x->x.DeletionMark = false).And()
                       .Filter(fun x ->x.Status = "Активный").And()
                       //.Filter(fun x->x.Disciplines.Any(fun y->y.ControlTypeKey = new Guid("f4190a1a-4cdc-11e6-a716-c8600054f636")))
                       .Filter(fun x->x.Disciplines.Any(fun y->y.ControlTypeKey <> Guid.Empty))
                       .GetByFilter() |> Async.AwaitTask }

    let CollectDisciplines (program: Model.Program) =
        let arr = program.Disciplines        
        Seq.map (fun (x:Model.Discipline) -> {| Program = program.Title; DisciplineKey = x.DisciplineKey|}) arr                   

    let Get = 
        let accumuleted = GetAttestation |> Async.RunSynchronously |> Seq.map CollectDisciplines 
        let collected = Seq.concat accumuleted
        let grouped = collected.GroupBy(fun x->x.DisciplineKey)
        let result = grouped |> Seq.map (fun x-> {| Discipline = x.Key; Programs = x.Select(fun y->y.Program) |}) 
        result
        