using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPCore.Ecs.Components
{
    [Serializable]
    public struct PlayerInputComponent
    {
        public float HDeltaR;
        public float VDeltaR;
        public bool Dash;
        public bool Block;
    }
}

