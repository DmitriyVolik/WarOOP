using WarOOP.Models;

namespace WarOOP.Tests.Models;

public class Rookie : Warrior
{
    public Rookie()
    {
        CurrentHealth = 50;
        StartHealth = CurrentHealth;
        Attack = 1;
    }
}