using Errors = System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>;

namespace Desafio_Balta_IBGE.Shared.ValueObjects
{
    public abstract class ValueObject 
    {
        public ValueObject()
        {
            IsValid = true;
        }
        public bool IsValid { get; private set; }
        public Errors Errors { get; private set; }
        protected void AddNotification(Errors errors)
        {
            IsValid = false;
            Errors = errors;
        }

    }
}
