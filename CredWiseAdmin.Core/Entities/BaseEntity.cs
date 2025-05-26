using System;

namespace CredWiseAdmin.Core.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public virtual void UpdateAuditFields(string modifiedBy)
        {
            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }

        public virtual void Deactivate(string modifiedBy)
        {
            IsActive = false;
            UpdateAuditFields(modifiedBy);
        }

        public virtual void Activate(string modifiedBy)
        {
            IsActive = true;
            UpdateAuditFields(modifiedBy);
        }
    }
} 