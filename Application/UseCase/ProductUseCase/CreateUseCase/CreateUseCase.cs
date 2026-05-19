using Application.UseCase.ProductUseCase.CreateUseCase.Request;
using Application.UseCase.ProductUseCase.CreateUseCase.Response;
using Domain.Employee.ValueObject;
using Domain.Product;
using Domain.Product.Interface;
using Domain.Product.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.ProductUseCase.CreateUseCase
{
    public class CreateUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        private static Result<Price> ConvertePriceToVo(decimal price)
        {
            var voResult = Price.Create(price);
            if (voResult.IsFailure)
                return Result<Price>.Failure(voResult.Message);

            Price priceVo = voResult.Value!;

            return Result<Price>.Success(priceVo, "Vo criado");
        }
        private static Result<QuantityStock> ConverteQuantityToVo(int quantity)
        {
            var voResult = QuantityStock.Create(quantity);
            if (voResult.IsFailure)
                return Result<QuantityStock>.Failure(voResult.Message);

            QuantityStock quantityStock = voResult.Value!;

            return Result<QuantityStock>.Success(quantityStock, "Vo criado");
        }



        public async Task<Result<ProductCreateResponseDTO>> RegisterProductBase(RegisterProductBaseRequestDTO request)
        {

            var priceResult = ConvertePriceToVo(request.Price);

            if (priceResult.IsFailure)
                return Result<ProductCreateResponseDTO>.Failure(priceResult.Message);

            Price price = priceResult.Value!;

            var quantityResult = ConverteQuantityToVo(request.QuantityStock);
            if (quantityResult.IsFailure)
                return Result<ProductCreateResponseDTO>.Failure(quantityResult.Message);

            QuantityStock quantity = quantityResult.Value!;

            var productCategoryResult = ProductCategory.Create(request.CategoryId, request.TypeId, request.SubTypeId);
            if (productCategoryResult.IsFailure)
                return Result<ProductCreateResponseDTO>.Failure(productCategoryResult.Message);

            ProductCategory productCategory = productCategoryResult.Value!;

            var productResult = Product.CreteProductBase(request.Name, price, quantity, productCategory, request.Availability);

            if (productResult.IsFailure)
                return Result<ProductCreateResponseDTO>.Failure(productResult.Message);

            var idsSearch = new List<Guid> { request.CategoryId, request.TypeId, request.SubTypeId };

            var validCategory = await _categoryRepository.GetGuidsAsync(idsSearch);

            if (validCategory.Count < 3)
                return Result<ProductCreateResponseDTO>.Failure("Uma ou mais categorias não existe.");

            Product product = productResult.Value!;

            await _productRepository.CreateAsync(product);

            var productDTO = new ProductCreateResponseDTO(product.Id.ToString(), product.Name);

            return Result<ProductCreateResponseDTO>.Success(productDTO, "Produto registrado com sucesso");
        }
    }
}

   