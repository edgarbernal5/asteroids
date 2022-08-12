using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Components
{
    public class OffScreenWrapperComponentAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private MeshRenderer m_mesh;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new OffScreenWrapperComponent
            {
                m_bounds = m_mesh.bounds.extents.magnitude
            });
        }
    }
}
