namespace Logic

module Reception =

    let CreateReception model =
        Mongo.Database.Create model

    let GetAll = Mongo.Database.ReadAll