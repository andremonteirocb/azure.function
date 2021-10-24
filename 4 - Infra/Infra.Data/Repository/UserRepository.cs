using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Context;

namespace Infra.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected readonly SqlContext _sqlContext;
        public UserRepository(SqlContext sqlContext)
            : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

    }
}
