using Asteroids.Scripts.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Asteroids.Scripts.Systems
{
    public class PlayerControlSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var query = GetEntityQuery(typeof(InputComponent));
            var array = query.ToComponentDataArray<InputComponent>(Allocator.TempJob);

            if (array.Length == 0)
            {
                array.Dispose();
                return;
            }
            var inputData = array[0];

            Entities.WithAll<PlayerTagComponent>().ForEach((ref MovementCommandsComponent commands, ref WeaponComponent weapon) =>
            {
                var turningLeft = inputData.m_inputLeft ? 1 : 0;
                var turningRight = inputData.m_inputRight ? 1 : 0;

                var rotationDirection = turningLeft - turningRight;

                weapon.m_isFiring = inputData.m_inputShoot;

                commands.m_currentAngularCommand = new float3(0, 0, rotationDirection);
                commands.m_currentLinearCommand = inputData.m_inputForward ? 1: 0;

            }).ScheduleParallel();

            array.Dispose();
        }
    }
}
