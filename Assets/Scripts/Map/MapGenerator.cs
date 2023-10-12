using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static int mapSize = 500;
    public static float mapSizeScale = 1f;

    public int regionAmount = 15;
    public int nbPrefabsPerSquare = 5;
    public int nbSquare = 10;
    public float houseProbability = 0.03f;
    public float obeliskProbability = 0.02f;
    public GameObject[] treePrefabs;
    public GameObject[] rockPrefabs;
    public GameObject[] herbsPrefabs;
    public GameObject[] obelisks;
    public GameObject[] houses;

    private GameObject[][] biomesPrefabs;
    private float randomSeed;
    private VoronoiDiagram voronoi;
    private int[] voronoiDiagram;

    private List<GameObject> generatedMapPrefabs = new List<GameObject>();

    void Start()
    {
        voronoi = new VoronoiDiagram();
        biomesPrefabs = new GameObject[3][];
        biomesPrefabs[0] = treePrefabs;
        biomesPrefabs[1] = rockPrefabs;
        biomesPrefabs[2] = herbsPrefabs;
        voronoiDiagram = voronoi.GetDiagram(regionAmount, 3);
        Generate();
        RemoveNeighbors();
    }

    void RemoveNeighbors()
    {
        RemoveNeighbors rmn;

        for (int i = 0; i < generatedMapPrefabs.Count; i++)
        {
            rmn = generatedMapPrefabs[i].GetComponentInChildren<RemoveNeighbors>();
            if (rmn)
                rmn.Remove();
        }
    }

    void Generate()
    {
        GameObject prefab;
        GameObject toInstantiate;
        int squareSize = mapSize / nbSquare;

        for (int startx = 0; startx < mapSize; startx += squareSize)
        {
            for (int starty = 0; starty < mapSize; starty += squareSize)
            {
                for (int i = 0; i < nbPrefabsPerSquare; i++)
                {
                    Vector2 randomPosInSquare = new Vector2(Random.Range(startx, startx + squareSize), Random.Range(starty, starty + squareSize));
                    int biome = voronoiDiagram[(int)randomPosInSquare.x * MapGenerator.mapSize + (int)randomPosInSquare.y];
                    float random = Random.Range(0f, 1f);
                    if (random <= houseProbability)
                        toInstantiate = houses[Random.Range(0, houses.Length)];
                    else if (random <= houseProbability + obeliskProbability)
                        toInstantiate = obelisks[Random.Range(0, obelisks.Length)];
                    else
                        toInstantiate = biomesPrefabs[biome][Random.Range(0, biomesPrefabs[biome].Length)];
                    Vector3 position = new Vector3((randomPosInSquare.x - mapSize / 2) * mapSizeScale, (randomPosInSquare.y - mapSize / 2) * mapSizeScale, 0);
                    prefab = Instantiate(toInstantiate, position, Quaternion.identity);
                    Vector2 offset = prefab.GetComponent<Collider2D>().bounds.center;
                    prefab.GetComponent<SpriteRenderer>().sortingOrder = (int)((offset.y) * -10);
                    generatedMapPrefabs.Add(prefab);
                }
            }
        }
    }
}