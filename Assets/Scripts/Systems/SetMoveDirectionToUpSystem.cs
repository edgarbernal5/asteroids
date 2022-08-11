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
                in Rotation _rotation) =>
            {
                var direction = math.mul(_rotation.Value, math.up());
                movementCommands.m_currentDirectionOfMove = direction;

            }).Schedule();
        }
    }
}