using System;

namespace GameService.Domain.Entities.Base
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
