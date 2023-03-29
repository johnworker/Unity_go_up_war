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
        // 公開 事件 移動 = 初始值為空的委託 (事件就永遠不會為空的)
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
            // 禁用動作表示
            inputActions.Gameplay.Disable();
        }

        public void OnEnableGameplayInput()
        {
            // 啟用動作表示
            inputActions.Gameplay.Enable();

            // 鼠標可見度是隱藏
            Cursor.visible = false;
            // 鼠標狀態鎖定
            Cursor.lockState = CursorLockMode.Locked;


        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                // 永遠不為空就不需要再做空值檢查
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
