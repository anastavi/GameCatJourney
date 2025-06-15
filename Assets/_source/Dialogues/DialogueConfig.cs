using System;
using UnityEngine;

namespace GameDialogues
{
    internal enum DoActionType { None, InstantAction, ConfirmationAction, AddItem, RemoveItem, }

    [CreateAssetMenu(fileName = nameof(DialogueConfig), menuName = "Dialogues/" + nameof(DialogueConfig))]
    public class DialogueConfig : ScriptableObject
    {
        [SerializeField] private string _saveKey;
        [SerializeField] private DialogueBase[] _dialogues;

        private int _currentIndex = 0;

        internal string SaveKey => _saveKey;
        internal int Count => _dialogues.Length;

        internal bool TryGetSentenceByDialogueIndex(int index, out DialogueInfo info)
        {
            info.Sentence = string.Empty;
            info.DoAction = _dialogues[index].DoAction;
            info.Item = _dialogues[index].Item;

            if (_currentIndex >= _dialogues[index].Count || _currentIndex < 0)
                return false;

            info = _dialogues[index].GetSentence(_currentIndex);
            _currentIndex++;
            return true;
        }

        internal void ResetIndex() => _currentIndex = 0;

        private void OnValidate()
        {
            _saveKey = name;
        }

        [Serializable]
        private class DialogueBase
        {
            [SerializeField] private string[] _sentences;
            [SerializeField] private DoActionType _doAction;
            [SerializeField] private Items _item;

            internal DoActionType DoAction => _doAction;
            internal Items Item => _item;
            internal int Count => _sentences.Length;

            internal DialogueInfo GetSentence(int index)
            {
                var info = new DialogueInfo();

                if (index >= _sentences.Length || index < 0)
                    return info;

                info.Sentence = _sentences[index];
                info.DoAction = _doAction;
                info.Item = _item;
                return info;
            }
        }
    }

    [Serializable]
    public struct DialogueInfo
    {
        [SerializeField] internal string Sentence;
        [SerializeField] internal DoActionType DoAction;
        [SerializeField] internal Items Item;
    }
}