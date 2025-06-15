using UnityEngine;
using GameMenu;
using GameContracts;

namespace GameRoot
{
    public class SaveData : IMenuRepository
    {
        private const string SAVE_KEY = "SaveKey";

        private readonly SaveLoadService _saveService;

        public SaveData() => _saveService = new();

        public Scenes SceneToLoad { get; set; }

        public void Save() => _saveService.Save(SAVE_KEY, this);

        public void Load()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY) == false)
                return;

            var data = _saveService.Load<SaveData>(SAVE_KEY);
            SetData(data);
        }

        private void SetData(SaveData data)
        {
            SceneToLoad = data.SceneToLoad;
        }

        private class SaveLoadService
        {
            internal void Save<T>(string key, T saveData)
            {
                string jsonDataString = JsonUtility.ToJson(saveData, true);
                PlayerPrefs.SetString(key, jsonDataString);
            }

            internal T Load<T>(string key)
            {
                string loadedString = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(loadedString);
            }
        }
    }
}