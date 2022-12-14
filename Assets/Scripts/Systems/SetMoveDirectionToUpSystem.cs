using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Asteroids.Scripts.Systems
{
    public class SetMoveDirectionToUpSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<MovingInUpDirectionComponent>().ForEach((
                ref MovementCommandsComponent movementCommands,
                in Rotation rotation) =>
            {
                var direction = math.mul(rotation.Value, math.up());
                movementCommands.m_currentMoveDirection = direction;

            }).Schedule();
        }
    }
}