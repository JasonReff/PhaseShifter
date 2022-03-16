public class KnockbackPowerup : Powerup
{
    protected override void OnPickup()
    {
        base.OnPickup();
        _characterStats.Stats.KnockbackMultiplier *= _statMultiplier;
    }
}
