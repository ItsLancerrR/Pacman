using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    //create the Ghost references along with movement.
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    //information for ghost initial behavior.
    //There are 2 different initial behaviors.
    public GhostBehavior initialBehavior;

    //Object we want to chase(i.e. Pacman)
    public Transform target;
    public int points = 200;

    //assigns all the references
    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
    }


    private void Start()
    {
        ResetState();
    }

    //resets a ghost, gets called when restarting a round
    public void ResetState()
    {
        //same 2 lines as Pacman Script
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        //Ghosts will only start in scatter mode.
        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        //turn home mode off if not the initial behavior of ghost
        if (this.home != this.initialBehavior)
        {
            this.home.Disable();
        }

        //check if an initial behavior has been assigned
        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    //check what object a Ghost collides with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //did Ghost collide with Pacman?
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            //depending on the state of Ghost, either Pacman is eaten or the Ghost is eaten.
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = this.transform.position.z;
        this.transform.position = position;
    }

}