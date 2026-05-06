public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public int? ParentId { get; private set; }
    public Category? Parent { get; private set; }

    private readonly List<Category> _children = [];
    public IReadOnlyCollection<Category> Children => _children;

    protected Category() { }

    public Category(string name, IEnumerable<Category>? children = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome da categoria não informado.", nameof(name));

        Name = name;

        if (children is not null)
        {
            foreach (var item in children)
                AddSubCategory(item);
        }
    }

    public void AddSubCategory(Category children)
    {
        if (children is null)
            throw new ArgumentNullException(nameof(children), "Categoria filha não pode ser nula.");

        if (children == this)
            throw new ArgumentException("Categoria não pode ser filha dela mesma.", nameof(children));

        children.Parent = this;
        children.ParentId = Id;

        _children.Add(children);
    }
}