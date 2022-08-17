using Unity.Entities;

namespace Asteroids.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct PickablePowerUpComponent : IComponentData
    {
        public PowerUpType m_powerUpType;
    }

    public enum PowerUpType
    {
        Rambo,
        God
    }
}