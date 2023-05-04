using DG.Tweening;
using UnityEngine;

public class Entity : MonoBehaviour
{
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

    [SerializeField]
    private Sprite requestSprite;

    private void Awake()
    {
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.DOScale(0, 0);
        transform.DOScale(1, TimeManager.singleton.GetUIDelay()).SetEase(Ease.Flash);
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
    public void Send(Vector3 buttonPosition)
    {
        SetParent(null);
        transform.DOMove(buttonPosition, TimeManager.singleton.GetUIDelay()).OnComplete(() =>
        {
            ParticleManager.singleton.PlayParticleAtPoint(transform.position);
            AudioManager.singleton.PlaySound("SparkleSFX");
        });
        spriteRendererComponent.sortingOrder = deactiveTileSortingOrder;
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
        transform.DOLocalMove(Vector3.zero, TimeManager.singleton.GetUIDelay());
    }

    /// <summary>
    /// Returns the sprite associated with the sprite renderer component.
    /// </summary>
    /// <returns></returns>
    public Sprite GetSprite()
    {
        return spriteRendererComponent.sprite;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Sprite GetRequestSprite()
    {
        return requestSprite;
    }
}
