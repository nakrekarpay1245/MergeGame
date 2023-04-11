using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T singleton;

    protected virtual void OnEnable()
    {
        //Debug.Log("Base awake");

        if (!singleton)
        {
            singleton = (T)this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }
}
