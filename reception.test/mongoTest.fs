namespace Reception.Test

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open MongoDB.Bson
open MongoDB.Bson.Serialization
open NamelessInteractive.FSharp.MongoDB
open Domain

[<TestClass>]
type TestClass () =

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
                Schedule = Schedule.Dateless (NumberOrFree.Free Seq.empty)
                ResponsibleUserKey = [Guid.NewGuid()]
                DisciplineKeys = [Guid.NewGuid();Guid.NewGuid();Guid.NewGuid()]
                Constraints = Some con
                History = Seq.empty
            }


        BsonSerializer.RegisterSerializationProvider(new FSharpTypeSerializationProvider());

        let dto = Mongo.Database.ModelToDto(item)


        //let fact = Mongo.Database.Create(item)

        let result = Mongo.Database.ReadAll



        let json = dto.ToJson();
        Console.WriteLine(json);
        
        let r = BsonSerializer.Deserialize<Mongo.Model.Dto.ReceptionMongoDto>(json);
        Console.WriteLine(r);


        let aaa = Mongo.Database.SearchByKey(new Guid("5df2584a-bfe2-421b-a4ce-eae4fa4bb737"))

        //let bbb = Mongo.Database.SearchByTeacherKey(new Guid("f51bd392-cf87-4fa5-a386-40fa7008eafc"))

        aaa.GetEnumerator()
        //bbb.GetEnumerator()

        Assert.IsTrue(true);
