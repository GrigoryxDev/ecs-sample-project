using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.ECSlogic.Systems
{
    public class QuestObservableSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RequestNewQuest>> requestNewQuestFilter;
        private readonly EcsPoolInject<ExpiredTag> expiredTagPool;
        private readonly EcsCustomInject<IQuestDynamicService> questDynamicService;
        private readonly EcsCustomInject<IReadOnlyGameModel> readOnlyGameModel;

        public void Run(IEcsSystems systems)
        {
            foreach (var item in requestNewQuestFilter.Value)
            {
                questDynamicService.Value.GetNextQuest();
                expiredTagPool.Value.GetOrAdd(item);
            }

            CheckCompletedQuest();
        }

        private void CheckCompletedQuest()
        {
            if (readOnlyGameModel.Value.GetCurrentQuest.Value != null)
            {
                if (readOnlyGameModel.Value.GetCurrentQuest.Value.IsCompleted())
                {
                    questDynamicService.Value.GetNextQuest();
                }
            }
        }
    }
}