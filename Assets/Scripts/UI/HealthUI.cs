using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Color _full, _empty;
    [SerializeField] private List<Image> _hearts = new List<Image>();
    [SerializeField] private Image _characterPortrait;
    [SerializeField] private Sprite _redHeart, _blueHeart;
    [SerializeField] private List<Sprite> _redPortraits, _bluePortraits, _currentPortraits;
    private int _health = 3;

    private void OnEnable()
    {
        PlayerHealthController.OnHealthChanged += FillHearts;
        PlayerHealthController.OnHealthChanged += ChangePortrait;
        PhaseController.OnPhaseChanged += ChangePhase;
        ChangePhase(PhaseController.Phase);
    }

    private void OnDisable()
    {
        PlayerHealthController.OnHealthChanged -= FillHearts;
        PlayerHealthController.OnHealthChanged -= ChangePortrait;
        PhaseController.OnPhaseChanged -= ChangePhase;
    }

    private void FillHearts(int health)
    {
        _health = health;
        if (health > 3)
            health = 3;
        foreach (var heart in _hearts)
            heart.color = _empty;
        for (int i = 0; i < health; i++)
        {
            _hearts[i].color = _full;
        }
    }

    private void ChangePortrait(int health)
    {
        switch (health)
        {
            case 3:
                _characterPortrait.sprite = _currentPortraits[0];
                break;
            case 2:
                _characterPortrait.sprite = _currentPortraits[1];
                break;
            case 1:
                _characterPortrait.sprite = _currentPortraits[2];
                break;
            default:
                break;
        }
    }

    private void ChangePhase(Phase phase)
    {
        switch (phase)
        {
            case Phase.Red:
                _currentPortraits = _redPortraits;
                break;
            case Phase.Blue:
                _currentPortraits = _bluePortraits;
                break;
        }
        ChangePortrait(_health);
        ChangeHearts(phase);
    }

    private void ChangeHearts(Phase phase)
    {
        foreach (var heart in _hearts)
        {
            switch (phase)
            {
                case Phase.Red:
                    heart.sprite = _redHeart;
                    break;
                case Phase.Blue:
                    heart.sprite = _blueHeart;
                    break;
            }
        }
    }
}
