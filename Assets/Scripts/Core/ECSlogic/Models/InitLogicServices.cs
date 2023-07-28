using Core.ECSlogic.Interfaces.Services;

namespace Core.ECSlogic.Models
{
    public class InitLogicServices
    {
        public IReadOnlyGameModel ReadOnlyGameModel { get; }
        public IRandomService RandomService { get; }
        public IQuestDynamicService QuestDynamicService { get; }
        public IElementAnalyzeService ElementAnalyzeService { get; }
        public IMovablePositionsService MovablePositionsService { get; }
        public IElementStaticService ElementStaticService { get; }

        public InitLogicServices(IRandomService randomService,
        IQuestDynamicService questDynamicService,
        IElementAnalyzeService elementAnalyzeService,
        IReadOnlyGameModel readOnlyGameModel,
        IMovablePositionsService movablePositionsService,
        IElementStaticService elementStaticService)
        {
            RandomService = randomService;
            QuestDynamicService = questDynamicService;
            ElementAnalyzeService = elementAnalyzeService;
            ReadOnlyGameModel = readOnlyGameModel;
            MovablePositionsService = movablePositionsService;
            ElementStaticService = elementStaticService;
        }
    }

}
