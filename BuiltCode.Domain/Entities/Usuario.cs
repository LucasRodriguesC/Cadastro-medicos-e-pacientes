namespace BuiltCode.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public EPerfil Perfil { get; set; }
    }
}
