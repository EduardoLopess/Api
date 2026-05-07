using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Order
{
    public class ItemOrder
    {
        public Guid Id { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalItem => UnitPrice * Quantity - Discount;


        protected ItemOrder () {}

        public ItemOrder(int id, int productId, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice; 
            Discount = 0;
        }

        public void IncrementQuantity()
        {
            if (Quantity >= 99) 
                throw new InvalidOperationException("Quantidade máxima excedida.");

            Quantity += 1;
        }

        public void DecrementQuantity()
        {
            if (Quantity <= 1) 
                throw new InvalidOperationException("Quantidade minima permitida é 1. ");

            Quantity -= 1;
        }

        public void ApplyDiscountValue(decimal valueDiscont)
        {
            var subtotal = UnitPrice * Quantity;

            if (valueDiscont <= 0 || valueDiscont >= subtotal)
                throw new ArgumentException("Valor de desconto inválido.");

            Discount = valueDiscont;
        }

        public void ApplyDiscountPercent(int valuePercent)
        {

        }
    }
}
