using UnityEngine;

namespace GameMenu
{
    internal class PauseMenuButtonView : MenuButtonView
    {
        private const float ScaleMultiplier = 1.2f;

        private Vector3 _startScale;

        internal override void Init()
        {
            _startScale = transform.localScale;
            UpdateText();
        }

        internal override void OnSelectAnimation()
        {
            transform.localScale *= ScaleMultiplier;
        }

        internal override void OnDeselectAnimation()
        {
            transform.localScale = _startScale;
        }
    }
}