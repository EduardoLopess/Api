namespace Domain.Product.ValueObject
{
    public class ProductCategory
    {
        public Category Category { get; private set; }
        public Category Type { get; private set; }
        public Category SubType { get; private set; }

        public ProductCategory(Category category, Category type, Category subType)
        {

            ArgumentNullException.ThrowIfNull(category);

            ArgumentNullException.ThrowIfNull(type);

            ArgumentNullException.ThrowIfNull(subType);

            if (type.ParentId != category.Id)
                throw new ArgumentException("Tipo inválido.");

            if (subType.ParentId != type.Id)
                throw new ArgumentException("Subtipo inválido.");

            Category = category;
            Type = type;
            SubType = subType;
        }
    }
}
