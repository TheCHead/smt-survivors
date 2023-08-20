using System;
using UnityEngine;

namespace TPCore.Ecs.Components
{
    [Serializable]
    public struct TransformComponent
    {
        public Transform BaseTf;
        public Transform BodyTf;
    }
}

