using UnityEngine;
using GameContracts;

namespace GameRoot
{
    [CreateAssetMenu(fileName = nameof(GlobalServices), menuName = "Services/" + nameof(GlobalServices))]
    public class GlobalServices : ScriptableObject, IGlobalServices
    {
        public ILoadingScreen LoadingScreen { get; internal set; }
        public ISceneLoader SceneLoader { get; internal set; }
        public IInput Input { get; internal set; }
        public IInterfaceInput InterfaceInput { get; internal set; }
        public SaveData SaveService { get; internal set; }
    }
}