namespace Reception.Logic

open Reception.Domain
open Reception.Mongo.Model.Dto
open System
open Reception.Logic

module Logic =

    let CreateReception dto =
        let model = Convert.ReceptionDtoToModel dto
        Reception.Mongo.MongoDb.Create model

    let GetAll = Reception.Mongo.MongoDb.ReadAll