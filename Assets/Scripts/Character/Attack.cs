using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected virtual void Awake() { }
    protected virtual void OnEnable() { }
    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void DealDamage(Transform obj, uint damage) { }
    protected virtual void OnDestroy() { }
    protected virtual void OnDisable() { }
}
