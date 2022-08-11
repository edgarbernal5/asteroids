using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Transforms;

namespace Asteroids.Scripts.Systems
{
    public class RotateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((
                ref PhysicsVelocity velocity, ref MovementCommandsComponent movementCommands,
                ref Rotation rotation, in MovementParametersComponent movementParameters, in PhysicsMass physicsMass) =>
            {
                PhysicsComponentExtensions.ApplyAngularImpulse(
                    ref velocity, physicsMass,
                    movementCommands.m_currentAngularCommand * movementParameters.m_angularVelocity);

                var currentAngularSpeed = PhysicsComponentExtensions.GetAngularVelocityWorldSpace(in velocity, in physicsMass, in rotation);

                if (math.length(currentAngularSpeed) > movementParameters.m_maxAngularVelocity)
                {
                    PhysicsComponentExtensions.SetAngularVelocityWorldSpace(ref velocity, physicsMass, rotation,
                        math.normalize(currentAngularSpeed) * movementParameters.m_maxAngularVelocity);
                }

            }).Schedule();
        }
    }
}
