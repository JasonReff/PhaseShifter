public class ShotSpeedPowerup : Powerup
{
    protected override void OnPickup()
    {
        base.OnPickup();
        _characterStats.Stats.ShotSpeedMultiplier *= _statMultiplier;
    }
}
