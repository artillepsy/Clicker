using Business.Components;
using Business.Flags;
using Business.Reactive;
using Leopotam.Ecs;
using UnityEngine;

namespace Business.Systems
{
    public class BusinessEarnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EarnTimer, EarnProgressBar, Earn, PurchasedMarker> _businessFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;

        public void Run()
        {
            foreach (var i in _businessFilter)
            {
                ref var timer = ref _businessFilter.Get1(i);
                ref var progressBar = ref _businessFilter.Get2(i);
                ref var earn = ref _businessFilter.Get3(i);

                timer.currentTime += Time.deltaTime;
                progressBar.fillImage.fillAmount = timer.currentTime / timer.earnTime;
                
                if (!(timer.currentTime >= timer.earnTime)) continue;

                progressBar.fillImage.fillAmount = 0f;
                timer.currentTime = 0f;

                foreach (var j in _balanceFilter)
                {
                    ref var earnedEvent = ref _balanceFilter.GetEntity(j).Get<EarnedMoneyEvent>();
                    earnedEvent.moneyToAdd = earn.earn;
                }
            }
        }
    }
}