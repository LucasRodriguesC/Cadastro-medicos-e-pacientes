using System;

namespace BuiltCode.Domain.Entities
{
    public class Parceiro : BaseEntity
    {
        public string Nome { get; set; }
        public Guid ApiKey { get; set; }

        public Parceiro()
        {
            ApiKey = Guid.NewGuid();
        }
    }
}
