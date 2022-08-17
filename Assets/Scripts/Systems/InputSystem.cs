using Asteroids.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Systems
{
    public class InputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((ref InputComponent input) =>
            {
                input.m_inputLeft = Input.GetKey(KeyCode.LeftArrow);
                input.m_inputRight = Input.GetKey(KeyCode.RightArrow);
                input.m_inputForward = Input.GetKey(KeyCode.UpArrow);
                input.m_inputShoot = Input.GetKey(KeyCode.Space);
                
            }).Run();
        }
    }

}