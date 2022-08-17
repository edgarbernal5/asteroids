using Asteroids.Scripts.Components;
using Unity.Entities;

namespace Asteroids.Scripts.Systems
{
    public class PlayerDestructionSystem : SystemBase
    {
        private EntityManager m_entityManager;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            m_entityManager = World.EntityManager;
        }

        protected override void OnUpdate()
        {
            Entities.WithStructuralChanges().WithoutBurst().WithAll<PlayerTagComponent>()
                .ForEach((Entity entity, ref DestroyableComponent destroyableComponent) =>
                    {
                        if (destroyableComponent.m_mustBeDestroyed)
                        {
                            m_entityManager.DestroyEntity(entity);
                            SceneInitialization.m_instance.CheckAndSpawnPlayer();
                        }
                    }
                ).Run();
        }
    }
}