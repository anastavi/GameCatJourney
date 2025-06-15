using UnityEngine;

namespace GameContracts
{
    public interface ISaveService
    {
        public void Save();
        public void Load();
    }
}