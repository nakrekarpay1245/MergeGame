using DG.Tweening;
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Tile : MonoBehaviour
{
    private Entity _currentEntity;
    public Entity Entity
    {
        get { return _currentEntity; }
        set
        {
            if (value)
            {
                if (_currentEntity)
                {
                    _currentEntity.gameObject.SetActive(false);
                    value.gameObject.SetActive(false);

                    Clear();

                    Entity newEntity = Instantiate(EntityManager.singleton.
                        GetEntityWithIndex(_entityLevel + 1), transform.position, Quaternion.identity);

                    _currentEntity = newEntity;
                    _currentEntity.SetParent(transform);
                    _entityLevel = _currentEntity.EntityLevel;
                }
                else
                {
                    _currentEntity = value;
                    _currentEntity.SetParent(transform);
                    _entityLevel = _currentEntity.EntityLevel;
                }

                _spriteRenderer.sprite = _fullTileSprite;
                PopScale();
            }
        }
    }

    public bool IsFull
    {
        get { return _currentEntity; }
        private set { }
    }

    [Header("Tile Visualize")]
    [SerializeField]
    private Sprite _emptyTileSprite;
    [SerializeField]
    private Sprite _fullTileSprite;

    private SpriteRenderer _spriteRenderer;

    private int _entityLevel;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Entity = GetComponentInChildren<Entity>();
    }

    private void Start()
    {
        transform.DOScale(0, 0);
        transform.DOScale(1, TimeManager.singleton.GetUIDelay()).SetEase(Ease.Flash);
    }

    /// <summary>
    /// This function pops the scale of the current transform by a small amount using DOTween's PunchScale method, 
    /// then resets the scale to 1 using DOTween's DOScale method.
    /// </summary>
    private void PopScale()
    {
        transform.DOPunchScale(Vector3.one * 0.1f, TimeManager.singleton.GetUIDelay2()).OnComplete(() =>
        {
            transform.DOScale(1, 0);
        });
    }

    /// <summary>
    /// This function clears the current entity on the tile, making it empty, and sets the tile
    /// sprite to the emptyTileSprite
    /// </summary>
    public void Clear()
    {
        _currentEntity = null;
        _spriteRenderer.sprite = _emptyTileSprite;
    }
}