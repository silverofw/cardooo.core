using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : IExecuteSystem
{
    readonly IGroup<InputEntity> _touches;
    readonly List<InputEntity> _entities = new List<InputEntity>();

    public InputSystem(Contexts contexts)
    {
        _touches = contexts.input.GetGroup(InputMatcher.Touch);
    }

    public void Execute()
    {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        TouchInputUpdate();
#else
        MouseInputUpdate();
#endif
    }

    void MouseInputUpdate()
    {
        _touches.GetEntities(_entities);

        if (Input.GetMouseButtonDown(0))
        {
            InputEntity touchEntity = Contexts.sharedInstance.input.GetEntityWithTouch(0);
            if (touchEntity == null)
            {
                var touchC = new TouchComponent
                {
                    fingerId = 0,
                    startPos = Input.mousePosition,
                    currentPos = Input.mousePosition,
                    delta = Vector2.zero,
                    touchPhase = 0
                };
                Contexts.sharedInstance.input.CreateEntity().AddComponent(InputComponentsLookup.Touch, touchC);
            }
            else
            {
                var touchC = touchEntity.GetComponent(InputComponentsLookup.Touch) as TouchComponent;
                touchC.fingerId = 0;
                touchC.startPos = Input.mousePosition;
                touchC.currentPos = Input.mousePosition;
                touchC.delta = Vector2.zero;
                touchC.touchPhase = 0;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            InputEntity touchEntity = Contexts.sharedInstance.input.GetEntityWithTouch(0);
            if (touchEntity != null)
            {
                var touchC = touchEntity.GetComponent(InputComponentsLookup.Touch) as TouchComponent;
                Vector2 pos = Input.mousePosition;
                touchC.delta = pos - touchC.currentPos;
                touchC.currentPos = pos;
                touchC.touchPhase = 1;
            }
        }
        else
        {
            InputEntity touchEntity = Contexts.sharedInstance.input.GetEntityWithTouch(0);
            if (touchEntity != null)
            {
                touchEntity.Destroy();
            }
        }
    }
    void TouchInputUpdate()
    {
        for (int i = 0; i < Input.touches.Length; ++i)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                InputEntity touchEntity = Contexts.sharedInstance.input.GetEntityWithTouch(0);
                if (touchEntity == null)
                {
                    var touchC = new TouchComponent
                    {
                        fingerId = touch.fingerId,
                        startPos = touch.position,
                        currentPos = touch.position,
                        delta = Vector2.zero,
                        touchPhase = 0
                    };
                    Contexts.sharedInstance.input.CreateEntity().AddComponent(InputComponentsLookup.Touch, touchC);
                }
                else
                {
                    var touchC = touchEntity.GetComponent(InputComponentsLookup.Touch) as TouchComponent;
                    touchC.fingerId = touch.fingerId;
                    touchC.startPos = touch.position;
                    touchC.currentPos = touch.position;
                    touchC.delta = Vector2.zero;
                    touchC.touchPhase = 0;
                }
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                InputEntity touchEntity = Contexts.sharedInstance.input.GetEntityWithTouch(touch.fingerId);
                if (touchEntity != null)
                {
                    var touchC = touchEntity.GetComponent(InputComponentsLookup.Touch) as TouchComponent;
                    Vector2 pos = Input.mousePosition;
                    touchC.delta = pos - touchC.currentPos;
                    touchC.currentPos = pos;
                    touchC.touchPhase = 1;
                }
            }
            else if (touch.phase > TouchPhase.Stationary)
            {
                InputEntity touchEntity = Contexts.sharedInstance.input.GetEntityWithTouch(touch.fingerId);
                if (touchEntity != null)
                {
                    touchEntity.Destroy();
                }
            }
        }
    }
}
