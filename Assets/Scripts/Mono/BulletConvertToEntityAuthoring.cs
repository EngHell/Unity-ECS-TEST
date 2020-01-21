using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Mono
{
    [RequiresEntityConversion]
    public class BulletConvertToEntityAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject bulletPrefab;
        public float speed = 10.0f;
        public float maxDistanceTraveled = 20.0f;

        // here we get an entity conversion at runtime time, then we add this entity to the entity list, with a specific component just for this prefab :D
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var entityPrefabData = new Components.Prefabs.BulletAuthoring
            {
                prefabEntity = conversionSystem.GetPrimaryEntity(bulletPrefab),
                speed = speed,
                attackRange = maxDistanceTraveled
            };

            dstManager.AddComponentData(entity, entityPrefabData);                

        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(bulletPrefab);
        }
    }

}