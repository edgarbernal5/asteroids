using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    public struct OffScreenWrapperComponent : IComponentData
    {
        public float m_bounds;

        public bool m_isOffScreenLeft;
        public bool m_isOffScreenRight;
        public bool m_isOffScreenDown;
        public bool m_isOffScreenUp;
    }
}
