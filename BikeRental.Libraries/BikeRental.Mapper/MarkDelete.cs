using BikeRental.Entities;

namespace BikeRental.Mapper
{
    public class MarkDelete<T> where T : BaseEntity
    {
        public T Mark(T entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedDate = DateTime.UtcNow;
            return entity;
        }
    }
}
