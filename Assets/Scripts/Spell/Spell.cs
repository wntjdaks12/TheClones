public interface ISpell : ICaster, ISubject
{ 
}

// ������
public interface ICaster
{
    public Entity Caster { get; set; }
}

// �����
public interface ISubject
{
    public Entity[] Subjects { get; set; }
}

/*
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
}*/

/*
// ��ų�̳� ���� ������ �ֹ��ϴ� ���
public class Spell
{
    public Caster Caster { get; set; }
    public Subject Subject { get; set; }
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
}*/