
namespace Domain.Table.ValueObject
{
    public record UserId
    {
        public int Value { get; }

        public UserId (int value)
        {
            if (value <= 0) throw new ArgumentException("Id inválido.");

            Value = value;
        }

    }
}
