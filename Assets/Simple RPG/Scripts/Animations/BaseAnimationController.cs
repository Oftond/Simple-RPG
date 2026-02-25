using UnityEngine;

public abstract class BaseAnimationController : MonoBehaviour
{
    protected abstract void GetComponents();
    protected abstract int GetState();
    protected float lockedTill;
    protected int lockState(int s, float t)
    {
        lockedTill = Time.time + t;
        return s;
    }
}