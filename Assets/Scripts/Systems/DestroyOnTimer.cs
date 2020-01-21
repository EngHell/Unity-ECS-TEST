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
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class DestroyOnTimer : ComponentSystem
    {
        EndSimulationEntityCommandBufferSystem endSimulationCBS;
        public struct DestroyOnTimerJob : IJobForEachWithEntity<Components.LifeTime>
        {
            public EntityCommandBuffer cmd;
            public float deltaTime;


            public void Execute(Entity entity, int index, ref LifeTime lifeTime)
            {
                if (lifeTime.Value <= 0)

                    cmd.DestroyEntity(entity);
                else
                {
                    Debug.Log("Current: " + lifeTime.Value + " delta:" + deltaTime);
                    lifeTime.Value -= deltaTime;
                }

            }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            endSimulationCBS = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected JobHandle OnUpdate(JobHandle inputDeps)
        {
            float deltaTime = Time.DeltaTime;
            EntityCommandBuffer cmd = endSimulationCBS.PostUpdateCommands;

            var job = new DestroyOnTimerJob { cmd = cmd, deltaTime = deltaTime };

            var jobHandle = job.Schedule(this, inputDeps);

            endSimulationCBS.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }

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