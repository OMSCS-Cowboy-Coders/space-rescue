using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainAssets : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject[] terrainPrefabs;
    private GameObject TerrainAssetsParent;
    void Start()
    {
        TerrainAssetsParent = new GameObject("TerrainAssetsParent");
        GenerateAssets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateAssets(){
        //Active terrain, should only be 1
        RaycastHit rayHit;
        //Random amount for whatever we are spawning
        int randomMin = Random.Range(100,200);
        int randomMax = Random.Range(300,500);
        TerrainGenerator terrainScript = GetComponent<TerrainGenerator>();
        Terrain terrain = Terrain.activeTerrain;
        Vector3 terrainPos = terrain.transform.position;
        Bounds terrainBounds = terrain.terrainData.bounds;
        for(int i = 0; i < terrainPrefabs.Length; i++){
            //Get random amount to generate
            int randomNum = Random.Range(randomMin,randomMax);
            GameObject terrainPrefab = terrainPrefabs[i];
            for(int j = 0; j < randomNum; j ++){
                Vector3 randomPos = new Vector3();
                Vector3 terrainMin = terrain.terrainData.bounds.min;
                Vector3 terrainMax = terrain.terrainData.bounds.max;
                randomPos.x = UnityEngine.Random.Range(terrainPos.x + terrainMin.x, terrainPos.x + terrainMax.x);
                randomPos.z = UnityEngine.Random.Range(terrainPos.z + terrainMin.z, terrainPos.z + terrainMax.z);
                randomPos.y = terrain.SampleHeight(new Vector3(randomPos.x,0,randomPos.z )) + terrain.transform.position.y + 1.5f;

                // Vector3 randomPos = Vector3.Lerp(randomWorldPosMin, randomWorldPosMax, UnityEngine.Random.value);
                //Convert randomPos coords to normalized terrain cords, assuming it is in the terrain
                // Vector3 randomPosNormalized = terrainScript.WorldToTerrain(terrain, randomPos);
                // Vector3 randomPosClamped = terrainScript.clampPositionWithinTerrain(terrain, randomPosNormalized);
                // //Convert normalized clamped position to world position
                // Vector3 randomPosWorld = terrainScript.TerrainToWorld(terrain, randomPosClamped);
                //Raycast downwards, get the spot that is hit
                Physics.Raycast(randomPos, Vector3.down,  out rayHit);
                randomPos = rayHit.point;
                // Generate Enemy
                GameObject terrainAsset = Instantiate(terrainPrefab, randomPos, Quaternion.identity, TerrainAssetsParent.transform);
            }
        }
    }
}
