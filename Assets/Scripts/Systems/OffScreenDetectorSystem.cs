using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class OffScreenDetectorSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var screenData = GetSingleton<ScreenInfoComponent>();

            Entities.ForEach(
                (Entity _entity, ref OffScreenWrapperComponent offScreen,
                    in MovementCommandsComponent moveComponent, in Translation translation, in PhysicsVelocity velocity) =>
                {
                    var previousPosition = moveComponent.m_previousPosition;
                    var currentPosition = translation.Value;

                    var isMovingLeft = velocity.Linear.x < 0.0f;
                    var isMovingRight = velocity.Linear.x > 0.0f;
                    var isMovingUp = velocity.Linear.y > 0.0f;
                    var isMovingDown = velocity.Linear.y < 0.0f;

                    offScreen.m_isOffScreenLeft = isMovingLeft && translation.Value.x < -(screenData.m_width + offScreen.m_bounds) * 0.5f;
                    offScreen.m_isOffScreenRight = isMovingRight && translation.Value.x > (screenData.m_width + offScreen.m_bounds) * 0.5f;
                    
                    //Debug.Log($"dt.y = {(currentPosition.y - previousPosition.y)} entity = ${_entity.Index}");

                    offScreen.m_isOffScreenUp = isMovingUp && translation.Value.y > (screenData.m_height + offScreen.m_bounds) * 0.5f;
                    offScreen.m_isOffScreenDown = isMovingDown && translation.Value.y < -(screenData.m_height + offScreen.m_bounds) * 0.5f;

                }).ScheduleParallel();
        }
    }
}
