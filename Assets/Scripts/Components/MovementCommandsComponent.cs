using Unity.Mathematics;
using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct MovementCommandsComponent : IComponentData
    {
        public float3 m_currentMoveDirection;
        public float3 m_currentAngularCommand;
        public float m_currentLinearCommand;
        public float3 m_previousPosition;
    }
}