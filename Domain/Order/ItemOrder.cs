using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Order
{
    public class ItemOrder
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int Quantitiy { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set;  }
        public decimal TotalItem => UnitPrice * Quantitiy;


        protected ItemOrder () {}

        public ItemOrder(int id, int productId, int quantitiy, decimal unitPrice)
        {
            Id = id;
            ProductId = productId;
            Quantitiy = quantitiy;
            UnitPrice = unitPrice;
            Discount = 0;
        }

        public void IncrementQuantitiy()
        {
            if (Quantitiy >= 99) 
                throw new InvalidOperationException("Quantidade máxima excedida.");

            Quantitiy += 1;
        }

        public void DecrementQuantity()
        {
            if (Quantitiy <= 1) 
                throw new InvalidOperationException("Quantidade minima permitida é 1. ");

            Quantitiy -= 1;
        }

        public void ApplyDiscount ()
        {

        }
    }
}
