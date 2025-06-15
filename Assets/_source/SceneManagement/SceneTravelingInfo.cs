using UnityEngine;
using GameContracts;

namespace GameSceneManagement
{
    [CreateAssetMenu(fileName = nameof(SceneTravelingInfo), menuName = "SceneData/TravelingInfo")]
    public class SceneTravelingInfo : ScriptableObject
    {
        public Scenes TravelFromScene;

        public void ResetInfo()
        {
            TravelFromScene = Scenes.None;
        }
    }
}