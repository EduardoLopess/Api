
namespace Domain.Table.ValueObject
{
    public record UserId
    {
        public Guid Value { get; }

        public UserId (Guid value)
        {
            if (value == Guid.Empty) 
                throw new ArgumentException("Id inválido.");

            Value = value;
        }

    }
}
