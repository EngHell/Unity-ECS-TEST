using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    [Serializable]
    public struct LifeTime : IComponentData
    {
        public float Value;
        public bool Counting;


    }
}

namespace Mono
{
    public class LifeTimeComponent : ComponentDataProxy<Components.LifeTime> { }
}