using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct LifeTimeComponent : IComponentData
    {
        public float m_currentLifeTime;
        public float m_maxLifeTime;
    }
}
