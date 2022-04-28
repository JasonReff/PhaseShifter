﻿using UnityEngine;

public class ShotgunShoot : CharacterShoot
{
    [SerializeField] private int _numberOfProjectiles;

    public override void ShootInDirection(Vector2 attackDirection)
    {
        for (int i = 0; i < _numberOfProjectiles; i++)
            base.ShootInDirection(attackDirection);
    }
}