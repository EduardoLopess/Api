using Application.DTOs.OrderDTO;
using Domain.Order.Interface;
using Domain.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCase.OrderUseCase
{
    public class OrderUseCase
    {
        private readonly IOrderRepository _repository;
        private readonly ITableRepository _tableRepository;

        public async Task<List<OrderPreviewTableDTO>> PreviewOrderTable(Guid tableId) 
        {
            
        }
    }
}
