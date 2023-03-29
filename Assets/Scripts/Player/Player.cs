using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        PlayerInput input;

        private void OnEnable()
        {
            input.onMove += Move;
            input.onStopMove += StopMove;
        }

        private void OnDisable()
        {
            input.onMove -= Move;
            input.onStopMove -= StopMove;
        }

        void Start()
        {

        }

        private void Move(Vector2 moveInput)
        {
            
        }

        private void StopMove()
        {
            
        }
    }
}
