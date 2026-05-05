using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Order
{
    public class Order
    {
        public int Id { get; private set; }
        public int TableId { get; private set; }
        public DateTime DateCreate { get; private set; }
        public int TotalOrder { get; private set;  }

        private readonly List<ItemOrder> _orderItens = [];
        public IReadOnlyCollection<ItemOrder> OrderItens => _orderItens;

        protected Order() {}


        public Order(DateTime dateCreate, IEnumerable<ItemOrder> orderItens)
        {

            if (orderItens is null) throw new InvalidOperationException("Deve ser informado algum item.");

            if (orderItens is not null)
            {
                foreach (var item in orderItens)
                {
                    AddItem(item);
                }
            }

            DateCreate = DateTime.UtcNow;
        }



        public void AddItem (ItemOrder item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item), "Item não informado");

       
            _orderItens.Add(item);
        }

        public void RemoveItem (ItemOrder item)
        {
            var itemRemove = _orderItens.Find(i => i.Id == item.Id);

            if (itemRemove is null)
                throw new InvalidOperationException("Item não encontrado.");

            _orderItens.Remove(itemRemove);

        }


      
    }
}
