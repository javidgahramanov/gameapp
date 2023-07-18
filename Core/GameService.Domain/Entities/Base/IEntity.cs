using System;

namespace GameService.Domain.Entities.Base
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}