namespace GameContracts
{
    public interface IGlobalServices
    {
        public ILoadingScreen LoadingScreen { get; }
        public ISceneLoader SceneLoader { get; }
        public IInput Input { get; }
        public IInterfaceInput InterfaceInput { get; }
    }
}