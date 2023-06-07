using Business.Components;
using Constants;
using Leopotam.Ecs;
using Saves.Components;
using Saves.SceneData;
using Saves.Utils;
using UnityEngine;

namespace Saves.Systems
{
    public class SaveSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly ApplicationFocusCatcher _focusCatcher = null;
        private readonly EcsFilter<GameStateSaveData> _saveDataFilter = null;
        private readonly EcsFilter<BusinessLevel> _filter = null;
        
        public void Init()
        {
            _focusCatcher.OnApplicationUnfocused += OnUnfocused;
        }

        public void Destroy()
        {
            _focusCatcher.OnApplicationUnfocused -= OnUnfocused;
        }

        private void OnUnfocused()
        {
          //  SaveLoadUtils.Save(data, Literals.SaveFileName);
          Debug.Log("Save data imitation");
        }

        private void UpdateSaveData()
        {
           // var data = _filter.Get1(0);
            
            
            
        }
    }
}