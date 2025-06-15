namespace GameContracts
{
    public interface ISceneLoader
    {
        public string CurrentSceneName { get; }
        public void Load(Scenes scene, bool allowReloadScene = false);
    }

    public enum Scenes
    {
        None,
        MainMenu,
        VillageScene,
        MeadowScene,
        Boot,
        MountainScene,
        Fishing,
        FinalScene,
        Maze_1,
        Maze_2,
        Maze_3,
    }
}