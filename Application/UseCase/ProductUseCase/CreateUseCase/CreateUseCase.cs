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

        private Price ConvertePriceToVo(decimal price) => new Price(price);
        private QuantityStock ConverteQuantityToVo(int quantity) => new QuantityStock(quantity);



        public async Task<Result<ProductCreateResponseDTO>> RegisterProductBase(RegisterProductBaseRequestDTO request)
        {

            var idsSearch = new List<Guid> { request.CategoryId, request.TypeId, request.SubTypeId };

            var validCategory = await _categoryRepository.GetGuidsAsync(idsSearch);

            if (validCategory.Count < 3)
                return Result<ProductCreateResponseDTO>.Failure("Uma ou mais categorias não existe.");

            var price = ConvertePriceToVo(request.Price);
            var quantity = ConverteQuantityToVo(request.QuantityStock);

            var productCategory = new ProductCategory(request.CategoryId, request.TypeId, request.SubTypeId);
           

            var product = new Product(request.Name, price, quantity, productCategory, request.Availability);

            await _productRepository.CreateAsync(product);

            var productDTO = new ProductCreateResponseDTO(product.Id.ToString(), product.Name);

            return Result<ProductCreateResponseDTO>.Success(productDTO, "Produto registrado com sucesso");
        }
    }
}

   