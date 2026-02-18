using System;
using System.Collections.Generic;
using System.Text;

namespace CashBox.Core.Entities.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}