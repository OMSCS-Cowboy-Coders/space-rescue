using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainAssets : MonoBehaviour
{
    // Start is called before the first frame update
    
    float treeScaleMin = 1f;
    float treeScaleMax = 3f;
    float treeAmountMin = 1000f;
    float treeAmountMax = 2000f;
    public GameObject[] treePrefabs;

    float rockScaleMin = 0.1f;
    float rockScaleMax = 1f;
    float rockAmountMin = 500f;
    float rockAmountMax = 1000f;
    public GameObject[] rockPrefabs;

    float plantScaleMin = 1f;
    float plantScaleMax = 10f;
    float plantAmountMin = 750f;
    float plantAmountMax = 1500f;
    public GameObject[] plantPrefabs;
    
    private GameObject TerrainAssetsParent;
    void Start()
    {
        TerrainAssetsParent = new GameObject("TerrainAssetsParent");
        //Generate tree prefabs
        float treeAmount = Random.Range(treeAmountMin, treeAmountMax);
        GenerateAssets(treePrefabs,treeAmount, treeScaleMin, treeScaleMax);
        //Generate rock prefabs
        float rockAmount = Random.Range(rockAmountMin, rockAmountMax);
        GenerateAssets(rockPrefabs, rockAmount, rockScaleMin, rockScaleMax);
        //Generate rock prefabs
        float plantAmount = Random.Range(rockAmountMin, rockAmountMax);
        GenerateAssets(plantPrefabs, plantAmount, plantScaleMin, plantScaleMax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateAssets(GameObject[] prefabs, float amountToGenerate, float scaleMin, float scaleMax){
        //Active terrain, should only be 1
        RaycastHit rayHit;
        TerrainGenerator terrainScript = GetComponent<TerrainGenerator>();
        Terrain terrain = Terrain.activeTerrain;
        Vector3 terrainPos = terrain.transform.position;
        Bounds terrainBounds = terrain.terrainData.bounds;
        for(int i = 0; i < prefabs.Length; i++){
            //Get random amount to generate
            GameObject terrainPrefab = prefabs[i];
            float randomScale = Random.Range(scaleMin, scaleMax);
            int amount = 0;
            while(amount < amountToGenerate){
                Vector3 randomPos = new Vector3();
                Vector3 terrainMin = terrain.terrainData.bounds.min;
                Vector3 terrainMax = terrain.terrainData.bounds.max;
                randomPos.x = UnityEngine.Random.Range(terrainPos.x + terrainMin.x, terrainPos.x + terrainMax.x);
                randomPos.z = UnityEngine.Random.Range(terrainPos.z + terrainMin.z, terrainPos.z + terrainMax.z);
                randomPos.y = terrain.SampleHeight(new Vector3(randomPos.x,0,randomPos.z )) + terrain.transform.position.y + 1.5f;
                //Raycast downwards, get the spot that is hit
                if(Physics.Raycast(randomPos, Vector3.down,  out rayHit) && !rayHit.transform.root.CompareTag("Structure")){
                    //Only generate if it the ray doesn't intersect with a structure
                    randomPos = rayHit.point;
                    // Generate Terrain
                    GameObject terrainAsset = Instantiate(terrainPrefab, randomPos, Quaternion.identity, TerrainAssetsParent.transform);
                    terrainAsset.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                    amount++;
                }
            }
        }
    }
}
