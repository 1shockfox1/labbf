using System;
using System.Collections.Generic;



public interface IWeapon { string Name { get; } }
public interface IArmor { string Name { get; } }
public interface IEnvironment { string Name { get; } }

public class Sword : IWeapon { public string Name => "Меч"; }
public class MagicArmor : IArmor { public string Name => "Магическая броня"; }
public class FantasyWorld : IEnvironment { public string Name => "Средневековое королевство"; }

public class EnergyGun : IWeapon { public string Name => "Энергетическое ружьё"; }
public class TechShield : IArmor { public string Name => "Технологический щит"; }
public class SpaceWorld : IEnvironment { public string Name => "Космическая станция"; }

public interface IWorldFactory
{
    IWeapon CreateWeapon();
    IArmor CreateArmor();
    IEnvironment CreateEnvironment();
}

public class FantasyFactory : IWorldFactory
{
    public IWeapon CreateWeapon() => new Sword();
    public IArmor CreateArmor() => new MagicArmor();
    public IEnvironment CreateEnvironment() => new FantasyWorld();
}

public class SciFiFactory : IWorldFactory
{
    public IWeapon CreateWeapon() => new EnergyGun();
    public IArmor CreateArmor() => new TechShield();
    public IEnvironment CreateEnvironment() => new SpaceWorld();
}



public class Character
{
    public string Name { get; init; }
    public string Class { get; init; }
    public string Weapon { get; init; }
    public string Armor { get; init; }
    public int Level { get; init; }
    public List<string> Abilities { get; init; } = new();

    public override string ToString() =>
        $"{Name} | класс: {Class} | оружие: {Weapon} | броня: {Armor} | " +
        $"уровень: {Level} | способности: {string.Join(", ", Abilities)}";
}

public class CharacterBuilder
{
    private string _name = "Безымянный";
    private string _class = "Новичок";
    private string _weapon = "Кулаки";
    private string _armor = "Рубаха";
    private int _level = 1;
    private readonly List<string> _abilities = new();

    public CharacterBuilder SetName(string name) { _name = name; return this; }
    public CharacterBuilder SetClass(string cls) { _class = cls; return this; }
    public CharacterBuilder SetWeapon(string weapon) { _weapon = weapon; return this; }
    public CharacterBuilder SetArmor(string armor) { _armor = armor; return this; }
    public CharacterBuilder SetLevel(int level) { _level = level; return this; }
    public CharacterBuilder AddAbility(string ability) { _abilities.Add(ability); return this; }

    public Character Build() => new Character
    {
        Name = _name,
        Class = _class,
        Weapon = _weapon,
        Armor = _armor,
        Level = _level,
        Abilities = _abilities
    };
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Abstract Factory ===");
        IWorldFactory world = new FantasyFactory();
        Console.WriteLine($"Мир: {world.CreateEnvironment().Name}");
        Console.WriteLine($"Оружие: {world.CreateWeapon().Name}");
        Console.WriteLine($"Броня: {world.CreateArmor().Name}");

        world = new SciFiFactory();
        Console.WriteLine($"Мир: {world.CreateEnvironment().Name}");
        Console.WriteLine($"Оружие: {world.CreateWeapon().Name}");
        Console.WriteLine($"Броня: {world.CreateArmor().Name}");

        Console.WriteLine("\n=== Builder ===");
        Character hero = new CharacterBuilder()
            .SetName("Артас")
            .SetClass("Паладин")
            .SetWeapon("Светозар")
            .SetArmor("Латный доспех")
            .SetLevel(10)
            .AddAbility("Свет")
            .AddAbility("Исцеление")
            .Build();
        Console.WriteLine(hero);

        Character noob = new CharacterBuilder().SetName("Новичок").Build();
        Console.WriteLine(noob);
    }
}