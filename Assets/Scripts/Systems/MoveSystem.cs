using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;

namespace Asteroids.Scripts.Systems
{
    public class MoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                ref MovementCommandsComponent movementCommands, ref PhysicsVelocity velocity,
                in MovementParametersComponent movementParameters,
                in PhysicsMass physicsMass, in Translation translation) =>
            {
                PhysicsComponentExtensions.ApplyLinearImpulse(
                    ref velocity,
                    physicsMass,
                    movementCommands.m_currentMoveDirection *
                    movementCommands.m_currentLinearCommand *
                    movementParameters.m_linearVelocity
                );

                if (math.length(velocity.Linear) > movementParameters.m_maxLinearVelocity)
                {
                    velocity.Linear = math.normalize(velocity.Linear) * movementParameters.m_maxLinearVelocity;
                }
            }).ScheduleParallel();
        }
    }
}