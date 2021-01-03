using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using reception.database.mongodb.Dto;

namespace reception.database.mongodb
{
    public class Repository<T>
    {
        private MongoClient client;
        public string DatabaseName { get; private set; }
        public string CollectionName { get; private set; }

        public Repository(MongoClient client)
        {
            this.client = client;
            CollectionName = typeof(T).Name;
            DatabaseName = "lessonDb";

            BsonClassMap.RegisterClassMap<ReceptionDto>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }

        public Repository(string connectionString)
        : this(new MongoClient(connectionString))
        { }

        public async Task<IEnumerable<ReceptionDto>> GetAll()
        {
            var db = client.GetDatabase(DatabaseName);
            var collection = db.GetCollection<ReceptionDto>(CollectionName);

            var filte2r = new BsonDocument("$or", new BsonArray{
                 
                new BsonDocument("Age",new BsonDocument("$gte", 33)),
                new BsonDocument("Name", "Tom")
            });
            
            var filter = new BsonDocument();
            var array = await collection.Find(filter).ToListAsync();

            return array;
        }
        
        public async Task<IEnumerable<ReceptionDto>> GetTeach(Guid key)
        {
            var db = client.GetDatabase(DatabaseName);
            var collection = db.GetCollection<ReceptionDto>(CollectionName);

            var filter = new BsonDocument("ResponsibleUserKeys", key);
            
            var array = await collection.Find(filter).ToListAsync();

            return array;
        }

        public async Task Add(ReceptionDto item)
        {
            var ittt = JsonSerializer.Serialize(item);

            var db = client.GetDatabase(DatabaseName);
            var collection = db.GetCollection<ReceptionDto>(CollectionName);

            await collection.InsertOneAsync(item);
        }

        public Domain.Domain.Reception GetOne()
        {
            var constr = new Domain.Domain.Constraints(
                    programKeys: new List<Guid> { Guid.NewGuid() },
                    groupKeys: new List<Guid> { Guid.NewGuid() },
                    subGroupKeys: new List<Guid> { Guid.NewGuid() },
                    attemptsCount: new Microsoft.FSharp.Core.FSharpOption<int>(456),
                    subscribeBefore: new Microsoft.FSharp.Core.FSharpOption<DateTime>(DateTime.Now),
                    unsubscribeBefore: new Microsoft.FSharp.Core.FSharpOption<DateTime>(DateTime.Now),
                    checkContractExpired: true
                );

            var nbr = Domain.Domain.SubscribeVariant.NewNumber( new Tuple<int, IEnumerable<Domain.Domain.Position>>(12, new List<Domain.Domain.Position>()));
            var shedl = Domain.Domain.Schedule.NewDate(DateTime.Now, nbr);

            var item = new Domain.Domain.Reception(
                    key: Guid.NewGuid(),
                    isActive: true,
                    dataVersion: 245,
                    createdAt: DateTime.Now,
                    createdBy: Guid.NewGuid(),
                    schedule: shedl,
                    responsibleUserKeys: new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                    disciplineKeys: new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                    constraints: constr,
                    limits: new Domain.Domain.Limit.Free(new List<Guid>())
                    history: new List<Domain.Domain.History>()
                );

            return item;
        }

        public void CreateNew()
        {
            var limit = new NumberLmt();
            var schedule = new DataVariant(DateTime.Now, limit);

            var contr = new ConstraintsDto
            {
                ProgramKeys = new List<Guid>(){Guid.NewGuid(),Guid.NewGuid()},
                GroupKeys = new List<Guid>(){Guid.NewGuid(),Guid.NewGuid()},
                SubGroupKeys = new List<Guid>(){Guid.NewGuid(),Guid.NewGuid()},
                SubscribeBefore = DateTime.Today,
                UnsubscribeBefore = DateTime.Today,
                CheckContractExpired = false
            };
            
            var item = new ReceptionDto
            {
                Key = Guid.NewGuid(),
                IsActive = true,
                DataVersion = 65465465,
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
                Schedule = schedule,
                ResponsibleUserKeys = new List<Guid>(){Guid.NewGuid(),Guid.NewGuid()},
                DisciplineKeys = new List<Guid>(){Guid.NewGuid(),Guid.NewGuid()},
                Constraints = contr,
                History = new List<HistoryDto>(){ new HistoryDto{ Object = Guid.NewGuid(), Action = " использовал ", Subject = Guid.NewGuid(), DateTime = DateTime.Now}}
            };
            
            var db = client.GetDatabase(DatabaseName);
            var collection = db.GetCollection<ReceptionDto>(CollectionName);

            collection.InsertOneAsync(item).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}