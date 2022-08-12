using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    public struct ScreenInfoComponent : IComponentData
    {
        public float m_width;
        public float m_height;
    }
}
