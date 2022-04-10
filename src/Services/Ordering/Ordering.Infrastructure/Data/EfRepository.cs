using Ardalis.Specification.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Interfaces;

namespace Ordering.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IAsyncRepository<T> where T : BaseEntity
{
    public EfRepository(OrderContext dbContext) : base(dbContext)
    {
    }
}