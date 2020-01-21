using Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    [UpdateAfter(typeof(VectorToMouse))]
    public class RotateAtMouse : JobComponentSystem
    {
        [BurstCompile]
        public struct RotateAtMouseSystemJob : IJobForEach<Rotation, Components.VectorToMouse, Components.LookAtMouse>
        {
            public void Execute(ref Rotation rot, ref Components.VectorToMouse vectorToMouse, [ReadOnly]ref Components.LookAtMouse empty)
            {
                rot.Value = Quaternion.LookRotation(vectorToMouse.Value, Vector3.up);
            }
        }
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new RotateAtMouseSystemJob();

            return job.Schedule(this, inputDeps);
        }
    }
}
