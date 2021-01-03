namespace Reception.Test

open System
open Domain.Domain
open Microsoft.VisualStudio.TestTools.UnitTesting
open MongoDB.Bson
open MongoDB.Bson.Serialization
open NamelessInteractive.FSharp.MongoDB
open Domain
open reception.database.mongodb

[<TestClass>]
type TestClass2 () =

    [<TestMethod>]
    member this.TestMethodPassing () =

        let bb = Scores.Hundred 65
        let sdff = Scores.Hundred 65 |> Result.Rate

        let pos:Position =
            {
                Key = Guid.NewGuid()
                IsActive = true
                DataVersion = DateTime.Now.Ticks
                Time = Some (10,35)
                StudentKey = Guid.NewGuid()
                DisciplineKey = Guid.NewGuid()
                Result = Result.IsVisit true
                Comment = "Test"
                History = Seq.empty
            }
        let pos1 =
           {
                Key = Guid.NewGuid()
                IsActive = true
                DataVersion = DateTime.Now.Ticks
                Time = Some (10,35)
                StudentKey = Guid.NewGuid()
                DisciplineKey = Guid.NewGuid()
                Result = Result.IsVisit true
                Comment = "Test2222"
                History = Seq.empty
            }

        let con: Domain.Constraints = 
            {
                ProgramKeys = [Guid.NewGuid()]
                GroupKeys = [Guid.NewGuid()]
                SubGroupKeys = [Guid.NewGuid()]
                CheckContractExpired = true
                AttemptsCount = Some 45
                SubscribeBefore = Some DateTime.Now
                UnsubscribeBefore = Some DateTime.Now
            }

        let item : Domain.Reception  = 
            {
                Key = Guid.NewGuid()
                IsActive = true
                DataVersion = 86L
                CreatedAt = DateTime.Now
                CreatedBy = Guid.NewGuid()
                ResponsibleUserKeys = [Guid.NewGuid()]
                DisciplineKeys = [Guid.NewGuid();Guid.NewGuid();Guid.NewGuid()]
                Constraints = Some con
                History = Seq.empty
                Limits = Domain.Limit.Free (Seq.empty)
                Date = Some DateTime.Now
                Positions = list.Empty
            }
        

        
        let sdfdd = item.AddPosition pos1
        Assert.IsTrue(true);
