namespace Domain.Product.ValueObject
{
    public sealed record ProductCategory
    (
        Guid CategoryId,
        Guid TypeId,
        Guid SubTypeId
    );
}