using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Csmmon.Data
{
    public class DataCollection<T>
        where T : DataModel, new()
    {
        private readonly MongoCollectionBase<T> _collection;

        public DataCollection(string name)
        {
            _collection = StorageProvider.GetMongoCollection<T>(name);
        }

        public async ValueTask InsertDocumentAsync(T document, CancellationToken cancellationToken = default)
            => await _collection.InsertOneAsync(document, cancellationToken: cancellationToken);

        public async ValueTask InsertDocumentsAsync(IEnumerable<T> documents, CancellationToken cancellationToken = default)
            => await _collection.InsertManyAsync(documents, cancellationToken: cancellationToken);

        public async ValueTask InsertOrUpdateDocumentAsync(T document, CancellationToken cancellationToken = default)
        {
            if (document.ObjectId == ObjectId.Empty)
                await _collection.InsertOneAsync(document, cancellationToken: cancellationToken);
            else
                await _collection.ReplaceOneAsync(x => x.ObjectId == document.ObjectId, document, cancellationToken: cancellationToken);
        }

        public async ValueTask<bool> UpdateDocumentAsync(T document, CancellationToken cancellationToken = default)
        {
            var entity = await (await _collection.FindAsync(x => x.ObjectId == document.ObjectId, cancellationToken: cancellationToken))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (entity is not null)
            {
                await _collection.ReplaceOneAsync(x => x.ObjectId == document.ObjectId, document, cancellationToken: cancellationToken);
                return true;
            }
            return false;
        }

        public async ValueTask<bool> ModifyDocumentAsync(T document, UpdateDefinition<T> update, CancellationToken cancellationToken = default)
            => (await _collection.UpdateOneAsync(x => x.ObjectId == document.ObjectId, update, cancellationToken: cancellationToken)).IsAcknowledged;

        public async ValueTask<bool> DeleteDocumentAsync(T document, CancellationToken cancellationToken = default)
            => (await _collection.DeleteOneAsync(x => x.ObjectId == document.ObjectId, cancellationToken: cancellationToken)).IsAcknowledged;

        public async ValueTask<bool> DeleteManyDocumentsAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
            => (await _collection.DeleteManyAsync<T>(filter, cancellationToken: cancellationToken)).IsAcknowledged;

        public async ValueTask<T> FindDocumentAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
            => await (await _collection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        public async IAsyncEnumerable<T> FindManyDocumentsAsync(Expression<Func<T, bool>> filter, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var collection = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

            foreach (var entity in collection.ToEnumerable(cancellationToken: cancellationToken))
            {
                yield return entity;
            }
        }

        public async ValueTask<T> GetFirstDocumentAsync(CancellationToken cancellationToken = default)
            => await (await _collection.FindAsync(new BsonDocument(), cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        public async IAsyncEnumerable<T> GetAllDocumentsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var collection = await _collection.FindAsync(new BsonDocument(), cancellationToken: cancellationToken);

            foreach (var entity in collection.ToEnumerable(cancellationToken: cancellationToken))
            {
                yield return entity;
            }
        }
    }
}
