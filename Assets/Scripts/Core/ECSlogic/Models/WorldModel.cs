using System.Collections.Generic;
using Core.ECSlogic.Extensions;
using Leopotam.EcsLite;

namespace Core.ECSlogic.Models
{
    public class WorldModel
    {
        public bool IsNeedRestart { get; set; }
        public bool IsInited { get; set; }
        public EcsWorld World { get; set; }

        public WorldModel(bool isNeedRestart, EcsWorld world)
        {
            IsNeedRestart = isNeedRestart;
            World = world;
        }
    }

}
