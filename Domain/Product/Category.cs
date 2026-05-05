using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Product
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int? ParentId { get; private set; }

        public Category Parent { get; private set; }

        private readonly List<Category> _children = [];
        public IReadOnlyCollection<Category> Children => _children;


        public Category (string name, IEnumerable<Category> children)
        {
            if (children is null) throw new ArgumentNullException(nameof(children), "A lista de subcategorias não pode ser nula.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            Name = name;

            foreach (var item in children)
            {
                AddSubCategory(item);
            }

        }



        public void AddSubCategory(Category children)
        {
            if (children is null) throw new ArgumentNullException(nameof(children), "Categoria filha não pode ser nula.");

            if (children == this) throw new ArgumentNullException(nameof(children), "Categoria não pode ser filha dela mesma.");

            _children.Add(children);
        }

      
    }
}
