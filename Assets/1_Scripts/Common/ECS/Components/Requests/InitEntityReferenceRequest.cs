
using System;
using Common.Ecs.Utility;

namespace Common.Ecs.Components
{
    [Serializable]
    public struct InitEntityReferenceRequest
    {
        public EntityReference Reference;
    }
}