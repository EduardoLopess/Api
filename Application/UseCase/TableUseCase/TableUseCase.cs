
using Application.DTOs.TableDTO;
using Application.DTOs.TableDTO.Request;
using Application.Mappers;
using Domain.Order.Interface;
using Domain.Table;

namespace Application.UseCase.TableUseCase
{
    public class TableUseCase
    {
        private readonly ITableRepository _tableRepository;
        
        public async Task LockedAcess (LockedAcessRequestDTO lockedAcessRequestDTO)
        {
            

            var table = await _tableRepository.GetByIdAsync(lockedAcessRequestDTO.TableId);
            table.TableIsLocked();



        }

        public async Task UnlockedAcess () 
        {
            
        
        }


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

        public async Task Delete (Guid tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table is null)
                throw new InvalidOperationException("Mesa não encontrada.");

            table.DeleteTable();

            await _tableRepository.Delete(table);
        }

     

    }
}
