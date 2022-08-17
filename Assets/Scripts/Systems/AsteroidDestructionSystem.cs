
using Asteroids.Scripts.Components;
using Unity.Entities;

namespace Assets.Scripts.Systems
{
    public class AsteroidDestructionSystem : SystemBase
    {
        private EntityManager m_entityManager;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_entityManager = World.EntityManager;
        }

        protected override void OnUpdate()
        {
            Entities
                .WithoutBurst()
                .WithStructuralChanges()
                .WithAll<AsteroidTagComponent>()
                .ForEach((
                    Entity entity,
                    in DestroyableComponent destroyable) =>
            {
                if (destroyable.m_mustBeDestroyed)
                {
                    m_entityManager.DestroyEntity(entity);
                }

            }).Run();
        }
    }
}