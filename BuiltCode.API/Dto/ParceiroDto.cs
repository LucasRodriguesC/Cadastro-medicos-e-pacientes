using System;

namespace BuiltCode.API.Dto
{
    public class ParceiroDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid ApiKey { get; set; }
    }
}
