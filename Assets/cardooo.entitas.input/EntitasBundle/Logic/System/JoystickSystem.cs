using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class JoystickSystem : IExecuteSystem, IInitializeSystem
{
    readonly IGroup<InputEntity> _touches;
    readonly List<InputEntity> _entities = new List<InputEntity>();

    bool _lastLeftMove = false;
    bool _lastRightMove = false;

    public JoystickSystem(Contexts contexts)
    {
        _touches = contexts.input.GetGroup(InputMatcher.Touch);
    }

    public void Initialize()
    {
        // left control move
        var leftJoystick = Contexts.sharedInstance.input.CreateEntity();
        leftJoystick.AddComponent(InputComponentsLookup.MoveJoystick, new MoveJoystickComponent()
        {
            tag = 0,
            validArea = new Rect(0f, 0f, Screen.width * 0.5f, Screen.height * 0.5f),
            radius = 51,
            active = false,
            fingerId = -1,
            centerPos = Vector2.zero,
            direction = Vector2.zero
        });

        //right control skill direction
        var rightJoystick = Contexts.sharedInstance.input.CreateEntity();
        rightJoystick.AddComponent(InputComponentsLookup.MoveJoystick, new MoveJoystickComponent()
        {
            tag = 1,
            validArea = new Rect(Screen.width * 0.5f, 0f, Screen.width * 0.35f, Screen.height * 0.5f),
            radius = 51,
            active = false,
            fingerId = -1,
            centerPos = Vector2.zero,
            direction = Vector2.zero
        });

    }

    public void Execute()
    {
        _touches.GetEntities(_entities);
        bool isLeftMove = false;
        bool isRightMove = false;
        if (_entities.Count > 0)
        {
            InputEntity leftJoystickEntity = Contexts.sharedInstance.input.GetEntityWithMoveJoystick(0);

            MoveJoystickComponent leftJoystick = null;
            if (leftJoystickEntity != null)
            {
                leftJoystick = leftJoystickEntity.moveJoystick;
            }

            InputEntity rightJoystickEntity = Contexts.sharedInstance.input.GetEntityWithMoveJoystick(1);

            MoveJoystickComponent rightJoystick = null;
            if (rightJoystickEntity != null)
            {
                rightJoystick = rightJoystickEntity.moveJoystick;
            }

            foreach (var e in _entities)
            {
                if (!e.hasTouch)
                    continue;

                if (e.touch.touchPhase == 0)
                {

                    if (leftJoystick != null && leftJoystick.validArea.Contains(e.touch.startPos))
                    {
                        // Begin >> left
                        leftJoystick.fingerId = e.touch.fingerId;
                        leftJoystick.centerPos = e.touch.startPos;
                        leftJoystick.direction = (e.touch.currentPos - e.touch.startPos).normalized;
                        leftJoystick.currentPos = e.touch.currentPos;
                        SetHostMoveInput(leftJoystick.direction);
                        isLeftMove = true;
                    }
                    else if (rightJoystick != null && rightJoystick.validArea.Contains(e.touch.startPos))
                    {
                        // Begin >> right
                        rightJoystick.fingerId = e.touch.fingerId;
                        rightJoystick.centerPos = e.touch.startPos;
                        rightJoystick.direction = (e.touch.currentPos - e.touch.startPos).normalized;
                        rightJoystick.currentPos = e.touch.currentPos;
                        SetHostSkillInput(rightJoystick.direction, false);
                        isRightMove = true;
                    }
                }
                else if (e.touch.touchPhase == 1)
                {
                    if (leftJoystick != null
                        && leftJoystick.validArea.Contains(e.touch.startPos)
                        && leftJoystick.fingerId == e.touch.fingerId)
                    {
                        // move >> left
                        leftJoystick.direction = (e.touch.currentPos - e.touch.startPos).normalized;
                        leftJoystick.currentPos = e.touch.currentPos;
                        SetHostMoveInput(leftJoystick.direction);
                        isLeftMove = true;
                    }
                    else if (rightJoystick != null
                        && rightJoystick.validArea.Contains(e.touch.startPos)
                        && rightJoystick.fingerId == e.touch.fingerId)
                    {
                        // move >> right
                        rightJoystick.direction = (e.touch.currentPos - e.touch.startPos).normalized;
                        rightJoystick.currentPos = e.touch.startPos;
                        SetHostSkillInput(rightJoystick.direction, false);
                        isRightMove = true;
                    }
                }
            }
        }

        if (!isLeftMove && _lastLeftMove)
        {
            // end left
            SetHostMoveInput(Vector2.zero);
        }

        if (!isRightMove && _lastRightMove)
        {
            // end right
            SetHostSkillInput(Vector2.zero, true);
        }

        _lastLeftMove = isLeftMove;
        _lastRightMove = isRightMove;
    }

    void SetHostMoveInput(Vector2 left)
    {
        /*
        var host = Contexts.sharedInstance.game.GetEntityWithHost(1);
        if (host != null && host.hasMoveDirection)
        {
            Vector2 leftMove = TransformMoveDir(left);
            leftMove.Normalize();
            if (leftMove.magnitude <= 0.1f)
            {
                leftMove = Vector2.zero;
            }
            host.moveDirection.value = (leftMove.x * Vector3.right + leftMove.y * Vector3.forward);
        }
        */
    }

    void SetHostSkillInput(Vector2 right, bool useSkill = false)
    {
        if (useSkill)
        {
            Debug.Log("[useSkill]");
        }
    }

    bool GetCameraMoveDirection(ref Vector3 moveForward, ref Vector3 moveRight)
    {
        var mainCamera = Camera.main;
        if (mainCamera == null)
            return false;

        var euler = mainCamera.transform.eulerAngles;
        var cameraMoveSpace = Quaternion.Euler(0, euler.y, 0);
        moveForward = cameraMoveSpace * Vector3.forward;
        moveRight = cameraMoveSpace * Vector3.right;
        return true;
    }

    Vector2 TransformMoveDir(Vector2 origin)
    {
        Vector3 moveForward = Vector3.zero, moveRight = Vector3.zero;
        if (GetCameraMoveDirection(ref moveForward, ref moveRight))
        {
            var resultV3 = moveForward * origin.y + moveRight * origin.x;
            return new Vector2(resultV3.x, resultV3.z);
        }
        return origin;
    }
}
