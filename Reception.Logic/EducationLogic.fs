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
                       .IncludeDiscipline(true)
                       .Filter(fun x->x.DeletionMark = false).And()
                       .Filter(fun x ->x.Status = "Активный").And()
                       .Filter(fun x->x.Disciplines.Any(fun y->y.ControlTypeKey = new Guid("dc91a256-8ed6-11e6-8102-10c37b94684b")))
                       //.Filter(fun x->x.Disciplines.Any(fun y->y.ControlTypeKey <> Guid.Empty))
                       .GetByFilter() |> Async.AwaitTask }

    let getDiscipline (inf:Model.DisciplineInfo) =
        {|
            Key = inf.DisciplineKey
            Title = inf.Discipline.Description
            ControlKey = inf.ControlTypeKey
        |}

    let CollectDisciplines (program: Model.Program) =
        let arr = program.Disciplines
        Seq.map (fun (x:Model.DisciplineInfo) -> {| Program = program.Title; Discipline = {| Key = x.DisciplineKey; Title = x.Discipline.Description  |}; ControlType = x.ControlTypeKey |}) arr

    //let asd (prog:Model.Program) =
    //    let disciplines = Seq.map getDiscipline prog.Disciplines
    //    let coll = 
    //        Seq.map (fun (x)->{| Key =x.Key; Title = x.Title; ControlKey =x.ControlTypeKey; Program = prog.Title |} ) disciplines
    //    coll

    let Get = 
        let accumuleted = GetAttestation |> Async.RunSynchronously |> Seq.map CollectDisciplines 
        let collected = Seq.concat accumuleted
        let grouped = collected.GroupBy(fun x->x.Discipline.Key)
        let result = 
            grouped |> 
            Seq.map (fun x-> 
                {| 
                    Key = x.Key
                    Title = x.First().Discipline.Title
                    ControlType = x.First().ControlType
                    Programs = x.Select(fun y->y.Program) 
                 |}) 
        result
        