
using Domain.Product;
using Domain.Product.Interface;
using Domain.Product.ValueObject;


namespace Application.UseCase.ProductUseCase.CreateUseCase
{
    public class CreateUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<Result<ProductCreateResponse>> RegisterProductBase(CreateProductBaseRequest request)
        {

            var priceResult = Price.Create(request.Price);
            if (priceResult.IsFailure)
                return Result<ProductCreateResponse>.Failure(priceResult.Message);

            var quantityResult = QuantityStock.Create(request.QuantityStock);
            if (quantityResult.IsFailure)
                return Result<ProductCreateResponse>.Failure(quantityResult.Message);

            var productCategoryResult = ProductCategory.Create(request.CategoryId, request.TypeId, request.SubTypeId);
            if (productCategoryResult.IsFailure)
                return Result<ProductCreateResponse>.Failure(productCategoryResult.Message);

            var productResult = Product.CreteProductBase(
                request.Name,
                priceResult.Value!,
                quantityResult.Value!,
                productCategoryResult.Value!, 
                request.Availability);

            if (productResult.IsFailure)
                return Result<ProductCreateResponse>.Failure(productResult.Message);

            var idsSearch = new List<Guid> { request.CategoryId, request.TypeId, request.SubTypeId };

            var validCategory = await _categoryRepository.GetGuidsAsync(idsSearch);

            if (validCategory.Count < 3)
                return Result<ProductCreateResponse>.Failure("Uma ou mais categorias não existe.");

            Product product = productResult.Value!;

            await _productRepository.CreateAsync(product);

            var productDTO = new ProductCreateResponse(product.Id.ToString(), product.Name);

            return Result<ProductCreateResponse>.Success(productDTO, "Produto registrado com sucesso");
        }
    }
}

   