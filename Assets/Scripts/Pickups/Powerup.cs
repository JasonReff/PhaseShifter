using DG.Tweening;
using TMPro;
using UnityEngine;

public class Powerup : Pickup
{
    [SerializeField] protected CharacterStats _characterStats;
    [SerializeField] protected float _statMultiplier;
    [SerializeField] private Canvas _pickupTextPrefab;
    [SerializeField] private string _pickupText;

    protected override void OnPickup()
    {
        base.OnPickup();
        var canvas = Instantiate(_pickupTextPrefab, transform.position, Quaternion.identity);
        var textbox = canvas.GetComponentInChildren<TextMeshProUGUI>();
        textbox.text = _pickupText;
        textbox.transform.DOMoveY(transform.position.y + 1, 1f).OnComplete(() =>
        {
            textbox.DOFade(0, 0.5f).OnComplete(() => 
            {
                Destroy(canvas.gameObject);
            });
        });
    }
}
