using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Herohunk
{
    [CreateAssetMenu(menuName = "Player Input")]
    public class PlayerInput : ScriptableObject,InputActions.IGameplayActions
    {
        // ���} �ƥ� ���� = ��l�Ȭ��Ū��e�U (�ƥ�N�û����|���Ū�)
        public event UnityAction<Vector2> onMove = delegate { };

        public event UnityAction onStopMove = delegate { };

        InputActions inputActions;

        private void OnEnable()
        {
            inputActions = new InputActions();

            inputActions.Gameplay.SetCallbacks(this);
        }

        private void OnDisable()
        {
            DisableAllInputs();
        }

        public void DisableAllInputs()
        {
            // �T�ΰʧ@���
            inputActions.Gameplay.Disable();
        }

        public void OnEnableGameplayInput()
        {
            // �ҥΰʧ@���
            inputActions.Gameplay.Enable();

            // ���Хi���׬O����
            Cursor.visible = false;
            // ���Ъ��A��w
            Cursor.lockState = CursorLockMode.Locked;


        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                // �û������ŴN���ݭn�A���ŭ��ˬd
                // if(OnMove != null)
                onMove.Invoke(context.ReadValue<Vector2>());
            }

            if(context.phase == InputActionPhase.Canceled)
            {
                onStopMove.Invoke();
            }
        }
    }
}
