using UnityEngine;
using GameMenu;
using GameSceneManagement;

namespace GameRoot
{
    public class MainMenuRoot : CompositeRoot
    {
        [SerializeField] private MainMenuButtons _mainMenuButtons;
        [SerializeField] private SceneTravelingInfo _travelingInfo;

        private void OnEnable()
        {
            _travelingInfo.ResetInfo();
        }

        internal override void Init()
        {
            _mainMenuButtons.Init(GlobalServices.SceneLoader, GlobalServices.InterfaceInput, GlobalServices.SaveService.SceneToLoad);
        }
    }
}