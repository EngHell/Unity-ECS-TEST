using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct VectorToMouse : IComponentData
    {
        public float3 Value;
    }
}
