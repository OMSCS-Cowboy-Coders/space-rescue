using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainAssets : MonoBehaviour
{
    // Start is called before the first frame update
    
    float treeScaleMin = 2.5f;
    float treeScaleMax = 5f;
    float treeAmountMin = 250f;
    float treeAmountMax = 500f;
    public GameObject[] treePrefabs;

    float rockScaleMin = 0.1f;
    float rockScaleMax = 1f;
    float rockAmountMin = 250;
    float rockAmountMax = 500f;
    public GameObject[] rockPrefabs;

    float plantScaleMin = 1f;
    float plantScaleMax = 10f;
    float plantAmountMin = 300f;
    float plantAmountMax = 600f;
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
        //Culminate all colliders initially for structures
       
        GenerateAssets(plantPrefabs, plantAmount, plantScaleMin, plantScaleMax);
    }

    // Update is called once per frame
    void Update()
    {
    }

    Bounds culminateBoundsOfChildren(Transform parent){
        Bounds bounds  = new Bounds();
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        for(int i = 0; i < colliders.Length; i++){
            Collider collider = colliders[i];
            bounds.Encapsulate(collider.bounds);
        }
        return bounds;
    }
    bool isProblematicLocation(RaycastHit rayhit, Vector3 position){
        string[] collisionTags = {"Structure", "TerrainAsset", "Player"};

        //Check collider tag collision
        for(int i = 0; i < collisionTags.Length; i++){
            string collisionTag = collisionTags[i];
            //Check if collided structure's bounds is within proposed position
            //Get top level root
            if(rayhit.transform.root.CompareTag(collisionTag) || rayhit.collider.CompareTag(collisionTag)){
                //Expand collider temporarily and check if it's within bounds
                if(rayhit.collider.bounds.Contains(position)){
                    return true;
                }
            }
        }
        return false;
    }
    void GenerateAssets(GameObject[] prefabs, float amountToGenerate, float scaleMin, float scaleMax){
        //Active terrain, should only be 1
        RaycastHit rayHit;
        TerrainGenerator terrainScript = GetComponent<TerrainGenerator>();
        Terrain terrain = Terrain.activeTerrain;
        Vector3 terrainPos = terrain.transform.position;
        Vector3 terrainMin = terrain.terrainData.bounds.min;
        Vector3 terrainMax = terrain.terrainData.bounds.max;
        for(int i = 0; i < prefabs.Length; i++){
            //Get random amount to generate
            GameObject terrainPrefab = prefabs[i];
            int amount = 0;
            while(amount < amountToGenerate){
                float randomScale = Random.Range(scaleMin, scaleMax);
                Quaternion randomRotation = Random.rotation;
                randomRotation.x = 0;
                randomRotation.z = 0;
                Vector3 randomPos = new Vector3();
                randomPos.x = UnityEngine.Random.Range(terrainPos.x + terrainMin.x, terrainPos.x + terrainMax.x);
                randomPos.z = UnityEngine.Random.Range(terrainPos.z + terrainMin.z, terrainPos.z + terrainMax.z);
                randomPos.y = terrain.SampleHeight(new Vector3(randomPos.x,0,randomPos.z )) + terrain.transform.position.y + 1.0f;
                //Raycast 
                if(Physics.Raycast(randomPos, Vector3.down,  out rayHit)&& !isProblematicLocation(rayHit, randomPos)){
                    //Only generate if it the ray doesn't intersect with a structure
                    randomPos = rayHit.point;
                    // Generate Terrain
                    GameObject terrainAsset = Instantiate(terrainPrefab, randomPos, Quaternion.identity, TerrainAssetsParent.transform);
                    terrainAsset.transform.localScale = new Vector3(randomScale,randomScale,randomScale);
                    terrainAsset.transform.localRotation = randomRotation;
                    amount++;
                }
            }
        }
    }
}
