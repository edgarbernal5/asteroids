using Asteroids.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Asteroids.Scripts.Systems
{
    public class InputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref InputComponent _input) =>
            {
                _input.m_inputLeft = Input.GetKey(KeyCode.LeftArrow);
                _input.m_inputRight = Input.GetKey(KeyCode.RightArrow);
                _input.m_inputForward = Input.GetKey(KeyCode.UpArrow);
                _input.m_inputShoot = Input.GetKey(KeyCode.Space);
            }).Run();
        }
    }

}