using System;

public class HealthPickup : Pickup
{
    public static event Action OnHealthPickup;
    protected override void OnPickup()
    {
        base.OnPickup();
        OnHealthPickup?.Invoke();
    }
}