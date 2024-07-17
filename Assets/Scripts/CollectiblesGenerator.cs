using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Collectibles;

    public int collectibleDuplicateCount = 10;

    public int collectibleYOffset = 5;

    private int numUniqueCollectibles;
    private GameObject[] collectiblesRoots;

    private GameObject collectiblesRoot;
    void Start()
    {
        collectiblesRoots = new GameObject[Collectibles.Length];
        numUniqueCollectibles = Collectibles.Length;
        collectiblesRoot = new GameObject("Collectibles");
        for(int i = 0; i < Collectibles.Length; i++){
            string collectibleName = Collectibles[i].name;
            GameObject collectibleRoot = new GameObject(collectibleName + "-root");
            collectibleRoot.transform.parent = collectiblesRoot.transform;
            collectiblesRoots[i] = collectibleRoot;
        }

        Terrain[] terrains = Terrain.activeTerrains;
        for(int i = 0; i < numUniqueCollectibles; i++){
            GameObject collectible = Collectibles[i];
            GameObject collectibleRoot = collectiblesRoots[i];
            for(int j = 0; j < terrains.Length; j++){
            Terrain curTerrain = terrains[j];
            Vector3 curTerrainPos = curTerrain.transform.position;
            Vector3 curTerrainMin = curTerrain.terrainData.bounds.min;
            Vector3 curTerrainMax = curTerrain.terrainData.bounds.max;

                for(int k = 0; k < collectibleDuplicateCount; k++){
                    float randomX = UnityEngine.Random.Range(curTerrainPos.x + curTerrainMin.x, curTerrainPos.x + curTerrainMax.x);
                    float randomZ = UnityEngine.Random.Range(curTerrainPos.z + curTerrainMin.z, curTerrainPos.z + curTerrainMax.z);
                    float yPos = curTerrain.SampleHeight(new Vector3(randomX,0,randomZ)) + curTerrain.transform.position.y + 1.5f;
                    Instantiate(collectible, new Vector3(randomX, yPos + collectibleYOffset, randomZ), Quaternion.identity, collectibleRoot.transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
