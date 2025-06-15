using System;

namespace GameContracts
{
    public interface IConfirmSuggestion
    {
        public void Open(Action confirmAction, Action declineAction);
    }
}