
using Application.DTOs.TableDTO;
using Application.Mappers;
using Domain.Table;

namespace Application.UseCase.TableUseCase
{
    public class TableUseCase
    {
        private readonly ITableRepository _tableRepository;


        public async Task<List<TablePreviewDTO>> ListTable()
        {
            var tables = await _tableRepository.GetAllAsync();

            if (!tables.Any()) 
                throw new InvalidOperationException("Nenhuma mesa encontrada.");

            return tables.Select(x => new TablePreviewDTO
            {
                Id = x.Id,
                Number = x.Number,
                Status = x.Status.StatusMap(),

            }).ToList();


        }
    }
}
