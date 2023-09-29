using UnityEngine;

//we cannot have a GhostBehavior without a Ghost
[RequireComponent(typeof(Ghost))]

//make class abstract so you can never have GhostBehavior by itself
//always needs to inherit a certain behavior
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;

    //assigns ghost reference
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    //we mark as virtual to allow overriding in the different ghost behavior classes
    //enables (turns on) a ghost behavior for a given duration
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        //make sure behavior resets everytime
        CancelInvoke();
        //set a timer to disable
        Invoke(nameof(Disable), duration);
    }

    //we mark as virtual to allow overriding in the different ghost behavior classes
    //diasbles (turns off) a ghost behavior
    public virtual void Disable()
    {
        this.enabled = false;
        //make sure behavior resets everytime
        CancelInvoke();
    }

}