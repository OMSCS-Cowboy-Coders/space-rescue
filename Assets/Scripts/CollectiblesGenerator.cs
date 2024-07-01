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
        for(int i = 0; i < terrains.Length; i++){
            Terrain curTerrain = terrains[i];
            Vector3 curTerrainMin = curTerrain.terrainData.bounds.min;
            Vector3 curTerrainMax = curTerrain.terrainData.bounds.max;
            float yPos = curTerrain.SampleHeight(new Vector3(0,0,0));

            for(int j = 0; j < numUniqueCollectibles; j++){
                GameObject collectible = Collectibles[j];
                GameObject collectibleRoot = collectiblesRoots[j];
                for(int k = 0; k < collectibleDuplicateCount; k++){
                    float randomX = UnityEngine.Random.Range(curTerrainMin.x, curTerrainMax.x);
                    float randomZ = UnityEngine.Random.Range(curTerrainMin.z, curTerrainMax.z);
                    Instantiate(collectible, new Vector3(randomX, yPos, randomZ), Quaternion.identity, collectibleRoot.transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
