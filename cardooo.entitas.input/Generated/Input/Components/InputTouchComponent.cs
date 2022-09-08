//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public TouchComponent touch { get { return (TouchComponent)GetComponent(InputComponentsLookup.Touch); } }
    public bool hasTouch { get { return HasComponent(InputComponentsLookup.Touch); } }

    public void AddTouch(int newFingerId, UnityEngine.Vector2 newStartPos, UnityEngine.Vector2 newCurrentPos, UnityEngine.Vector2 newDelta, int newTouchPhase) {
        var index = InputComponentsLookup.Touch;
        var component = (TouchComponent)CreateComponent(index, typeof(TouchComponent));
        component.fingerId = newFingerId;
        component.startPos = newStartPos;
        component.currentPos = newCurrentPos;
        component.delta = newDelta;
        component.touchPhase = newTouchPhase;
        AddComponent(index, component);
    }

    public void ReplaceTouch(int newFingerId, UnityEngine.Vector2 newStartPos, UnityEngine.Vector2 newCurrentPos, UnityEngine.Vector2 newDelta, int newTouchPhase) {
        var index = InputComponentsLookup.Touch;
        var component = (TouchComponent)CreateComponent(index, typeof(TouchComponent));
        component.fingerId = newFingerId;
        component.startPos = newStartPos;
        component.currentPos = newCurrentPos;
        component.delta = newDelta;
        component.touchPhase = newTouchPhase;
        ReplaceComponent(index, component);
    }

    public void RemoveTouch() {
        RemoveComponent(InputComponentsLookup.Touch);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherTouch;

    public static Entitas.IMatcher<InputEntity> Touch {
        get {
            if (_matcherTouch == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Touch);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherTouch = matcher;
            }

            return _matcherTouch;
        }
    }
}
