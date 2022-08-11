using Unity.Mathematics;
using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct MovementParametersComponent : IComponentData
    {
        public float m_linearVelocity;
        public float m_maxLinearVelocity;
        public float m_angularVelocity;
        public float m_maxAngularVelocity;
    }
}