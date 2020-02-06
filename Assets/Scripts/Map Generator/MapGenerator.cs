using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("Auto Generate on Play Start")]
    public bool autoStartGenerating = true;
    public bool mapGeneratorActive = true;
    [SerializeField] GameObject mapCreatorPanel;

    public GameObject playerPosition;
    public Tilemap groundTilemap;
    public Tilemap waterTilemap;
    public Tilemap collisionTilemap;
    bool isColliderTile = false;

    public Text mapSizeText;
    public Slider mapSizeSlider;

    [Header("Map Tiles")]
    public Tile[] tileArray = new Tile[10];
    [Header("Animated Map Tiles")]
    public AnimatedTile[] animatedTileArray = new AnimatedTile[10];

    [Header("Pickup Objects")]
    public GameObject[] pickUps = new GameObject[10];

    public int mapWidth;
    public int mapHeight;

    // For organising objects in hierarchy while play mode
    public Transform instanceGrouping;

    // PERLIN NOISE GENERATION
    [Header("Map Seed")]
    public int seed;
    public bool randomSeed;

    [Header("Noise Values")]
    public float frequency;
    public float amplitude;

    public float lacunarity;
    public float persistance;

    public int octaves;

    public bool useFalloff;
    public float fallOffValueA = 3;
    public float fallOffValueB = 2.2f;
    float[,] falloffMap;



    [Header("Ground Height limits")]
    
    public float beachStartHeight;
    public float beachEndHeight;
    public float dirtStartHeight;
    public float dirtEndHeight;
    public float grassStartHeight;
    public float grassEndHeight;
    public float stoneStartHeight;
    public float stoneEndHeight;

    [Header("Water Height limits")]
    public float deepWaterLevel;
    public float seaLevelStart;
    public float seaLevelEnd;

    PerlinNoise noise;
    float[,] noiseValues;



    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight, fallOffValueA, fallOffValueB);

        if (randomSeed)
        {
            int value = Random.Range(-1000, 10000);
            seed = value;
        }

        noise = new PerlinNoise(seed.GetHashCode(), frequency, amplitude, lacunarity, persistance, octaves);
    }

    void Start()
    {
        mapCreatorPanel.SetActive(true);
        groundTilemap.ClearAllTiles();
        waterTilemap.ClearAllTiles();
        collisionTilemap.ClearAllTiles();

        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        

        
        // Player position
        playerPosition.transform.position = new Vector3(100, 100, 0);
        //Set map size
        groundTilemap.size = new Vector3Int(mapWidth, mapHeight, 1);
        waterTilemap.size = new Vector3Int(mapWidth, mapHeight, 1);
        collisionTilemap.size = new Vector3Int(mapWidth, mapHeight, 1);


        GenerateGround();
        PlaceRandomPickups();

        //Set Camera Bounds
        CameraMovement.instance.SetCameraMax(mapWidth, mapHeight);

        mapCreatorPanel.SetActive(false);
    }

    void GenerateGround()
    {
        noiseValues = noise.GetNoiseValues(mapWidth, mapHeight);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (useFalloff)
                {
                    // SUBSTRACT FALLOFF MAP VALUES
                    noiseValues[x, y] = Mathf.Clamp01(noiseValues[x, y] - falloffMap[x, y]);
                }
                Tile currentTile = MakeTileAtHeight(noiseValues[x, y]);


                groundTilemap.SetTile(new Vector3Int(x, y, 0), currentTile);
            }
        }

        GenerateWaterTiles(noiseValues);
    }

    void PlaceRandomPickups()
    {
        DestroyPickups();

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (groundTilemap.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    string currentTile = groundTilemap.GetTile(new Vector3Int(x, y, 0)).name;

                    int chance = Random.Range(1, 101);

                    int rnd = Random.Range(0, 3);

                    if(noiseValues[x, y] > deepWaterLevel)
                    switch (currentTile)
                    {
                        case "Sand":
                            if (chance >= 96)
                            {
                                Instantiate(pickUps[3], new Vector3(x, y, -2f), pickUps[3].transform.rotation).transform.SetParent(instanceGrouping);
                            }
                            break;
                        case "Grass":
                            if (chance >= 98)
                            {
                                Instantiate(pickUps[rnd], new Vector3(x, y, -2f), pickUps[rnd].transform.rotation).transform.SetParent(instanceGrouping);
                            }
                            break;
                        case "grassTile2":
                            if (chance >= 98)
                            {
                                Instantiate(pickUps[rnd], new Vector3(x, y, -2f), pickUps[rnd].transform.rotation).transform.SetParent(instanceGrouping);
                            }
                            break;
                        case "grassTile3":
                            if (chance >= 98)
                            {
                                Instantiate(pickUps[rnd], new Vector3(x, y, -2f), pickUps[rnd].transform.rotation).transform.SetParent(instanceGrouping);
                            }
                            break;
                    }
                }
            }
        }
    }

    void DestroyPickups()
    {
        GameObject[] pickUps;

        pickUps = GameObject.FindGameObjectsWithTag("Pickups");

        foreach (var item in pickUps)
        {
            Destroy(item);
        }
    }

    Tile MakeTileAtHeight(float currentHeight)
    {

        if (currentHeight >= beachStartHeight && currentHeight <= beachEndHeight)
        {
            isColliderTile = false;
            return tileArray[4]; // SAND
        }

        if (currentHeight >= dirtStartHeight && currentHeight <= dirtEndHeight)
        {
            isColliderTile = false;
            return tileArray[Random.Range(8, 10)]; // SANDGRASS
        }

        if (currentHeight >= grassStartHeight && currentHeight <= grassEndHeight)
        {
            isColliderTile = false;
            return tileArray[Random.Range(0, 3)]; // GRASS
        }

        if (currentHeight >= stoneStartHeight && currentHeight <= stoneEndHeight)
        {
            isColliderTile = false;
            return tileArray[5]; //STONE
        }

        return tileArray[7]; // THIS SHOULD NEVER BE RETURNED

    }

    void GenerateWaterTiles(float[,] heightValues)
    {
        float[,] noiseValues = heightValues;
        
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (noiseValues[x, y] <= seaLevelEnd && noiseValues[x, y] >= seaLevelStart)
                {
                    //Light Water
                    waterTilemap.SetTile(new Vector3Int(x, y, 0), animatedTileArray[0]);
                }
        
                if (noiseValues[x, y] <= deepWaterLevel)
                {
                    //Deep Water
                    collisionTilemap.SetTile(new Vector3Int(x, y, 0), animatedTileArray[1]);
                }
            }
        }
    }
}
