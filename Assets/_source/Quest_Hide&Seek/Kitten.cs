using System.Collections;
using UnityEngine;
using GameContracts;

namespace GameHideAndSeek
{
    public class Kitten : MonoBehaviour, IDialogueAction
    {
        [SerializeField] private Transform _startPosition;
        [SerializeField] private GameObject _dialogueObject;
        [SerializeField] private GameObject _triggerObject;

        public void DoDialogueAction() => MoveToStartPosition();

        internal void MoveToHidePosition(Vector3 position)
        {
            transform.position = position;
            StartCoroutine(SetDialogueActive(true));
        }

        private void MoveToStartPosition()
        {
            transform.position = _startPosition.position;
            StartCoroutine(SetDialogueActive(true));
        }

        private IEnumerator SetDialogueActive(bool isActive)
        {
            yield return null;
            _dialogueObject.SetActive(isActive);
            _triggerObject.SetActive(isActive);
        }
    }
}