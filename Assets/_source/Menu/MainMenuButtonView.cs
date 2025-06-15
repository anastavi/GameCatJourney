using UnityEngine;

namespace GameMenu
{
    internal class MainMenuButtonView : MenuButtonView
    {
        private const int HighlitedOffset = 30;

        private Vector3 _startPosition;

        internal override void Init()
        {
            _startPosition = transform.position;
            UpdateText();
        }

        internal override void OnSelectAnimation()
        {
            var highlitedPosition = new Vector3(transform.position.x + HighlitedOffset, transform.position.y);
            transform.position = highlitedPosition;
        }

        internal override void OnDeselectAnimation()
        {
            transform.position = _startPosition;
        }
    }
}