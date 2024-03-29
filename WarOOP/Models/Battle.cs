using System.Diagnostics;

namespace WarOOP.Models;

public static class Battle
{
    
    private static void Validate(Army army1, Army army2)
    {
        if (army1 == null || army2 == null)
        {
            throw new NullReferenceException("Army can not be null");
        }
        
        if (!army1.HasUnits && !army2.HasUnits)
        {
            throw new Exception("Armies can not be empty");
        }
    }
    
    public static bool Fight(Warrior warrior1, Warrior warrior2)
    {
        if (warrior1.Attack == 0 && warrior2.Attack == 0)
        {
            throw new Exception("Non damage units");
        }
        
        while (true)
        {

            Console.WriteLine(warrior1.GetType().Name + ":" + warrior1.CurrentHealth + "->" + warrior2.GetType().Name + ":" + warrior2.CurrentHealth);
            warrior1.AttackTo(warrior2);
            if (!warrior2.IsAlive)
            {
                return true;
            }

            warrior2.AttackTo(warrior1);
            if (!warrior1.IsAlive)
            {
                return false;
            }
        }
    }

    public static bool Fight(Army army1, Army army2)
    {
        Validate(army1, army2);

        if (!army1.HasUnits)
        {
            return false;
        }
        
        if (!army2.HasUnits)
        {
            return true;
        }
        
        army1.MoveUnits();
        army2.MoveUnits();
        
        Console.WriteLine("-------------A1---------------");
        foreach (var item in army1.AllAlive())
        {
            Console.WriteLine(item.GetType().Name);
        }

        Console.WriteLine("-------------A2---------------");
        foreach (var item in army2.AllAlive())
        {
            Console.WriteLine(item.GetType().Name);
        }
        
        while (true)
        {
            army1.PrepareForFight();
            army2.PrepareForFight();
            var fightResult=Fight(army1.GetUnit(), army2.GetUnit());

            if (fightResult)
            {
                army2.MoveUnits();
                army2.SetNextUnit();
                if (!army2.HasUnits)
                {
                    return true;
                }
            }
            else
            {
                army1.MoveUnits();
                army1.SetNextUnit();
                if (!army1.HasUnits)
                {
                    return false;
                }
            }
        }
    }

    public static bool StraightFight(Army army1, Army army2)
    {
        Validate(army1, army2);
        
        army1.MoveUnits();
        army2.MoveUnits();
        
        while (true)
        {
            army1.PrepareForStraightFight();
            army2.PrepareForStraightFight();
            while (army1.HasUnits && army2.HasUnits)
            {
                foreach (var (first, second) in army1.AllAlive().Zip(army2.AllAlive()))
                {
                    var result = Fight(first, second);
                    if (result)
                    {
                        army1.MoveUnits();
                    }
                    else
                    {
                        army2.MoveUnits();
                    }
                }
            }
            return army1.HasUnits;
        }
    }
}
