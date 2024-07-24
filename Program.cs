class Program
{
    public static void Main(string[] args)
    {
        Zombie enemy1 = new Zombie("John", 10, 1);
        Dragon enemy2 = new Dragon("Grigoru", 100, 10);
        IUnit<IUnitCharacteristics>[] units = { enemy1, enemy2 };
        foreach (IUnit<IUnitCharacteristics> unit in units)
        {
            if (unit is Zombie zombie)
                zombie.Sound();
        }
        Enemy[] enemies = { enemy1, enemy2 };
        foreach (Enemy enemy in enemies)
        {
            if (enemy is Zombie zombie)
                zombie.Sound();
        }
        /*
         * Если есть необходимость в обобщение обьектов, которые
         * не имеют общего абстрактного класса, но 
         * имеют общию реализацию интерфейса то интерфейс
         * должен быть ковариантным.
         * Ключевое слово out
         */
    }
}

public interface IUnitCharacteristics
{
    public int Health { get; private protected set; }
    public int Damage { get; private protected set; }
}
public interface IUnit<out T> where T : IUnitCharacteristics
{
    string Name { get; set; }
    void Attack(IUnit<IUnitCharacteristics> unit);
    void GetDamage(int damage);
}
public class EnemyCharacteristics : IUnitCharacteristics
{
    public int Health { get; set; }
    public int Damage { get; set; }
}
public abstract class Enemy : IUnit<EnemyCharacteristics>
{
    public string Name { get; set; }
    protected EnemyCharacteristics _enemyCharacteristics;
    public int Health
    {
        get => _enemyCharacteristics.Health;
        protected set => _enemyCharacteristics.Health = value;
    }
    public int Damage
    {
        get => _enemyCharacteristics.Damage;
        protected set => _enemyCharacteristics.Damage = value;
    }

    public Enemy(string name, int health, int damage)
    {
        Name = name;
        _enemyCharacteristics = new EnemyCharacteristics();
        Health = health;
        Damage = damage;
    }
    public virtual void GetDamage(int damage)
    {
        Health -= damage;
    }
    public virtual void Attack(IUnit<IUnitCharacteristics> unit) => unit.GetDamage(Damage);
}
public class Zombie : Enemy
{
    public Zombie(string name, int health, int damage) : base(name, health, damage)
    {
        
    }
    public void Sound()
    {
        Console.WriteLine($"{Name}: Rrrrr");
    }
}
public class Dragon : Enemy
{
    public Dragon(string name, int health, int damage) : base(name, health, damage)
    {
    }
}