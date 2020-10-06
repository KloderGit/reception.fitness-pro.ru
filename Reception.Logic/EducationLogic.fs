namespace Reception.Logic

open Reception.Domain
open Reception.Mongo.Model.Dto
open System
open Reception.Logic
open lc.fitnesspro.library
open System.Linq
open lc.fitnesspro.library.Model

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
        Seq.map (fun (x:Model.DisciplineInfo) -> {| Program = program.Title; Discipline = {| Key = x.DisciplineKey; Title = x.Discipline.Description; ControlType = x.ControlTypeKey  |} |}) arr

    //let asd (prog:Model.Program) =
    //    let disciplines = Seq.map getDiscipline prog.Disciplines
    //    let coll = 
    //        Seq.map (fun (x)->{| Key =x.Key; Title = x.Title; ControlKey =x.ControlTypeKey; Program = prog.Title |} ) disciplines
    //    coll

    type AttestationInfo = { ControlKey: Guid; ProgramTitle: string }

    type AttestationVM =
        {
            Key: Guid
            Title: string
            Info: seq<AttestationInfo>
        }

    let groupTo (group:IGrouping<_,_>) =
        {Key = group.Key.Key; Title = group.Key.Title; Info = Seq.empty}


    let Get = 
        
        let programs = GetAttestation |> Async.RunSynchronously |> Seq.cast<Program>

        let disciplines = 
            programs 
            |> Seq.map (fun prog -> Seq.map (fun (dis:DisciplineInfo) -> {| Key = dis.DisciplineKey; Title = dis.Discipline.Description|} ) prog.Disciplines)
            |> Seq.concat
            |> Seq.distinct

        let fn1 (pr:Model.Program) = 
            {| ProgramKey = pr.RefKey; ProgramTitle = pr.Description|}

        let fn1 (ds:DisciplineInfo) =
            {| DisciplineKey = ds.DisciplineKey; DisciplineTitle = ds.Discipline.Description |}

        let disInProg (gd:Guid) =
            
            let asdd dsp : bool = 
                Seq.contains  dsp

            let prs = Seq.where (fun x-> (Seq. (fun y -> y.DisciplineKey = gd) x.Disciplines) ) programs
            Seq.map (fun (x:Model.Program)-> {| Key = x.RefKey; Title = x.Description |}) prs


        Seq.map fn1 disciplines



    

