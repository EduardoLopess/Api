
public record Category(Guid Id, Guid? ParentId, Category? Parent)
{
    public Guid Id { get; private set; } = Id;
    public string Name { get; private set; } = string.Empty;

    public Guid? ParentId { get; private set; } = ParentId;
    public Category? Parent { get; private set; } = Parent;

    private readonly List<Category> _children = [];
    public IReadOnlyCollection<Category> Children => _children;

    protected Category() : this(default, null, null) { }

    public Category(string name, IEnumerable<Category>? children = null) : this(Guid.NewGuid(), null, null)
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
            throw new ArgumentNullException(nameof(children));

        if (children == this)
            throw new ArgumentException("Categoria não pode ser filha dela mesma.");

        children.Parent = this;
        children.ParentId = Id;

        _children.Add(children);
    }
}