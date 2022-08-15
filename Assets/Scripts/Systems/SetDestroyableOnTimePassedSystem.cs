using Asteroids.Scripts.Components;
using Unity.Entities;

namespace Asteroids.Scripts.Systems
{
    public class SetDestroyableOnTimePassedSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            Entities.ForEach((Entity entity, ref LifeTimeComponent lifeTime, ref DestroyableComponent destroyableComponent) =>
            {
                lifeTime.m_currentLifeTime += deltaTime;

                if (lifeTime.m_currentLifeTime >= lifeTime.m_maxLifeTime && !destroyableComponent.m_mustBeDestroyed)
                {
                    destroyableComponent.m_mustBeDestroyed = true;
                }
            }).ScheduleParallel();
        }
    }
}
