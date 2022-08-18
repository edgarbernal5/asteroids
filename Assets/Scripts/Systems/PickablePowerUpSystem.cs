using System.Diagnostics;
using Asteroids.Scripts.Components;
using Unity.Entities;

namespace Assets.Scripts.Systems
{
    public class PickablePowerUpSystem : SystemBase
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
                .WithAll<PickablePowerUpComponent>()
                .ForEach((
                    Entity entity,
                    in DestroyableComponent destroyable) =>
                {
                    if (destroyable.m_mustBeDestroyed)
                    {
                        //UnityEngine.Debug.Log($"pickable power up. Entity {entity.Index}");
                        m_entityManager.DestroyEntity(entity);
                    }

                }).Run();
        }
    }
}