using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Systems
{
    [AlwaysUpdateSystemAttribute]
    public class Input : ComponentSystem
    {
        protected override void OnCreate()
        {
            var inputSingleton = new Components.InputSingleton { fire1 = false, fire2 = false, fire3 = false, horizontal = .0f, vertical = .0f };
            EntityManager.CreateEntity(ComponentType.ReadOnly<Components.InputSingleton>());
            this.SetSingleton<Components.InputSingleton>(inputSingleton);
        }

        protected override void OnUpdate()
        {
            var inputSingleton = new Components.InputSingleton { fire1 = false, fire2 = false, fire3 = false, horizontal = .0f, vertical = .0f };

            inputSingleton.horizontal = UnityEngine.Input.GetAxis("Horizontal");
            inputSingleton.vertical = UnityEngine.Input.GetAxis("Vertical");

            this.SetSingleton<Components.InputSingleton>(inputSingleton);

            if (UnityEngine.Input.GetButtonDown("Fire1"))
                inputSingleton.fire1 = true;
            else
                inputSingleton.fire1 = false;
        }
    }
}


