using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public class VectorToMouse : JobComponentSystem
    {
        // Start is called before the first frame update
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float3 mousePos = UnityEngine.Input.mousePosition;
            //Debug.Log("mousePos:" + mousePos);
            //mousePos.z = 10;
            float3 mouseInWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        
            var jobHandle = Entities
                .ForEach((ref Components.VectorToMouse vectorToMouse,
                          in LocalToWorld location) =>
                {
                    float3 mouseAtBodyHeight = mouseInWorld;
                    mouseAtBodyHeight.y = location.Position.y;

                    vectorToMouse.Value = mouseAtBodyHeight - location.Position;
                })
                .Schedule(inputDeps);

            return jobHandle;
        }
    }
}
