namespace Mongo.Model

open Domain.Domain
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes
open System
open Domain

module Dto =

    [<BsonIgnoreExtraElements>]
    type ReceptionMongoDto =
        {
            Key: Guid
            Reception: Reception
        }   
            
    [<BsonIgnoreExtraElements>]
    type PositionDto =
        {
            _id: ObjectId
            Key:Guid
            IsActive: bool
            DataVersion: int64
            Time: Option<int * int>
            StudentKey: Guid
            DisciplineKey: Guid
            Result: Result
            Comment: string
            History: seq<History>
        }     
    
        
    [<BsonIgnoreExtraElements>]
    type ReceptionDto =
        {
            _id: ObjectId
            Key:Guid
            IsActive: bool
            DataVersion: int64
            CreatedAt: DateTime
            CreatedBy: Guid
            Date: DateTime
            Count: int
            Positions: seq<Position>
            ResponsibleUserKeys: seq<Guid>
            EventKeys: seq<Guid>
            Constraints : Option<Constraints>
            History: seq<History>
        }      