using System;
using UnityEngine;

public class UpgradeChest : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _opened = false;

    public static event Action OnUpgradeChestOpened;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player" || _opened)
            return;
        OpenChest();
    }

    private void OpenChest()
    {
        OnUpgradeChestOpened?.Invoke();
        _animator.SetBool("Opened", true);
        _opened = true;
    }
}