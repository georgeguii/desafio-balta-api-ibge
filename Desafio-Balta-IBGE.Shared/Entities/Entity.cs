namespace Desafio_Balta_IBGE.Shared.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity()
            => Id = Guid.NewGuid();
    }
}
