using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input]
public class MoveJoystickComponent : IComponent
{
    /// <summary>
    /// 0 : left
    /// 1 : right
    /// </summary>
    [PrimaryEntityIndex]
    public int tag;

    public Rect validArea;
    public float radius;
    public int fingerId;
    public bool active;
    public Vector2 centerPos;
    public Vector2 currentPos;
    public Vector2 direction;
}
