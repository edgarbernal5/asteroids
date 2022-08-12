using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    public struct InputComponent : IComponentData
    {
        public bool m_inputLeft;
        public bool m_inputRight;
        public bool m_inputForward;
        public bool m_inputShoot;
    }

}