using Soulgram.Interests.Application.Interfaces;
using Soulgram.Mongo.Repository;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors
{
    public class GenericRepository<TDocument> : MongoRepository<TDocument>,
        IRepository<TDocument> where TDocument : class
    {
        public GenericRepository(IMongoConnection connection)
            : base(connection)
        {
        }
    }
}
