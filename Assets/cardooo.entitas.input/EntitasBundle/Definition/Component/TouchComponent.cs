using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input]
public class TouchComponent : IComponent
{
    [PrimaryEntityIndex]
    public int fingerId;

    public Vector2 startPos;
    public Vector2 currentPos;
    public Vector2 delta;
    /// <summary>
    /// 0 : begin
    /// 1 : moved
    /// 2 : ended
    /// </summary>
    public int touchPhase;
}
