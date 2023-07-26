using DG.Tweening;
using UnityEngine;

public class Entity : MonoBehaviour, ISelectable, IMoveable
{
    [Header("Selection")]
    [SerializeField]
    private int _activeTileSortingOrder;
    [SerializeField]
    private int _deactiveTileSortingOrder;

    [Header("Entity Level")]
    [SerializeField]
    private int _entityLevel;
    public int EntityLevel
    {
        get { return _entityLevel; }
        private set { }
    }

    [Header("References")]
    [SerializeField]
    private SpriteRenderer _spriteRendererComponent;

    [SerializeField]
    private Sprite _requestSprite;
    public Sprite RequestSprite
    {
        get { return _requestSprite; }
        private set { }
    }

    public Sprite Sprite
    {
        get { return _spriteRendererComponent.sprite; }
        private set { }
    }

    private void OnEnable()
    {
        _spriteRendererComponent = GetComponent<SpriteRenderer>();
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
        _spriteRendererComponent.sortingOrder = _activeTileSortingOrder;
    }

    /// <summary>
    /// Sets the sorting order of the sprite renderer component to the deactive tile sorting order.
    /// </summary>
    public void DeSelect()
    {
        _spriteRendererComponent.sortingOrder = _deactiveTileSortingOrder;
    }

    /// <summary>
    /// Starts the SendRoutine coroutine and sets the sorting order of
    /// the sprite renderer component to the deactive tile sorting order
    /// </summary>
    public void Move(Vector2 buttonPosition)
    {
        SetParent(null);
        transform.DOMove(buttonPosition, TimeManager.singleton.GetUIDelay()).OnComplete(() =>
        {
            ParticleManager.singleton.PlayParticleAtPoint(transform.position);
            AudioManager.singleton.PlaySound("Sparkle");
        });
        _spriteRendererComponent.sortingOrder = _deactiveTileSortingOrder;
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
}