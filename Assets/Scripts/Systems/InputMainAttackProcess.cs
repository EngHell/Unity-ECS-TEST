using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;

namespace Systems
{
    [UpdateAfter(typeof(Input))]
    public class InputMainAttackProcess : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var input = this.GetSingleton<Components.InputSingleton>();

            if (input.fire1)
            {
                Entities.ForEach((ref Components.Prefabs.BulletAuthoring bulletAuthoring, ref LocalToWorld location, ref Components.VectorToMouse vectorToMouse) =>
                {
                    var instance = EntityManager.Instantiate(bulletAuthoring.prefabEntity);
                    float3 vel = math.normalize(vectorToMouse.Value) * bulletAuthoring.speed;
                    Debug.Log(string.Format("vel: {0}", vel));
                    float time = bulletAuthoring.attackRange / bulletAuthoring.speed;
                    

                    EntityManager.SetComponentData(instance, new Translation { Value = location.Position });
                    EntityManager.SetComponentData(instance, new PhysicsVelocity { Linear = vel });
                    EntityManager.SetComponentData(instance, new Components.LifeTime { Value = time, Counting = true });
                });
            }
        }
    }
}
