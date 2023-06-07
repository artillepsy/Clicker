using Leopotam.Ecs;
using Saves.Components;
using TimeScale.SceneData;
using UnityEngine;
using Utils;

namespace TimeScale.Systems
{
    /// Changes time speed after clicking on certain buttons
    public class InitTimeScaleSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly TimeScaleCanvas _timeScaleCanvas = null;
        private readonly EcsFilter<GameStateSaveData> _saveDataFilter = null;
        private readonly EcsFilter<Components.TimeScale> _timeScaleFilter = null;
        
        /// Initialises timeScale component ad the start and adds listeners to buttons
        public void Init()
        {
            var entity = _world.NewEntity();
            var data = _saveDataFilter.Get1(0);
            ref var timeScale = ref entity.Get<Components.TimeScale>();
            
            timeScale.value = data.timeScale;
            timeScale.speedLabel = _timeScaleCanvas.speedLabel;
            timeScale.reduceButon = _timeScaleCanvas.reduceButon;
            timeScale.increaseButon = _timeScaleCanvas.increaseButon;

            timeScale.reduceButon.onClick.AddListener(OnClickReduceTimeScale);
            timeScale.increaseButon.onClick.AddListener(OnClickIncreaseTimeScale);
            
            UpdateScene(ref timeScale);
        }

        /// Calls after clicking on reduce time speed button
        private void OnClickReduceTimeScale()
        {
            ref var timeScale = ref _timeScaleFilter.Get1(0);
            
            if (timeScale.value == 1) return;
            
            timeScale.value--;
            UpdateScene(ref timeScale);
        }

        /// Calls after clicking on increase time speed button
        private void OnClickIncreaseTimeScale()
        {
            ref var timeScale = ref _timeScaleFilter.Get1(0);
            
            if (timeScale.value == 99) return;
            
            timeScale.value++;
            UpdateScene(ref timeScale);
        }

        /// Updates scene values
        private void UpdateScene(ref Components.TimeScale timeScale)
        {
            Time.timeScale = timeScale.value;
            timeScale.speedLabel.text = Literals.GetSpeedLabel(timeScale.value);
        }
    }
}