using System;
using UnityEngine;

public class FunctionTimer
{
    private Action action;
    private float timer;
    private GameObject gameObject;
    private bool isDestroyerd;
    private FunctionTimer(Action action, float timer, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.gameObject = gameObject;
        isDestroyerd = false;
    }

    // create timer
    public static FunctionTimer Create(Action action, float timer)
    {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer functiontimer = new FunctionTimer(action, timer, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functiontimer.Update;

        return functiontimer;
    }

    private class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;

        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }
    } 

    // update timer time
    public void Update()
    {
        if (!isDestroyerd)
        {           
           
            if (GameObject.FindObjectOfType<LayerManag>().layers[0].activeSelf == false)
            {
                timer -= Time.deltaTime;
                GameObject.FindObjectOfType<LevelManager>().ShowTime(timer);
            }
         
            if (timer < 0)
            {
                action();
                timer = 0;
                DestroySelf();
            }
        }
    }

    // destroy timer
    public void DestroySelf()
    {
        isDestroyerd = true;
        UnityEngine.Object.Destroy(gameObject);
    }
}