using Errors = System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>;

namespace Desafio_Balta_IBGE.Shared.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public bool IsValid { get; private set; }
        public Errors Errors { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            Errors = new Errors();
            IsValid = true;
        }

        protected void AddNotification(Errors errors)
        {
            IsValid = false;
            Errors = errors;
        }
    }
}
