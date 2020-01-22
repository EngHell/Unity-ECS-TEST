using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Mono
{
    [RequiresEntityConversion]
    public class BulletConvertToEntityAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public enum TYPE
        {
            LINEAR,
            PARABOLIC
        }
        [Header("Linear/Parabollic Bullet Settings")]
        public TYPE type = TYPE.LINEAR;
        public GameObject bulletPrefab;
        public float attackRange = 20.0f;

        [Header("Linear Settings")]
        public float speed = 10.0f;

        [Header("Parabollic Settings")]
        public float maxTimeInFly = 2.0f;
        

        // here we get an entity conversion at runtime time, then we add this entity to the entity list, with a specific component just for this prefab :D
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var entityPrefabData = new Components.Prefabs.BulletAuthoring
            {
                prefabEntity = conversionSystem.GetPrimaryEntity(bulletPrefab),
                speed = speed,
                attackRange = attackRange,
                type = type,
                maxTimeInFly = maxTimeInFly
            };

            dstManager.AddComponentData(entity, entityPrefabData);                

        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(bulletPrefab);
        }
    }

}