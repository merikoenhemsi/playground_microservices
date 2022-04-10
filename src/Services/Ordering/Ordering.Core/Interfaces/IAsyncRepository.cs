using Ardalis.Specification;
using Ordering.Core.Entities;

namespace Ordering.Core.Interfaces;

public interface IAsyncRepository<T> : IRepositoryBase<T> where T : BaseEntity
{
}
