
using System;
using Scripts.Ecs.Utility;
using UnityEngine.Serialization;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct InitEntityReferenceRequest
    {
        public EntityReference Reference;
    }
}