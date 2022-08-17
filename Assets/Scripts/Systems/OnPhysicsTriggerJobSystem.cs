﻿using Asteroids.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;


namespace Assets.Scripts.Systems
{
    public class OnPhysicsTriggerJobSystem : JobComponentSystem
    {
        private BuildPhysicsWorld m_physicsWorld;
        private StepPhysicsWorld m_stepPhysicsWorld;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_physicsWorld = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<BuildPhysicsWorld>();
            m_stepPhysicsWorld = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<StepPhysicsWorld>();
        }

        [BurstCompile]
        private struct TriggerJob : ITriggerEventsJob
        {
            public ComponentDataFromEntity<DestroyableComponent> m_destroyables;

            public void Execute(TriggerEvent triggerEvent)
            {
                if (m_destroyables.HasComponent(triggerEvent.EntityA))
                {
                    var destroyable = m_destroyables[triggerEvent.EntityA];
                    destroyable.m_mustBeDestroyed = true;
                    m_destroyables[triggerEvent.EntityA] = destroyable;
                }
                if (m_destroyables.HasComponent(triggerEvent.EntityB))
                {
                    var destroyable = m_destroyables[triggerEvent.EntityB];
                    destroyable.m_mustBeDestroyed = true;
                    m_destroyables[triggerEvent.EntityB] = destroyable;
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var destroyables = GetComponentDataFromEntity<DestroyableComponent>();

            var job = new TriggerJob
            {
                m_destroyables = destroyables
            };

            var jobHandle = job.Schedule(m_stepPhysicsWorld.Simulation, ref m_physicsWorld.PhysicsWorld, inputDeps);
            jobHandle.Complete();
            
            return jobHandle;
        }

    }
}