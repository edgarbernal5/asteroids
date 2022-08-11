using Asteroids.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts
{
    public class SceneInitialization : MonoBehaviour
    {
        private EntityManager entityManager;

        private void Awake()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        private void Start()
        {
            entityManager.CreateEntity(typeof(InputComponent));
        }
    }
}