using Assets.Scripts.Components;
using Asteroids.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts
{
    public class SceneInitialization : MonoBehaviour
    {
        private EntityManager entityManager;
        public Entity m_asteroidLibrary;

        public Entity m_playerEntity;

        [SerializeField] private Transform[] m_asteroidsSpawnPositions;

        private Vector3[] m_spawnPositionsVectors;

        private float m_currentTimer;
        [SerializeField] private float m_asteroidSpawnFrequency = 2.0f;

        public static SceneInitialization m_instance;
        
        private void Awake()
        {
            m_instance = this;
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            m_spawnPositionsVectors = new Vector3[m_asteroidsSpawnPositions.Length];
            for (int i = 0; i < m_spawnPositionsVectors.Length; i++)
            {
                m_spawnPositionsVectors[i] = m_asteroidsSpawnPositions[i].position;
            }
        }

        private void Start()
        {
            entityManager.CreateEntity(typeof(InputComponent));
            SpawnPlayerAtPosition(Vector3.zero);
        }

        private void SpawnAsteroid()
        {
            var buffer = entityManager.GetBuffer<EntityBufferElement>(m_asteroidLibrary);
            var lengthOfBuffer = buffer.Length;
            var randomAsteroidIndex = Random.Range(0, lengthOfBuffer);
            var newAsteroid = entityManager.Instantiate(buffer[randomAsteroidIndex].m_entity);

            var randomSpawnPositionIndex = Random.Range(0, m_spawnPositionsVectors.Length);
            var spawnPosition = m_spawnPositionsVectors[randomSpawnPositionIndex];

            entityManager.SetComponentData(newAsteroid, new Translation()
            {
                Value = spawnPosition
            });

            var randomMoveDirection = math.normalize(new float3(Random.Range(-.8f, .8f), Random.Range(-.8f, .8f), 0));
            var randomRotation = math.normalize(new float3(Random.value, Random.value, 0));

            entityManager.SetComponentData(newAsteroid, new MovementCommandsComponent()
            {
                m_currentMoveDirection = randomMoveDirection,
                m_currentLinearCommand = 1,
                m_currentAngularCommand = randomRotation
            });
        }

        private void Update()
        {
            m_currentTimer += Time.deltaTime;

            if (m_currentTimer > m_asteroidSpawnFrequency)
            {
                m_currentTimer = 0;
                SpawnAsteroid();
            }
        }

        public void CheckAndSpawnPlayer()
        {
            SpawnPlayerAtPosition(Vector3.zero);
        }

        private void SpawnPlayerAtPosition(Vector3 position)
        {
            var playerFactory = entityManager.GetComponentData<PlayerFactoryElementComponent>(m_playerEntity);

            var playerShip = entityManager.Instantiate(playerFactory.m_player);
            entityManager.SetComponentData(m_playerEntity, new Translation()
            {
                Value = position
            });
        }
    }
}