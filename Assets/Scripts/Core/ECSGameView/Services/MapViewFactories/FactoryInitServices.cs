namespace Core.ECSGameView.Services
{
    public struct FactoryInitServices
    {
        public IElementStaticService ElementStaticService;
        public ISpriteService SpriteService;

        public FactoryInitServices(IElementStaticService elementStaticService,
        ISpriteService spriteService)
        {
            this.ElementStaticService = elementStaticService;
            this.SpriteService = spriteService;
        }
    }
}