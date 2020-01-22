using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Components.Prefabs
{
    [GenerateAuthoringComponent]
    public struct BulletAuthoring :IComponentData
    {
        public Entity prefabEntity;
        public float speed;
        public float attackRange;
        public Mono.BulletConvertToEntityAuthoring.TYPE type;
        public float maxTimeInFly;
    }
}