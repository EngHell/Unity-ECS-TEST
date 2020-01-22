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
                    switch (bulletAuthoring.type)
                    {
                        case Mono.BulletConvertToEntityAuthoring.TYPE.LINEAR:
                            ShootLinearBullet(ref bulletAuthoring, vectorToMouse.Value, ref location);
                            break;
                        case Mono.BulletConvertToEntityAuthoring.TYPE.PARABOLIC:
                            ShootParabolicBullet(ref bulletAuthoring, vectorToMouse.Value, ref location);
                            break;
                    }
                });
            }
        }

        void ShootLinearBullet(ref Components.Prefabs.BulletAuthoring bulletAuthoring, float3 vectorToMouse, ref LocalToWorld location)
        {
            var instance = EntityManager.Instantiate(bulletAuthoring.prefabEntity);
            float3 vel = math.normalize(vectorToMouse) * bulletAuthoring.speed;
            float time = bulletAuthoring.attackRange / bulletAuthoring.speed;


            EntityManager.SetComponentData(instance, new Translation { Value = location.Position });
            EntityManager.SetComponentData(instance, new PhysicsVelocity { Linear = vel });
            EntityManager.SetComponentData(instance, new Components.LifeTime { Value = time, Counting = true });
        }

        void ShootParabolicBullet(ref Components.Prefabs.BulletAuthoring bulletAuthoring, float3 vectorToMouse, ref LocalToWorld location)
        {
            var instance = EntityManager.Instantiate(bulletAuthoring.prefabEntity);        

            float distance = Vector3.Magnitude(vectorToMouse);
            distance = math.clamp(distance, 0, bulletAuthoring.attackRange);
            float timeInFly = (distance * bulletAuthoring.maxTimeInFly) / bulletAuthoring.attackRange;
            float horizontalSpeedMultiplier = distance / timeInFly;

            float3 vel = math.normalize(vectorToMouse);
            vel *= horizontalSpeedMultiplier;
            vel.y = (0.0f - location.Position.y - ((0.5f) * (Physics.gravity.y) * (math.pow(timeInFly, 2.0f)))) / timeInFly;


            //float time = bulletAuthoring.attackRange / Vector2.SqrMagnitude(new Vector2(vel.x, vel.z));

            /*
            Debug.Log(string.Format("-------\n"+
                "spawnPos:{9} vectorToMouse:{0}, targetPoint:{1}, \nhorizontalDir:{2} clampedDist:{3}, \nt:{4}, horVel:{5} \nYf:{6}, Y0:{7}, g:{8}, \nBodyVel{10}",
                vectorToMouse, targetPoint,horizontalDirection,distance,timeInFly,
                horizontalSpeedMultiplier, location.Position.y, 0.0f,
                Physics.gravity.y, location.Position, vel));
                */

            EntityManager.SetComponentData(instance, new Translation { Value = location.Position });
            EntityManager.SetComponentData(instance, new PhysicsVelocity { Linear = vel });
            EntityManager.SetComponentData(instance, new Components.LifeTime { Value = timeInFly, Counting = true });
            EntityManager.SetComponentData(instance, new PhysicsGravityFactor { Value = 1.0f });
        }
    }
}
