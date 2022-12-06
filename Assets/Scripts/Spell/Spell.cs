public interface ISpell : ICaster, ISubject
{
}

// ������
public interface ICaster
{
    public Caster Caster { get; set; }
}

// �����
public interface ISubject
{
    public Subject Subject { get; set; }
}

// ��ų�̳� ���� ������ �ֹ��ϴ� ���
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