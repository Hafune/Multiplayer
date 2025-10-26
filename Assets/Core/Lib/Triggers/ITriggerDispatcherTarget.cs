using UnityEngine;

public interface ITriggerDispatcherTarget
{
    public void OnTriggerEnter(Collider col);
    public void OnTriggerExit(Collider col);

    public int GetInstanceID();
}