using System;

namespace Store.Domain;

public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}

public class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
}

public class Entity : Entity<int>
{
}
