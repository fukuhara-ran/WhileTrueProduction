using UnityEngine;
public class HealthManager
{
    public int FullHealthPoint { get; private set; }
    public int CurrentHealthPoint { get; private set; }

    public event System.Action OnDamaged;
    public event System.Action OnDied;

    public HealthManager(int fullHealthPoint)
    {
        FullHealthPoint = fullHealthPoint;
        CurrentHealthPoint = fullHealthPoint;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealthPoint -= damage;
        OnDamaged?.Invoke();

        if (CurrentHealthPoint <= 0)
        {
            CurrentHealthPoint = 0;
            OnDied?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealthPoint = Mathf.Min(CurrentHealthPoint + amount, FullHealthPoint);
    }
}