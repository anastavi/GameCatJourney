namespace GameContracts
{
    public interface ILoadingScreen
    {
        public bool InProgress { get; }
        public void Show();
        public void Hide();
        public void BlinkAction();
    }
}