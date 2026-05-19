using Domain.Common;

namespace Domain.Product.ValueObject
{
    public sealed record ProductCategory
    {
        public Guid CategoryId { get; }
        public Guid TypeId { get; }
        public Guid SubTypeId { get; }

        
        private ProductCategory(Guid categoryId, Guid typeId, Guid subTypeId)
        {
            CategoryId = categoryId;
            TypeId = typeId;
            SubTypeId = subTypeId;
        }

        public static Result<ProductCategory> Create(Guid categoryId, Guid typeId, Guid subTypeId)
        {
            
            if (categoryId == Guid.Empty || typeId == Guid.Empty || subTypeId == Guid.Empty)
            {
                return Result<ProductCategory>.Failure("Os IDs da categoria, tipo e subtipo são obrigatórios.");
            }

            var category = new ProductCategory(categoryId, typeId, subTypeId);
            return Result<ProductCategory>.Success(category);
        }
    }
}