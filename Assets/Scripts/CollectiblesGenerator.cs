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

    public int collectibleYOffset = 2;


    private int numUniqueCollectibles;
    private GameObject[] collectiblesRoots;

    private GameObject collectiblesRoot;
    
    void Start()
    {
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

    bool isProblematicLocation(RaycastHit rayhit, Vector3 position, GameObject preFab){
        string[] collisionTags = {"Structure", "TerrainAsset", "Player", "Border"};
        Collider preFabCollider = preFab.GetComponent<Collider>();
        //Check collider tag collision
        for(int i = 0; i < collisionTags.Length; i++){
            string collisionTag = collisionTags[i];
            //Check if collided structure's bounds is within proposed position
            //Get top level root
            if(rayhit.collider.CompareTag(collisionTag) ){
                //Expand structure collider temporarily and check if it's within bounds
                if(rayhit.collider.bounds.Intersects(preFabCollider.bounds)){
                    return true;
                }
            }
        }
        return false;
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
                randomPos.y = curTerrain.SampleHeight(new Vector3(randomPos.x,0,randomPos.z)) + curTerrain.transform.position.y;
                if(Physics.Raycast(randomPos, Vector3.down,  out rayHit) && !isProblematicLocation(rayHit, randomPos, collectible)){
                    randomPos = rayHit.point;
                    randomPos.y += collectibleYOffset;
                    Instantiate(collectible, randomPos, Quaternion.identity, collectibleRoot.transform);
                    collectiblesCount[i]++;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }
   
}