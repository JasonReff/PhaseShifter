using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerUpgrade Upgrade;
    [SerializeField] private TextMeshProUGUI _nameTextbox, _descriptionTextbox;
    [SerializeField] private float _largeScale = 1.1f, _normalScale = 1f, _scaleTime = 0.2f;

    public static event Action OnUpgradeChosen;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_largeScale, _scaleTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_normalScale, _scaleTime);
    }

    public void SetText()
    {
        _nameTextbox.text = Upgrade.UpgradeName;
        _descriptionTextbox.text = Upgrade.UpgradeDescription;
    }

    public void ChooseUpgrade()
    {
        Upgrade.AddUpgrade();
        OnUpgradeChosen?.Invoke();
    }
}