using Domain.Product.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.ProductUseCase.CreateUseCase
{
    public record CreateProductBaseRequest
     (
        string Name,
        decimal Price,
        Guid CategoryId,
        Guid TypeId,
        Guid SubTypeId,
        int QuantityStock,
        Availability Availability
     );
}
