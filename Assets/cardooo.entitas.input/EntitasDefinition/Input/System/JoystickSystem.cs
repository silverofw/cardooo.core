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
            validArea = new Rect(0f,0f,Screen.width*0.5f,Screen.height*0.5f),
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
            tag = 0,
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
                leftJoystick = rightJoystickEntity.moveJoystick;
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
                        leftJoystick.currentPos = e.touch.startPos;
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

        if (isRightMove && _lastRightMove)
        {
            SetHostSkillInput(Vector2.zero, true);
        }

        _lastLeftMove = isLeftMove;
        _lastRightMove = isRightMove;
    }

    void SetHostMoveInput(Vector2 left)
    { 
        
    }

    void SetHostSkillInput(Vector2 right, bool useSkill = false)
    { 
        
    }
}
