using Cassandra;
using Cassandra.Mapping;
using System;
using System.Collections.Generic;
namespace CassandraLogic.Helpers
{
    public class CassandraHelper : IDisposable
    {
        private ISession cassandraSession { get; set; }

        public CassandraHelper(string contactPoint, int port)
        {
            Cluster cluster = Cluster.Builder()
                    .WithPort(port)
                    .AddContactPoint(contactPoint)
                    .Build();
            cassandraSession = cluster.Connect();
        }

        public void Dispose()
        {
            if (cassandraSession != null)
            {
                cassandraSession.Dispose();
            }
        }

        public void InsertData(object objectToInsert)
        {
            Mapper mapper = new Mapper(cassandraSession);
            mapper.Insert(objectToInsert);
        }

        public IEnumerable<T> GetData<T>(string query)
        {
            Mapper mapper = new Mapper(cassandraSession);
            return mapper.Fetch<T>(query);
        }
    }
}