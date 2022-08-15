﻿using Asteroids.Scripts.Components;
using Unity.Entities;

namespace Assets.Scripts.Systems
{
    public class BulletDestructionSystem : SystemBase
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
                .WithAll<ProjectileTagComponent>()
                .ForEach((Entity entity, in DestroyableComponent _destroyable) =>
            {
                if (_destroyable.m_mustBeDestroyed)
                { 
                    m_entityManager.DestroyEntity(entity); 
                }

            }).Run();
        }
    }
}