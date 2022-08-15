using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct DestroyableComponent : IComponentData
    {
        public bool m_mustBeDestroyed;
    }
}
