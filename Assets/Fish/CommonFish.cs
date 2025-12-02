using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFish : Fish
{
    protected override Vector3 PickNewDestination()
    {
        Debug.Log("Picking new destination for CommonFish.");

        RaycastHit2D hit;
        if(isFlipped)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, obstacleLayer);

        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, obstacleLayer);
        }

        Vector2 debugDirection = (Vector3) hit.point - transform.position;
        Debug.DrawRay(transform.position, debugDirection, Color.red, 1f);
        return hit.point;
    }

    protected override IEnumerator MoveTo(Vector3 position)
    {
        while (!destinationReached)
        {
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Reached destination: " + position);
        yield return new WaitForSeconds(swimDelay);
        spriteRenderer.flipX = !spriteRenderer.flipX;
        destinationPosition = PickNewDestination();
    }
}
