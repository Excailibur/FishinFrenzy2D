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

        float range = Random.Range(minRange, maxRange);
        Vector2 destination;

        if(isFlipped)
        {
            destination = transform.position + Vector3.left * range;

        }
        else
        {
            destination = transform.position + Vector3.right * range;
        }

        Vector2 debugDirection = destination - (Vector2)transform.position;
        Debug.DrawRay(transform.position, debugDirection, Color.red, 1f);
        return destination;
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
