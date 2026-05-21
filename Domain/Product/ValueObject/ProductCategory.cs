using Domain.Common;
using System.Reflection.Metadata;

namespace Domain.Product.ValueObject
{
    public sealed record ProductCategory
    {
        public Guid CategoryId { get; }
        public string CategoryName { get; }
        public Guid TypeId { get; }
        public string TypeName { get; }
        public Guid SubTypeId { get; }
        public string SubTypeName { get; }

        
        private ProductCategory(Guid categoryId, string categoryName, Guid typeId, string typeName, Guid subTypeId, string subTypeName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            TypeId = typeId;
            TypeName = typeName;
            SubTypeId = subTypeId;
            SubTypeName = subTypeName;
        }

        public static Result<ProductCategory> Create(Guid categoryId, string categoryName, Guid typeId, string typeName, Guid subTypeId, string subTypeName)
        {
            var strings = new string[] { categoryName, typeName, subTypeName };
            var resultValidName = NameIsValid(strings);
            if (resultValidName.IsFailure)
                return Result<ProductCategory>.Failure($"Nome inválido: '{resultValidName.Value}'");


            if (categoryId == Guid.Empty || typeId == Guid.Empty || subTypeId == Guid.Empty)
                return Result<ProductCategory>.Failure("Os IDs da categoria, tipo e subtipo são obrigatórios.");

            if (categoryId != typeId && categoryId != subTypeId && typeId != subTypeId)
            {
                var category = new ProductCategory(categoryId,categoryName, typeId, typeName, subTypeId, subTypeName);
                return Result<ProductCategory>.Success(category);
            }

            return Result<ProductCategory>.Failure("Não foi possivel criar o ProductCategory.");

            
        }

        private static Result<string> NameIsValid(params string[] nameList)
        {

            var nomeInvalido = nameList.FirstOrDefault(nome => string.IsNullOrWhiteSpace(nome));
            if (nomeInvalido is not null)
                return Result<string>.Success(nomeInvalido, "Nome inválido.");

            return Result<string>.Success(null!, "Nomes validados com sucesso.");
        }
    }
}