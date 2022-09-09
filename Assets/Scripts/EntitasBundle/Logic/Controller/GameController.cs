using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Entitas;
using cardooo.core;

public class GameController : MonoBehaviour
{
    private Systems systems;
    // Start is called before the first frame update
    void Start()
    {
        var context = Contexts.sharedInstance;
        systems = new Feature("Systems").Add(new AddSystemFeature(context));
        systems.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (systems == null)
            return;
        systems.Execute();
        systems.Cleanup();
    }

    public void OnDestroy()
    {
        systems.ActivateReactiveSystems();
        systems.Execute();
        systems.Cleanup();

        systems = null;

        Contexts.sharedInstance.game.DestroyAllEntities();
        Contexts.sharedInstance.input.DestroyAllEntities();
        Contexts.sharedInstance.Reset();   
    }
}
