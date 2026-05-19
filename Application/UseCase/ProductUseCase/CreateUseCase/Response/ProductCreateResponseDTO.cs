using Domain.Product.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.ProductUseCase.CreateUseCase.Response
{
    public record ProductCreateResponseDTO
    (
        string Id,
        string Name
        
       );
}
