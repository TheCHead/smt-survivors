using System;
using DG.Tweening;

namespace TPCore.Ecs.Components
{
    [Serializable]
    public struct EnemyMoverComponent
    {
        public float Speed;
        public float ReachTime;
        public Tween ReachTween;
    }
}

