namespace Domain.Product.ValueObject
{
    public class ProductCategory
    {
        public Guid CategoryId { get; private set; }
        public Guid TypeId { get; private set; }
        public Guid SubTypeId { get; private set; }

        private ProductCategory() { }

        public ProductCategory(
            Guid categoryId,
            Guid typeId,
            Guid subTypeId)
        {


            CategoryId = categoryId;
            TypeId = typeId;
            SubTypeId = subTypeId;
        }
    }
}