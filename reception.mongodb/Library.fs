namespace Reception.Mongo

open MongoDB.Driver
open Reception.Domain
open Reception.Mongo.Model.Dto
open MongoDB.Bson.Serialization
open NamelessInteractive.FSharp.MongoDB

module MongoDb =
    [<Literal>]
    let ConnectionString = "localhost:7010"
    
    [<Literal>]
    let DbName = "lessonDb"
    
    [<Literal>]
    let CollectionName = "Receptions"

    let client         = MongoClient("mongodb://localhost:7010")
    let db             = client.GetDatabase(DbName)
    let testCollection = 
        BsonSerializer.RegisterSerializationProvider(new FSharpTypeSerializationProvider());    
        db.GetCollection<ReceptionMongoDto>(CollectionName)


    let ModelToDto (x:Reception) = { Key = x.Key; Reception = x }
    let DtoToModel x = x.Reception

    let Create item = item |> ModelToDto |> testCollection.InsertOne

    let ReadAll =
        let result = testCollection.Find(Builders.Filter.Empty).ToEnumerable()
        Seq.map DtoToModel result