public interface ISpell : ICaster, ISubject
{
}

// 시전자
public interface ICaster
{
    public Caster Caster { get; set; }
}

// 대상자
public interface ISubject
{
    public Subject Subject { get; set; }
}

// 스킬이나 마법 따위를 주문하는 용어
public class Spell
{
    public ICaster Caster { get; set; }
    public ISubject Subject { get; set; }
}

public class Caster
{
    public Caster(Entity entity)
    {
        Entity = entity;
    }

    public Entity Entity { get; set; }
}

public class Subject
{
    public Subject(Entity entity)
    {
        Entity = entity;
    }
    public Entity Entity { get; set; }
}