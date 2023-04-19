using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private const float uiDelay = 0.15f;

    [Header("Selection")]
    [SerializeField]
    private int activeTileSortingOrder;
    [SerializeField]
    private int deactiveTileSortingOrder;

    [Header("Entity Level")]
    [SerializeField]
    private int entityLevel;

    [Header("References")]
    [SerializeField]
    private SpriteRenderer spriteRendererComponent;

    private void Awake()
    {
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Sets the sorting order of the sprite renderer component to the active tile sorting order.
    /// </summary>
    public void Select()
    {
        spriteRendererComponent.sortingOrder = activeTileSortingOrder;
    }

    /// <summary>
    /// Sets the sorting order of the sprite renderer component to the deactive tile sorting order.
    /// </summary>
    public void Deselect()
    {
        spriteRendererComponent.sortingOrder = deactiveTileSortingOrder;
    }

    /// <summary>
    /// Starts the SendRoutine coroutine and sets the sorting order of
    /// the sprite renderer component to the deactive tile sorting order
    /// </summary>
    public void Send()
    {
        StartCoroutine(SendRoutine());
        spriteRendererComponent.sortingOrder = deactiveTileSortingOrder;
    }

    /// <summary>
    /// Moves the game object to a new position, waits for a delay, and then deactivates it
    /// <summary>
    private IEnumerator SendRoutine()
    {
        transform.DOMove(new Vector3(1, 3.5f, 0), uiDelay);
        yield return new WaitForSeconds(uiDelay * 2);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns the level of the entity
    /// </summary>
    /// <returns></returns>
    public int GetEntityLevel()
    {
        return entityLevel;
    }

    /// <summary>
    /// Sets the parent transform of the entity and resets its position
    /// </summary>
    /// <param name="parentTransform"></param>
    public void SetParent(Transform parentTransform)
    {
        transform.parent = parentTransform;
        ResetPosition();
    }

    /// <summary>
    /// Resets the position of the entity to its initial position
    /// </summary>
    public void ResetPosition()
    {
        transform.DOLocalMove(Vector3.zero, uiDelay);
    }

    /// <summary>
    /// Returns the sprite associated with the sprite renderer component.
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite()
    {
        return spriteRendererComponent.sprite;
    }
}
