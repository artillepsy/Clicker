using Constants;
using Leopotam.Ecs;
using Saves.Components;
using TimeScale.SceneData;
using UnityEngine;

namespace TimeScale.Systems
{
    public class InitTimeScaleSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly TimeScaleCanvas _timeScaleCanvas = null;
        private readonly EcsFilter<GameStateSaveData> _saveDataFilter = null;
        private readonly EcsFilter<Components.TimeScale> _timeScaleFilter = null;
        
        public void Init()
        {
            var entity = _world.NewEntity();
            var data = _saveDataFilter.Get1(0);
            ref var timeScale = ref entity.Get<Components.TimeScale>();
            
            timeScale.value = data.timeScale;
            timeScale.speedLabel = _timeScaleCanvas.speedLabel;
            timeScale.reduceButon = _timeScaleCanvas.reduceButon;
            timeScale.incrementButon = _timeScaleCanvas.incrementButon;

            timeScale.reduceButon.onClick.AddListener(OnClickReduceTimeScale);
            timeScale.incrementButon.onClick.AddListener(OnClickIncrementTimeScale);
            
            UpdateScene(ref timeScale);
        }

        private void OnClickReduceTimeScale()
        {
            ref var timeScale = ref _timeScaleFilter.Get1(0);
            
            if (timeScale.value == 1) return;
            
            timeScale.value--;
            UpdateScene(ref timeScale);
        }

        private void OnClickIncrementTimeScale()
        {
            ref var timeScale = ref _timeScaleFilter.Get1(0);
            
            if (timeScale.value == 99) return;
            
            timeScale.value++;
            UpdateScene(ref timeScale);
        }

        private void UpdateScene(ref Components.TimeScale timeScale)
        {
            Time.timeScale = timeScale.value;
            timeScale.speedLabel.text = Literals.GetSpeedLabel(timeScale.value);
        }
    }
}