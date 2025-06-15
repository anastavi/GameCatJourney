using UnityEngine;
using GameContracts;
using System.Collections.Generic;

namespace GameDialogues
{
    public class Dialogue : MonoBehaviour, IInteractAction
    {
        [SerializeField] private DialogueConfig _config;
        [SerializeField] private MonoBehaviour[] _dialogueActions;

        private IConfirmSuggestion _confirmSuggestion;
        private IPlayer _player;
        private List<IDialogueAction> _actions;
        private DialogueView _view;
        private int _dialogueIndex;
        private int _actionIndex;

        private void Awake()
        {
            if (_dialogueActions.Length > 0)
            {
                _actions = new List<IDialogueAction>();

                for (int i = 0; i < _dialogueActions.Length; i++)
                {
                    var action = _dialogueActions[i];
                    IDialogueAction iaction = (IDialogueAction)action;
                    _actions.Add(iaction);
                }
            }

            _dialogueIndex = PlayerPrefs.HasKey(_config.SaveKey) ? PlayerPrefs.GetInt(_config.SaveKey, 0) : 0;
            _actionIndex = 0;
        }

        public void Init(DialogueView view, IConfirmSuggestion confirmSuggestion, IPlayer player)
        {
            _confirmSuggestion = confirmSuggestion;
            _view = view;
            _player = player;
        }

        public void Interact(out bool wasCancelled)
        {
            wasCancelled = false;
            var hasAvailableSentence = _config.TryGetSentenceByDialogueIndex(_dialogueIndex, out DialogueInfo info);

            if (hasAvailableSentence == false)
            {
                if (info.DoAction == DoActionType.None)
                {
                    TryToStop();
                    Next();
                }

                if (info.DoAction == DoActionType.InstantAction)
                    DoAction();

                if (info.DoAction == DoActionType.ConfirmationAction)
                    _confirmSuggestion.Open(DoAction, TryToStop);

                if (info.DoAction == DoActionType.AddItem)
                {
                    Next();
                    _player.AddItem(info.Item);
                }

                if (info.DoAction == DoActionType.RemoveItem)
                {
                    if (_player.RemoveItem(info.Item))
                        Next();
                    else
                        TryToStop();
                }

                wasCancelled = true;
                return;
            }

            _view.Show(transform.position, info.Sentence);
        }

        private void Next()
        {
            _dialogueIndex++;
            _dialogueIndex = _dialogueIndex < 0 ? 0 : _dialogueIndex;
            _dialogueIndex = _dialogueIndex >= _config.Count ? _config.Count - 1 : _dialogueIndex;
            _view.Hide();
            _config.ResetIndex();
            PlayerPrefs.SetInt(_config.SaveKey, _dialogueIndex);
            PlayerPrefs.Save();
        }

        public void TryToStop()
        {
            _view.Hide();
            _config.ResetIndex();
        }

        private void DoAction()
        {
            if (_actionIndex < 0 && _actionIndex >= _actions.Count)
                return;

            _actions[_actionIndex].DoDialogueAction();
            _actionIndex++;
            Next();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_dialogueActions == null)
                return;

            for (int i = 0; i < _dialogueActions.Length; i++)
            {
                if (_dialogueActions[i] is IDialogueAction || _dialogueActions[i] == null)
                    continue;

                _dialogueActions[i] = null;
                Debug.LogWarning($"{_dialogueActions[i]} doesn't implement {typeof(IDialogueAction)}");
            }
        }
#endif
    }
}