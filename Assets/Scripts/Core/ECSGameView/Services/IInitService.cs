namespace Core.ECSGameView.Services
{
    public interface IInitService
    {
        bool IsInited { get; }
        void Init();
    }
}