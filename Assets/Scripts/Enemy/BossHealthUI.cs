using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _slider.value = 1;
        BossHealthController.OnBossHealthChanged += ChangeSliderValue;
        BossStateMachine.OnBossSpawned += ShowSlider;
        BossHealthController.OnBossDeath += HideSlider;
    }

    private void OnDisable()
    {
        BossHealthController.OnBossHealthChanged -= ChangeSliderValue;
        BossStateMachine.OnBossSpawned -= ShowSlider;
        BossHealthController.OnBossDeath -= HideSlider;
    }

    private void ShowSlider()
    {
        _slider.enabled = true;
    }

    private void HideSlider()
    {
        _slider.enabled = false;
    }

    private void ChangeSliderValue(float healthPercent)
    {
        _slider.value = healthPercent;
    }
}