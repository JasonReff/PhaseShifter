using UnityEngine;
using UnityEngine.UI;

public class PlayerUltimateUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        PlayerUltimateMeter.OnUltimatePercentChanged += UpdateSliderValue;
    }

    private void OnDisable()
    {
        PlayerUltimateMeter.OnUltimatePercentChanged -= UpdateSliderValue;
    }

    private void UpdateSliderValue(float ultimate)
    {
        _slider.value = ultimate;
    }

}
