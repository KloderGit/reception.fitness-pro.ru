namespace Reception.Test

//open System
//open Microsoft.VisualStudio.TestTools.UnitTesting
//open MongoDB.Bson
//open MongoDB.Bson.Serialization
//open NamelessInteractive.FSharp.MongoDB
//open Reception.Domain
//open Reception

//[<TestClass>]
//type TestClass () =

//    [<TestMethod>]
//    member this.TestMethodPassing () =

//        let bb = Scores.Hundred 65
//        let sdff = Scores.Hundred 65 |> Result.Rate

//        //let pos:Position =
//        //    {
//        //        Key = Guid.NewGuid()
//        //        IsActive = true
//        //        DataVersion = DateTime.Now.Ticks
//        //        Time = (10,35)
//        //        StudentKey = Guid.NewGuid()
//        //        DisciplineKey = Some Guid.NewGuid()
//        //        Result = Result.IsVisit true
//        //        Comment = Some "Test"
//        //        History = None
//        //    }

//        let con: Domain.Constraits = 
//            {
//                ProgramKeys = Some seq{ Guid.NewGuid() }
//                GroupKeys = [Guid.NewGuid()]
//                SubGroupKeys = [Guid.NewGuid()]
//                CheckContractExpired = true
//            }

//        let item : Logic.Domain.Reception  = 
//            {
//                Id = 0
//                IsActive = true
//                Date = new DateTime(2020,9,12)
//                TeacherKey = Guid.NewGuid()
//                DisciplineKeys = [Guid.NewGuid();Guid.NewGuid();Guid.NewGuid()]
//                Times = [pos]
//                Constraints = []
//            }


//        BsonSerializer.RegisterSerializationProvider(new FSharpTypeSerializationProvider());

//        let dto = Reception.Mongo.Model.Reception.Create(item)


//        let fact = Reception.Mongo.MongoDb.create(dto)


//        let result = Reception.Mongo.MongoDb.readAll



//        let json = dto.ToJson();
//        Console.WriteLine(json);
        
//        let r = BsonSerializer.Deserialize<Reception.Mongo.Model.Reception>(json);
//        Console.WriteLine(r);


//        Assert.IsTrue(true);
