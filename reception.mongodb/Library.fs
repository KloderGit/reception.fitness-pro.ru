namespace Mongo

open System.Collections.Generic
open Domain.Domain
open MongoDB.Bson
open MongoDB.Driver
open Domain
open Mongo.Model.Dto
open MongoDB.Bson.Serialization
open NamelessInteractive.FSharp.MongoDB
open System
open System.Linq

module Database =
    [<Literal>]
    let ConnectionString = "localhost:7500"
    
    [<Literal>]
    let DbName = "lessonDb"
    
    [<Literal>]
    let CollectionName = "Receptions"

    let client         = MongoClient("mongodb://localhost:7500")
    let db             = client.GetDatabase(DbName)
    let testCollection = 
        BsonSerializer.RegisterSerializationProvider(new FSharpTypeSerializationProvider())
        
        BsonClassMap.RegisterClassMap<Reception>(
                fun x-> x.AutoMap
                        x.SetIgnoreExtraElements true 
            )|> ignore
        
        db.GetCollection<Reception>(CollectionName)


    let ModelToDto (x:Reception) = { Key = x.Key; Reception = x }
    let DtoToModel x = x.Reception

    let Create item = item |> testCollection.InsertOne

    let ReadAll =
        testCollection.Find(Builders.Filter.Empty).ToEnumerable().ToList()

    let SearchByKey ( key : Guid ) = 
            testCollection.Find(fun x -> x.Key = key).ToEnumerable() 

    let SearchByTeacherKey ( key : Guid ) =
        let vals = seq{key}
        
        let filter = BsonDocument()
        let dic = new Dictionary<string, Object>()
        dic.Add("Reception.ResponsibleUserKey", key)
        filter.Add(dic)
        
        let filter = Builders<Reception>.Filter.Eq((fun x -> x.ResponsibleUserKeys), vals)
        //let filter = Builders<ReceptionMongoDto>.Filter.Eq("Reception.ResponsibleUserKey", key)
        let result = testCollection.Find(filter).ToList();
        result