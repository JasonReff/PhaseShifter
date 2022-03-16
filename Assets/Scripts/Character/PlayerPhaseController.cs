using UnityEngine;

public class PlayerPhaseController : CharacterPhaseController
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _blue, _red;

    protected override void ChangePhase(Phase phase)
    {
        base.ChangePhase(phase);
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        switch (Phase)
        {
            case Phase.Red:
                _spriteRenderer.sprite = _red;
                break;
            case Phase.Blue:
                _spriteRenderer.sprite = _blue;
                break;
        }
    }
}