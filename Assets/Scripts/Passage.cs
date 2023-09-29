using UnityEngine;

//we require a Collider2D Object
[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    //Either left or right connection
    public Transform connection;

    //changes the direction of the object that is colliding with the passages (i.e. Ghosts, Pacman)
    // Teleports character when they enter the tunnel.
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        other.transform.position = position;
    }

}