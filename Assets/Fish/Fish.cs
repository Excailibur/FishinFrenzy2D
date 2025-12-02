using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Fish : MonoBehaviour
{
    [SerializeField] protected string fishName;
    [SerializeField] protected float speed;
    [SerializeField] protected int score;
    [SerializeField] protected EnumRarity rarity = EnumRarity.COMMON;
    [SerializeField] protected float edgePadding = 0.5f;
    [SerializeField] protected float swimDelay = 1f;
    [SerializeField] protected LayerMask obstacleLayer;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 destinationPosition;
    protected bool destinationReached => Vector3.Distance(transform.position, destinationPosition) < edgePadding;
    protected bool isFlipped => spriteRenderer.flipX;
    protected Coroutine swimCoroutine;

    //protected bool destinationReached 
    //{
    //      return Vector3.Distance(transform.position, destinationPosition) < 0.1f}
    //}

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        swimCoroutine = StartCoroutine(SwimPattern());
    }
    protected virtual IEnumerator SwimPattern()
    {
        Debug.Log($"{fishName},{rarity} started swimming."); 
    
        destinationPosition = PickNewDestination();  // initial target

        while (true)
        {
            yield return MoveTo(destinationPosition);
        }
    }
    protected abstract IEnumerator MoveTo(Vector3 position);
    
    protected abstract Vector3 PickNewDestination();
}
