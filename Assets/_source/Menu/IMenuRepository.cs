using GameContracts;

namespace GameMenu
{
    public interface IMenuRepository
    {
        public Scenes SceneToLoad { get; set; }
    }
}