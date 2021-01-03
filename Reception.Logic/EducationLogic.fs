namespace Logic

open System
open lc.fitnesspro.library
open System.Linq

module Education =

    let client = new Manager("Kloder", "Kaligula2") 

    let GetAttestation = async {
            return! client.Program
                       .IncludeDiscipline(true)
                       .Filter(fun x->x.DeletionMark = false).And()
                       .Filter(fun x ->x.Status = "Активный").And()
                       //.Filter(fun x-> (x.Disciplines |> Seq.exists (fun y->y.ControlTypeKey = new Guid("f4190a1a-4cdc-11e6-a716-c8600054f636") ) ))
                       //.Filter(fun x->x.Disciplines.Any(fun y->y.ControlTypeKey = new Guid("f4190a1a-4cdc-11e6-a716-c8600054f636")))
                       .Filter(fun x->x.Disciplines.Any(fun y->y.ControlTypeKey <> Guid.Empty))
                       .GetByFilter() |> Async.AwaitTask }

    let getTestType = async {
            return! client.Control
                       .GetByFilter() |> Async.AwaitTask }        


    type KeyTitle = {Key: Guid; Title: string}

    type AttestationMap =
        {
            Key: Guid
            Title: string
            Control: Guid
        }

    type ProgramMap =
        {
            Key: Guid
            Title: string            
            Attestation : seq<AttestationMap>
        }

    type AttestationResult =
        {
            Key: Guid
            Title: string
            Control: KeyTitle
            Programs: seq<KeyTitle>
        }

    let programMap =
        let programs = GetAttestation |> Async.RunSynchronously   
        programs |> Seq.map (fun x -> { Key = x.RefKey; Title = x.Description; Attestation = Seq.map (fun (y:Model.DisciplineInfo) -> { Key = y.DisciplineKey; Title = y.Discipline.Description; Control = y.ControlTypeKey }) x.Disciplines })

    let testMap =
        let tests = getTestType |> Async.RunSynchronously
        tests |> Seq.map (fun x->{ Key = x.Key; Title = x.Description })

    let arrContains arr a = arr |> Seq.contains a
    let progsContainig arr g = arr |> Seq.filter (fun x-> arrContains x.Attestation g)

    let getTestbyGuid gd = testMap |> Seq.filter (fun x-> x.Key = gd) |> Seq.head

    let Get = 
        let programs = programMap

        let attestationMap = programs |> Seq.map (fun x-> x.Attestation) |> Seq.concat |> Seq.distinct

        let res = 
            let toProgram (x:ProgramMap) : KeyTitle = { Key= x.Key; Title = x.Title }
            let toAttestat (a:AttestationMap) (d:seq<ProgramMap>) =
                {   Key=a.Key ; Title = a.Title; 
                    Control = getTestbyGuid a.Control; 
                    Programs =
                        let pr = progsContainig d a
                        Seq.map (fun x-> toProgram x) pr
                }        
            attestationMap |> Seq.map (fun x-> toAttestat x programs)
        res


