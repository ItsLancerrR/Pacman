using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Checks that the ghost is currently in the scatter state and the node is not null.
        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            // If the random move picked by the program is the opposite direction of the current direction, then we change the direction.
            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;        // Switch direction.

                // Ensures no out of bound exception.
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }

}