using Domain.Product.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.ProductUseCase.CreateUseCase
{
    public record ProductCreateResponse
    (
        string Id,
        string Name
        
       );
}
