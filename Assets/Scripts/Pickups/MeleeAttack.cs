using DG.Tweening;
using UnityEngine;

public class MeleeAttack : Attack
{
    [SerializeField] private Vector3 _startingRotation, _endingRotation;
    [SerializeField] private float _swingSpeed;

    protected override void Update()
    {
        base.Update();
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(_startingRotation), Quaternion.Euler(_endingRotation), _currentLifetime * _swingSpeed);
    }
}