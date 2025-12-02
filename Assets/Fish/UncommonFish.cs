using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncommonFish : Fish
{
    [SerializeField] private float minRange = 3f;
    [SerializeField] private float maxRange = 5f;
    protected override Vector3 PickNewDestination()
    {
        Debug.Log("Picking new destination for UncommonFish.");

        RaycastHit2D hit;
        float range = Random.Range(minRange, maxRange);
        Vector2 targetPoint;
        Vector2 debugDirection;

        //Check for obstacles within range and if not in range set target point to max range
        if(isFlipped)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left, range, obstacleLayer);
            targetPoint = (Vector2)transform.position + Vector2.left * range;
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, range, obstacleLayer);
            targetPoint = (Vector2)transform.position + Vector2.right * range;
        }

        //Prevent wall clipping
        if (hit.collider != null)
        {
            Debug.Log($"Obstacle detected, adjusting target point {hit.collider.name}");
            debugDirection = (Vector3) hit.point - transform.position;
            Debug.DrawRay(transform.position, debugDirection, Color.red, 1f);
            targetPoint = hit.point;
        }
        else
        {
            debugDirection = targetPoint - (Vector2)transform.position;
            Debug.DrawRay(transform.position, debugDirection, Color.green, 1f);
            Debug.Log("No obstacle detected, moving to target point.");
        }

        return targetPoint;
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
