using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class AddSystemFeature : Feature
{
    // "AddSystemFeature"名稱自取
    public AddSystemFeature(Contexts contexts) : base("AddSystemFeature")
    {
        // input
        Add(new InputSystem(contexts));
        Add(new JoystickSystem(contexts));
    }
}
