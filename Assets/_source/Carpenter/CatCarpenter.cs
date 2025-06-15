using UnityEngine;
using GameContracts;

namespace GameCarpenter
{
    public class CatCarpenter : MonoBehaviour, IDialogueAction
    {
        private const string SaveKey = "Bridge";

        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _bridgeRenderer;
        [SerializeField] private Sprite _fixed;
        [SerializeField] private Sprite _broken;

        private void Awake()
        {
            bool isBroken = PlayerPrefs.GetInt(SaveKey, 0) == 0;

            if(isBroken)
            {
                _collider.enabled = true;
                _bridgeRenderer.sprite = _broken;
            }
            else
            {
                _collider.enabled = false;
                _bridgeRenderer.sprite = _fixed;
            }
        }

        public void DoDialogueAction()
        {
            _collider.enabled = false;
            _bridgeRenderer.sprite = _fixed;
            PlayerPrefs.SetInt(SaveKey, 1);
            PlayerPrefs.Save();
        }
    }
}