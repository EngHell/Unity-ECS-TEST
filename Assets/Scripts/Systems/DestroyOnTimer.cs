using UnityEngine;
using Unity.Physics;
using Unity.Entities;
using Unity.Jobs;
using Components;
using Unity.Collections;
using Unity.Physics.Systems;
using Unity.Burst;

namespace Systems
{
    public class DestroyOnTimer : ComponentSystem
    {

        [BurstCompile]
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities.ForEach((Entity ent, ref LifeTime time) =>
            {
                if (time.Counting)
                {
                    if (time.Value <= 0)

                        EntityManager.DestroyEntity(ent);
                    else
                    {
                        //Debug.Log("Current: " + time.Value + " delta:" + deltaTime);
                        time.Value -= deltaTime;
                    }
                }
            });
        }
    }

}