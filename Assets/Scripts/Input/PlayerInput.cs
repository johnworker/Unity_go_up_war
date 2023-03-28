using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Herohunk
{
    [CreateAssetMenu(menuName = "Player Input")]
    public class PlayerInput : ScriptableObject,InputActions.IGameplayActions
    {

        InputActions inputActions;

        private void OnEnable()
        {
            inputActions = new InputActions();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
