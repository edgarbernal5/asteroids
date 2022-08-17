using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Asteroids.Scripts.Systems
{
    public class OffScreenWrappingSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var screenDataComponent = GetSingleton<ScreenInfoComponent>();

            Entities.WithAll<OffScreenWrapperComponent>().ForEach((
                Entity entity, ref OffScreenWrapperComponent offScreenWrapperComponent, ref Translation translation) =>
            {
                if (offScreenWrapperComponent.m_isOffScreenLeft)
                {
                    translation.Value = SpawnOnRightSide(translation.Value, offScreenWrapperComponent.m_bounds, screenDataComponent);
                }
                else if (offScreenWrapperComponent.m_isOffScreenRight)
                {
                    translation.Value = SpawnOnLeftSide(translation.Value, offScreenWrapperComponent.m_bounds, screenDataComponent);
                }
                else if (offScreenWrapperComponent.m_isOffScreenUp)
                {
                    translation.Value = SpawnOnBottomSide(translation.Value, offScreenWrapperComponent.m_bounds, screenDataComponent);
                }
                else if (offScreenWrapperComponent.m_isOffScreenDown)
                {
                    translation.Value = SpawnOnTopSide(translation.Value, offScreenWrapperComponent.m_bounds, screenDataComponent);
                }

                offScreenWrapperComponent.m_isOffScreenDown = false;
                offScreenWrapperComponent.m_isOffScreenRight = false;
                offScreenWrapperComponent.m_isOffScreenUp = false;
                offScreenWrapperComponent.m_isOffScreenLeft = false;

            }).ScheduleParallel();
        }

        private static float3 SpawnOnRightSide(float3 position, float bounds, ScreenInfoComponent screenDataComponent)
        {
            return new float3((bounds + screenDataComponent.m_width) * 0.5f, position.y, 0); ;
        }
        
        private static float3 SpawnOnLeftSide(float3 position, float bounds, ScreenInfoComponent screenDataComponent)
        {
            return new float3(-(bounds + screenDataComponent.m_width) * 0.5f, position.y, 0); ;
        }
        
        private static float3 SpawnOnTopSide(float3 position, float bounds, ScreenInfoComponent screenDataComponent)
        {
            return new float3(position.x, (bounds + screenDataComponent.m_height) * 0.5f, 0);
        }
        
        private static float3 SpawnOnBottomSide(float3 position, float bounds, ScreenInfoComponent screenDataComponent)
        {
            return new float3(position.x, -(bounds + screenDataComponent.m_height) * 0.5f, 0);
        }
    }

}
