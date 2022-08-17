using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Asteroids.Scripts.Systems
{
    public class WeaponSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem m_endSimulationEntityCommandBufferSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_endSimulationEntityCommandBufferSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            var ecb = m_endSimulationEntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .ForEach((Entity entity, int entityInQueryIndex, ref WeaponComponent weaponComponent,
                in Translation translation, in Rotation rotation) =>
            {
                weaponComponent.m_timer += deltaTime;

                if (!weaponComponent.m_isFiring) return;
                if (!(weaponComponent.m_timer > weaponComponent.m_fireRate)) return;

                weaponComponent.m_timer = 0;
                var newProjectile = ecb.Instantiate(entityInQueryIndex, weaponComponent.m_projectilePrefab);

                ecb.SetComponent(entityInQueryIndex, newProjectile, new Translation
                {
                    Value = translation.Value
                });

                ecb.SetComponent(entityInQueryIndex, newProjectile, new Rotation()
                {
                    Value = rotation.Value
                });

                ecb.SetComponent(entityInQueryIndex, newProjectile, new MovementCommandsComponent()
                {
                    m_currentLinearCommand = 1,
                });
                
            }).ScheduleParallel();
            
            m_endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
