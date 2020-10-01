namespace Reception.Mongo.Model

open Reception.Domain
open MongoDB.Bson.Serialization.Attributes
open System

module Dto =

    [<BsonIgnoreExtraElements>]
    type ReceptionMongoDto =
        {
            Key: Guid
            Reception: Reception
        }

