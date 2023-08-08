using System;
using UnityEngine;

namespace Scripts.Ecs.Components
{
    [Serializable]
    public struct TransformComponent
    {
        public Transform BaseTf;
        public Transform BodyTf;
    }
}