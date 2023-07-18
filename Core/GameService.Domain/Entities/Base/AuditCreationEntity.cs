using System;

namespace GameService.Domain.Entities.Base
{
    public class AuditCreationEntity : BaseEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public AuditCreationEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}