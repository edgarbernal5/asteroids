using Asteroids.Scripts;
using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class AsteroidFactoryAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public SceneInitialization m_initializator;
        public GameObject[] m_asteroidsPrefabs;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var buffer = dstManager.AddBuffer<EntityBufferElement>(entity);
            foreach (var asteroid in m_asteroidsPrefabs)
            {
                buffer.Add(new EntityBufferElement()
                {
                    m_entity = conversionSystem.GetPrimaryEntity(asteroid)
                });
            }

            m_initializator.m_asteroidLibrary = entity;
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.AddRange(m_asteroidsPrefabs);
        }
    }

    public struct EntityBufferElement : IBufferElementData
    {
        public Entity m_entity;
    }
}