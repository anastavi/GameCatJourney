using UnityEngine;
using GameContracts;

public class CatFisher : MonoBehaviour, IDialogueAction
{
    private ISceneLoader _sceneLoader;

    public void DoDialogueAction()
    {
        _sceneLoader.Load(Scenes.Fishing);
    }

    public void Init(ISceneLoader loader)
    {
        _sceneLoader = loader;        
    }
}
