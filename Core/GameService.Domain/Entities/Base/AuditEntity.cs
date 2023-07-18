using System;

namespace GameService.Domain.Entities.Base
{
    public class AuditEntity : AuditCreationEntity
    {
        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        public AuditEntity()
        {
            ModifiedAt = DateTime.UtcNow;
        }
    }
}