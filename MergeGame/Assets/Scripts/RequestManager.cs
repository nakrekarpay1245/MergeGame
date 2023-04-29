using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RequestManager : MonoSingleton<RequestManager>
{
    [SerializeField]
    private List<RequestButton> requestButtonList;

    public void AddRequestButton(RequestButton requestButton)
    {
        if (!requestButtonList.Contains(requestButton))
        {
            requestButtonList.Add(requestButton);
        }
    }

    /// <summary>
    /// Gets a random request by selecting a random entity from the entity 
    /// list and setting its sprite on the request image component
    /// </summary>
    public Entity GetRequestEntity()
    {
        // Randomly selects an entity from the entity list and sets it as the request entity
        int requestIndex = Random.Range(0, 3);
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
        for (int i = 0; i < TileManager.singleton.GetActiveTileList().Count; i++)
        {
            Tile activeTile = TileManager.singleton.GetActiveTileList()[i];

            Entity activeTileEntity = activeTile.GetIsFull() ? activeTile.GetEntity() : null;

            if (!activeTileEntity)
            {
                continue;
            }
            if (requestEntity.GetEntityLevel() == activeTileEntity.GetEntityLevel())
            {
                activeTileEntity.Send(Camera.main.ScreenToWorldPoint(requestButton.position + Vector3.right * 50));
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
        for (int i = 0; i < TileManager.singleton.GetActiveTileList().Count; i++)
        {
            Tile activeTile = TileManager.singleton.GetActiveTileList()[i];

            Entity activeTileEntity = activeTile.GetIsFull() ? activeTile.GetEntity() : null;

            if (!activeTileEntity)
            {
                continue;
            }
            if (requestEntity.GetEntityLevel() == activeTileEntity.GetEntityLevel())
            {
                return true;
            }
        }
        return false;
    }

    public void ControlRequestButton()
    {
        for (int i = 0; i < requestButtonList.Count; i++)
        {
            requestButtonList[i].ControlRequest();
        }
    }
}
