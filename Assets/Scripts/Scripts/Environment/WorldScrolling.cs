using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0,0);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    [SerializeField] float tileSize;
    GameObject[,] terrainTiles;
    CharacterStats character;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;
    [SerializeField] int fieldOfVisionHeight = 10;
    [SerializeField] int fieldOfVisionWidth = 20;


    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    private void Start()
    {
        playerTransform = GameManager.instance.playerTransform;
    }

    private void Update()
    {
        if(character.isAlive)
        {
            playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
            playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

            playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
            playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;


            if (currentTilePosition != playerTilePosition)
            {
                currentTilePosition = playerTilePosition;

                onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
                onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
                UpdateTilesOnScreen();
            }

            if (playerTransform.position.x < 0) playerTilePosition.x = (int)((playerTransform.position.x / tileSize) - 0.5);
            else playerTilePosition.x = (int)((playerTransform.position.x / tileSize) + 0.5f);

            if (playerTransform.position.y < 0) playerTilePosition.y = (int)((playerTransform.position.y / tileSize) - 0.5);
            else playerTilePosition.y = (int)((playerTransform.position.y / tileSize) + 0.5f);
        }
    }

    private void UpdateTilesOnScreen()
    {
        for(int x = -(fieldOfVisionWidth / 2); x <= fieldOfVisionWidth/2; x++)
        {
            for(int y = -(fieldOfVisionHeight / 2); y <= fieldOfVisionHeight/2; y++)
            {
                int tileToUpdateX = CalculatePositionOnAxis(playerTilePosition.x + x, true);
                int tileToUpdateY = CalculatePositionOnAxis(playerTilePosition.y + y, false);

                GameObject tile = terrainTiles[tileToUpdateX, tileToUpdateY];
                Vector3 newPosition = CalculateTilePosition(playerTilePosition.x + x, playerTilePosition.y + y);

                if(newPosition != tile.transform.position)
                {
                    tile.transform.position = newPosition;
                    terrainTiles[tileToUpdateX, tileToUpdateY].GetComponent<TerrainTile>().Spawn();
                }

            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if(horizontal)
        {
            if(currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount -1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount -1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)currentValue;
    }

    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }

}
