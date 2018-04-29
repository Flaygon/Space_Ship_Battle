using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Player player;
    public Ship playerShipAsset;

    public Enemy enemyAsset;
    private List<Enemy> enemies = new List<Enemy>();

    public List<Ship> shipAssets;

    public List<GameObject> obstacleAssets;
    private List<GameObject> obstacles = new List<GameObject>();

    public float worldTileSize = 500;
    public float worldTileHalfSize = 250;
    public int worldTilesX = 10;
    public int worldTilesY = 10;

    void Start ()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GenerateLevel();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateLevel();
    }

    private void GenerateLevel()
    {
        ResetLevel();

        for(int iTileX = -worldTilesX / 2; iTileX <= worldTilesX / 2; ++iTileX)
        {
            for (int iTileY = -worldTilesY / 2; iTileY <= worldTilesY / 2; ++iTileY)
            {
                float middleX = iTileX * worldTileSize + worldTileHalfSize;
                float middleZ = iTileY * worldTileSize + worldTileHalfSize;

                int check = Random.Range(0, 10);
                if (check < 4) // obstacle check
                {
                    GameObject newObstacle = Instantiate(obstacleAssets[Random.Range(0, obstacleAssets.Count)]);
                    newObstacle.transform.position = new Vector3(middleX, 0, middleZ);
                    newObstacle.transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
                    obstacles.Add(newObstacle);
                }
                else if(check < 6)
                {
                    Enemy newEnemy = Instantiate(enemyAsset);

                    Ship newShip = Instantiate(shipAssets[Random.Range(0, shipAssets.Count)]);
                    newShip.transform.position = new Vector3(middleX, 0.0f, middleZ);
                    newShip.transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);

                    newEnemy.ship = newShip;
                    newEnemy.manager = this;

                    enemies.Add(newEnemy);
                }

                // otherwise empty space
            }
        }
    }

    private void ResetLevel()
    {
        if(player.ship == null)
        {
            player.ship = Instantiate(playerShipAsset, Vector3.zero, Quaternion.identity);
            player.ship.invincible = true;
        }
        player.ship.transform.position = Vector3.zero;

        foreach (GameObject iObstacle in obstacles)
        {
            Destroy(iObstacle);
        }
        obstacles.Clear();

        foreach (Enemy iEnemy in enemies)
        {
            if (iEnemy == null)
                continue;

            Destroy(iEnemy.ship.gameObject);
            Destroy(iEnemy.gameObject);
        }
        enemies.Clear();
    }
}