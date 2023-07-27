using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RequestManager : MonoSingleton<RequestManager>
{
    [SerializeField]
    private List<RequestButton> _requestButtonList;

    [SerializeField]
    private Transform _requestDisplayer;
    [SerializeField]
    private Image _requestDisplayerIcon;
    [SerializeField]
    private Transform _requestBrightness;

    public void AddRequestButton(RequestButton requestButton)
    {
        if (!_requestButtonList.Contains(requestButton))
        {
            _requestButtonList.Add(requestButton);
        }
    }

    /// <summary>
    /// Gets a random request by selecting a random entity from the entity 
    /// list and setting its sprite on the request image component
    /// </summary>
    public Entity GetRequestEntity()
    {
        // Randomly selects an entity from the entity list and sets it as the request entity
        //int requestIndex = Random.Range(0, EntityManager.singleton.GetEntityListCount());
        int requestIndex = Random.Range(0, 4);
        return EntityManager.singleton.GetEntityWithIndex(requestIndex);
    }

    /// <summary>
    /// This is a method named SetRequest() that is used to set a request for a specific
    /// entity. It first retrieves the current request entity from the RequestManager. 
    /// Then, it iterates through a list of activeTileList, which represents a list of 
    /// tiles that are currently in use in the game. For each active tile, it checks if
    /// the tile is occupied by an entity. If it is not, the iteration continues to the 
    /// next tile. Otherwise, it compares the entity level of the current request entity 
    /// with the entity level of the entity occupying the tile. If the levels match, it 
    /// starts a coroutine named SetRequestRoutine with the active tile entity and the 
    /// active tile as parameters. It also sets the deliver button visibility to false
    /// </summary>
    public bool SetRequest(Entity requestEntity, Transform requestButton)
    {
        for (int i = 0; i < TileManager.singleton.ActiveTileList.Count; i++)
        {
            Tile activeTile = TileManager.singleton.ActiveTileList[i];

            Entity activeTileEntity = activeTile.IsFull ? activeTile.Entity : null;

            if (!activeTileEntity)
            {
                continue;
            }
            if (requestEntity.EntityLevel == activeTileEntity.EntityLevel)
            {
                activeTileEntity.Move(Camera.main.ScreenToWorldPoint(requestButton.position + Vector3.right * 50));
                activeTile.Clear();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// This is a method named SetRequest() that is used to set a request for a specific
    /// entity. It first retrieves the current request entity from the RequestManager. 
    /// Then, it iterates through a list of activeTileList, which represents a list of 
    /// tiles that are currently in use in the game. For each active tile, it checks if
    /// the tile is occupied by an entity. If it is not, the iteration continues to the 
    /// next tile. Otherwise, it compares the entity level of the current request entity 
    /// with the entity level of the entity occupying the tile. If the levels match, it 
    /// starts a coroutine named SetRequestRoutine with the active tile entity and the 
    /// active tile as parameters. It also sets the deliver button visibility to false
    /// </summary>
    public bool ContolRequest(Entity requestEntity)
    {
        for (int i = 0; i < TileManager.singleton.ActiveTileList.Count; i++)
        {
            Tile activeTile = TileManager.singleton.ActiveTileList[i];

            Entity activeTileEntity = activeTile.IsFull ? activeTile.Entity : null;

            if (!activeTileEntity)
            {
                continue;
            }
            if (requestEntity.EntityLevel == activeTileEntity.EntityLevel)
            {
                return true;
            }
        }
        return false;
    }

    public void ControlRequestButton()
    {
        for (int i = 0; i < _requestButtonList.Count; i++)
        {
            _requestButtonList[i].ControlRequest();
        }
    }

    public void DisplayRequest(Sprite requestEntitySprite)
    {
        _requestDisplayerIcon.sprite = requestEntitySprite;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_requestDisplayer.DOScale(0, 0));

        _requestBrightness.DORotate(180 * Vector3.forward, TimeManager.singleton.GetUIDelay4()).OnComplete(() =>
            _requestBrightness.DORotate(360 * Vector3.forward, TimeManager.singleton.GetUIDelay4()));

        sequence.Append(_requestDisplayer.DOScale(1, TimeManager.singleton.GetUIDelay()));

        sequence.Append(_requestDisplayer.DOScale(1.1f, TimeManager.singleton.GetUIDelay2()));

        sequence.Append(_requestDisplayer.DOScale(0, TimeManager.singleton.GetUIDelay()));
    }
}