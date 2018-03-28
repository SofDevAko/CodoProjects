using System;

namespace spookify.Models
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}