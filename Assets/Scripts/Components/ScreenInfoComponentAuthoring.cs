using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Asteroids.Scripts.Components
{
    public class ScreenInfoComponentAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private Camera m_camera;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var bottomLeftCornerPosition = m_camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f));
            var topRightCornerPosition = m_camera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f));

            var widthOfScreen = topRightCornerPosition.x - bottomLeftCornerPosition.x;
            var heightOfScreen = topRightCornerPosition.y - bottomLeftCornerPosition.y;
            var size = new float2(widthOfScreen, heightOfScreen);

            dstManager.AddComponentData(entity, new ScreenInfoComponent()
            {
                m_width = size.x,
                m_height = size.y
            });
        }
    }
}
