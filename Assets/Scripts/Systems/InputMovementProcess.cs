using UnityEngine;
using System.Collections;
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
 
namespace Systems
{

    [UpdateAfter(typeof(Input))]
    public class InputMovementProcess : ComponentSystem
    { 

        protected override void OnUpdate()
        {
            var input = this.GetSingleton<Components.InputSingleton>();

            Entities.ForEach((ref Speed speed, ref Components.Character player) =>
            {
                speed.Value.x = player.moveSpeed * input.horizontal;
                speed.Value.z = player.moveSpeed * input.vertical;
            });
        }
    }


}