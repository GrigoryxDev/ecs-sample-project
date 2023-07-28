using Leopotam.EcsLite;

namespace Core.ECSlogic.Extensions
{
    public static class EcsFilterExtension
    {
        public static bool TryGetAnyEntity(this EcsFilter filter, out int entity)
        {
            entity = -1;

            foreach (var item in filter)
            {
                entity = item;
                return true;
            }

            return false;
        }
    }
}
