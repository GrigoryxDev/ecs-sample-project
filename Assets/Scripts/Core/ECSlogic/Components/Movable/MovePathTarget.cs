using System.Collections.Generic;
using Core.ECSlogic.Extensions;

namespace Core.ECSlogic.Components
{
    public struct MovePathTarget
    {
        public List<Vector2Int> Path;
        public int CurrentIndex;
    }
}