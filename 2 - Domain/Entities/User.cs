namespace Domain.Entities
{
    public sealed class User : Entity
    {
        public string Name { get; private set; }
        public User(string name)
        {
            Name = name;
        }
    }
}
