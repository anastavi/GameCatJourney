namespace GameContracts
{
    public interface IPlayer
    {
        public void Init(IInput input);
        public void AddItem(Items item);
        public bool RemoveItem(Items item);
    }
}