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

    public int[] collectiblesCount;

    public int collectibleInitialCount = 10;
    public int collectibleDuplicateCount = 0;

    public int collectibleYOffset = 5;

    private int numUniqueCollectibles;
    private GameObject[] collectiblesRoots;

    private GameObject collectiblesRoot;
    
    void Start()
    {
        RaycastHit rayHit;
        collectiblesRoots = new GameObject[Collectibles.Length];
        collectiblesCount = new int[Collectibles.Length];
        numUniqueCollectibles = Collectibles.Length;
        collectiblesRoot = new GameObject("Collectibles");
        for(int i = 0; i < Collectibles.Length; i++){
            string collectibleName = Collectibles[i].name;
            GameObject collectibleRoot = new GameObject(collectibleName + "-root");
            collectibleRoot.transform.parent = collectiblesRoot.transform;
            collectiblesRoots[i] = collectibleRoot;
            collectiblesCount[i] = 0;
        }
        
        collectibleDuplicateCount = collectibleInitialCount;

        StartCoroutine(spawnCollectables());
    }

    // Update is called once per frame
    void Update()
    {
        collectibleDuplicateCount = collectibleInitialCount + (int) Time.realtimeSinceStartup;
    }

    IEnumerator spawnCollectables(){
        //Go through collectibles and make sure they are a certain amount
        RaycastHit rayHit;
        Terrain curTerrain = Terrain.activeTerrain;
        Vector3 curTerrainPos = curTerrain.transform.position;
        Vector3 curTerrainMin = curTerrain.terrainData.bounds.min;
        Vector3 curTerrainMax = curTerrain.terrainData.bounds.max;
        for(int i = 0; i < Collectibles.Length; i++){
            GameObject collectible = Collectibles[i];
            GameObject collectibleRoot = collectiblesRoots[i];
            while(collectiblesCount[i] < collectibleDuplicateCount){
                Vector3 randomPos = new Vector3();
                randomPos.x = UnityEngine.Random.Range(curTerrainPos.x + curTerrainMin.x, curTerrainPos.x + curTerrainMax.x);
                randomPos.z = UnityEngine.Random.Range(curTerrainPos.z + curTerrainMin.z, curTerrainPos.z + curTerrainMax.z);
                randomPos.y = curTerrain.SampleHeight(new Vector3(randomPos.x,0,randomPos.z)) + curTerrain.transform.position.y + 1.5f;
                if(Physics.Raycast(randomPos, Vector3.down,  out rayHit) && !rayHit.transform.root.CompareTag("Structure") && !rayHit.collider.CompareTag("TerrainAsset")){
                    randomPos = rayHit.point;
                    Instantiate(collectible, randomPos, Quaternion.identity, collectibleRoot.transform);
                    collectiblesCount[i]++;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }
   
}
