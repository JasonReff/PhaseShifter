using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerUpgradePool _upgradePool;
    [SerializeField] private GameObject _upgradeCanvas;
    [SerializeField] private int _numberOfUpgradeChoices = 3;


    private void OnEnable()
    {
        UpgradeChest.OnUpgradeChestOpened += ShowUpgrades;
        UpgradeChoice.OnUpgradeChosen += HideUpgrades;
    }

    private void OnDisable()
    {
        UpgradeChest.OnUpgradeChestOpened -= ShowUpgrades;
        UpgradeChoice.OnUpgradeChosen -= HideUpgrades;
    }

    private void ShowUpgrades()
    {
        Time.timeScale = 0;
        _upgradeCanvas.SetActive(true);
        List<PlayerUpgrade> upgrades = _upgradePool.Upgrades.PullWithoutReplacement(_numberOfUpgradeChoices);
        for (int i = 0; i < _numberOfUpgradeChoices; i++)
        {
            var choice = _upgradeCanvas.transform.GetChild(i).GetComponent<UpgradeChoice>();
            choice.Upgrade = upgrades[i];
            choice.SetText();
        }
    }

    private void HideUpgrades()
    {
        Time.timeScale = 1;
        _upgradeCanvas.SetActive(false);
    }
}
