namespace Desafio_Balta_IBGE.Shared.Entities
{
    public interface IEntity
    {
        Guid Id { get; } 
    }
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity()
            => Id = Guid.NewGuid();

        
    }
}
