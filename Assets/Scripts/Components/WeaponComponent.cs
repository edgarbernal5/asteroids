using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct WeaponComponent : IComponentData
    {
        public Entity m_projectilePrefab;
        public bool m_isFiring;
        public float m_fireRate;
        public float m_timer;
    }
}
