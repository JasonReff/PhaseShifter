using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Color _full, _empty;
    [SerializeField] private List<Image> _hearts = new List<Image>();
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private Image _heartPrefab;
    [SerializeField] private Transform _heartParent;
    [SerializeField] private Image _characterPortrait;
    [SerializeField] private Sprite _redHeart, _blueHeart;
    [SerializeField] private List<Sprite> _redPortraits, _bluePortraits, _currentPortraits;
    private int _health = 3;
    private int _maxHealth = 3;

    private void OnEnable()
    {
        PlayerHealthController.OnHealthChanged += FillHearts;
        PlayerHealthController.OnHealthChanged += ChangePortrait;
        PlayerHealthController.OnMaxHealthChanged += SpawnHearts;
        PhaseController.OnPhaseChanged += ChangePhase;
        ChangePhase(PhaseController.Phase);
    }

    private void OnDisable()
    {
        PlayerHealthController.OnHealthChanged -= FillHearts;
        PlayerHealthController.OnHealthChanged -= ChangePortrait;
        PhaseController.OnPhaseChanged -= ChangePhase;
    }

    private void Start()
    {
        _maxHealth = _characterStats.Stats.MaxHealth;
        _health = _characterStats.Stats.CurrentHealth;
        SpawnHearts(_maxHealth);
        FillHearts(_health);
    }

    private void SpawnHearts(int maxHealth)
    {
        for (int i = _hearts.Count - 1; i >= 0; i--)
        {
            Destroy(_hearts[i].gameObject);
            _hearts.RemoveAt(i);
        }
        for (int i = 0; i < maxHealth; i++)
        {
            var heartImage = Instantiate(_heartPrefab, _heartParent);
            _hearts.Add(heartImage);
        }
    }

    private void FillHearts(int health)
    {
        _health = health;
        if (health > _maxHealth)
            health = _maxHealth;
        foreach (var heart in _hearts)
            heart.color = _empty;
        for (int i = 0; i < health; i++)
        {
            _hearts[i].color = _full;
        }
    }

    private void ChangePortrait(int health)
    {
        float healthPercent = (float)health / _maxHealth;
        if (healthPercent >= 0.75f)
            _characterPortrait.sprite = _currentPortraits[0];
        else if (healthPercent >= 0.25f)
            _characterPortrait.sprite = _currentPortraits[1];
        else
            _characterPortrait.sprite = _currentPortraits[2];
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
