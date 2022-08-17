using System.Collections.Generic;
using Asteroids.Scripts;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Components
{
    public class PlayerFactoryAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        [SerializeField] private SceneInitialization m_sceneInitialization;
        [SerializeField] private GameObject m_playerPrefab;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new PlayerFactoryElementComponent()
            {
                m_player = conversionSystem.GetPrimaryEntity(m_playerPrefab)
            });

            m_sceneInitialization.m_playerEntity = entity;
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(m_playerPrefab);
        }
    }

    public struct PlayerFactoryElementComponent : IComponentData
    {
        public Entity m_player { get; set; }
    }
}