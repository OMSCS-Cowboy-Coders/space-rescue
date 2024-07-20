using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    //Terrain related variables
    public int terrainScale = 10;
    public int terrainHeight = 30;
    public int terrainWidth = 1000;
    public int terrainLength = 1000;
    
    //Mountain related variables
    public int mountainWidthOffset = 100;
    public int mountainLengthOffset = 100;
    public int mountainScale = 100;

    public string STRUCTURE_TAG = "Structure";

    //Boundary related variables.
    void Start()
    {
        //Generate terrain data
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        // clampStructures();
        //Rememeber to comment out below for final release.
        // NavMeshBuilder.ClearAllNavMeshes();
        // NavMeshBuilder.BuildNavMesh();

    }

    // Update is called once per frame
    void Update()
    {
        // Terrain terrain = GetComponent<Terrain>();
        // terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    public Terrain getClosestTerrain(GameObject gameObject){
        Terrain[] terrains = Terrain.activeTerrains;
        Terrain terrain = null;
        Vector3 curGameObjectPos = gameObject.transform.position;
        float closestDist = Mathf.Infinity;
        for(int i = 0; i < terrains.Length; i++){
            Terrain curTerain = terrains[i];
            Vector3 curTerrainPos = curTerain.GetPosition();
            float curDist = (curTerrainPos - curGameObjectPos).sqrMagnitude;
            if(curDist < closestDist){
                terrain = curTerain;
                closestDist = curDist;
            }
        }
        return terrain;
    }

    TerrainData GenerateTerrain(TerrainData terrainData){
        terrainData.heightmapResolution = terrainWidth + 1;
        terrainData.size = new Vector3(terrainWidth, terrainHeight, terrainLength);
        float[,] terrainHeights = new float[terrainWidth,terrainLength];
        GenerateGroundTerrainData(terrainHeights);
        GenerateBoundaryTerrainData(terrainHeights);
        terrainData.SetHeights(0,0, terrainHeights);      
        return terrainData;
    }

    void GenerateGroundTerrainData(float[,] terrainHeights){
       for(int i = mountainWidthOffset; i < terrainWidth - mountainWidthOffset; i ++){
            for (int j = mountainLengthOffset; j < terrainLength - mountainLengthOffset; j++){
                terrainHeights[i,j] = CalculateNoise(i,j, terrainScale);
            }
        }
    }
    
    void GenerateBoundaryTerrainData(float[,] terrainHeights){
        //From x = [0,offset], y = [0,end]
        for(int i = 0; i < mountainWidthOffset; i ++){
            for (int j = 0; j < terrainLength; j++){
                terrainHeights[i,j] = CalculateNoise(i,j, mountainScale);
            }
        }
        // From x = [terrain - offset, end] -> end, y = [0,end]
         for(int i = terrainWidth - mountainWidthOffset; i < terrainWidth; i ++){
            for (int j = 0; j < terrainLength; j++){
                terrainHeights[i,j] = CalculateNoise(i,j, mountainScale);
            }
        }
        // From x = [0,end], y = [0,offset]
        for(int i = 0; i < terrainWidth; i ++){
            for (int j = 0; j < mountainLengthOffset; j++){
                terrainHeights[i,j] = CalculateNoise(i,j, mountainScale);
            }
        }
        // From x  = [0,end], y = [terrain - offset, end]
        for(int i = 0; i < terrainWidth; i ++){
            for (int j = terrainLength - mountainLengthOffset; j < terrainLength; j++){
                terrainHeights[i,j] = CalculateNoise(i,j, mountainScale);
            }
        }
    }

    public float CalculateNoise(int xOrg, int yOrg, int scale){
        float xCoord = (float) xOrg  / terrainWidth * scale;
        float yCoord = (float) yOrg / terrainLength * scale;
        return Mathf.PerlinNoise(xCoord,yCoord);
    }

    public GameObject CreateBoundaryObject(Vector3 position, Quaternion rotation){
        GameObject boundary = new GameObject("Boundary");
        boundary.transform.position = position;
        boundary.transform.rotation = rotation;
        boundary.AddComponent<BoxCollider>();
        return boundary;
    }

    public Vector3 WorldToTerrain(Terrain terrain, Vector3 worldCords){
        Vector3 terrainCords = new Vector3();
        terrainCords.x = (worldCords.x - terrain.transform.position.x) / terrain.terrainData.size.x;
        terrainCords.y = (worldCords.y - terrain.transform.position.y) / terrain.terrainData.size.y;
        terrainCords.z = (worldCords.z - terrain.transform.position.z) / terrain.terrainData.size.z;
        return terrainCords;
    }
    public Vector3 TerrainToWorld (Terrain terrain, Vector3 normaliedCords){
        Vector3 worldCords = new Vector3();
        worldCords.x = (normaliedCords.x * terrain.terrainData.size.x) + terrain.transform.position.x;
        worldCords.y = (normaliedCords.y * terrain.terrainData.size.y) + terrain.transform.position.y;
        worldCords.z = (normaliedCords.z * terrain.terrainData.size.z) + terrain.transform.position.z;
        return worldCords;
    }

    void clampStructures(){
        GameObject[] structures = GameObject.FindGameObjectsWithTag(STRUCTURE_TAG);
        for(int i = 0; i < structures.Length; i++){
            GameObject structureObj = structures[i];
            MeshFilter[] meshChildren = structureObj.GetComponentsInChildren<MeshFilter>();
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            for(int j = 0; j < meshChildren.Length; j++){
                MeshFilter childMesh = meshChildren[j];
                bounds.Encapsulate(childMesh.mesh.bounds);
            }
            clampStructure(structureObj, bounds);
        }
    }

    void clampStructure(GameObject structure, Bounds bounds){
        //Get active terrain for structure
        RaycastHit rayHit;
        Terrain closestTerrain = getClosestTerrain(structure);
        int terrainWidth = closestTerrain.terrainData.heightmapResolution;
        int terrainLength = closestTerrain.terrainData.heightmapResolution;
        Debug.Log("Terrain Width: " + terrainWidth);
        float[,] terrainHeights = closestTerrain.terrainData.GetHeights(0,0, terrainWidth, terrainLength);
        // Vector3 boundsMin = bounds.min;
        // Vector3 boundsMax = bounds.max;
        int boundsMinX = Mathf.FloorToInt(bounds.min.x); //Convert world to local
        int boundsMaxX = Mathf.CeilToInt(bounds.max.x);
        int boundsMinZ = Mathf.FloorToInt(bounds.min.z);
        int boundsMaxZ = Mathf.CeilToInt(bounds.max.z);
        TerrainGenerator terrainScript = GetComponent<TerrainGenerator>();
        //Go through bound
        for(int i = 0; i < terrainWidth; i++){
            for(int j = 0; j < terrainLength; j++){
                Vector3 terrainWorldSpacePos = closestTerrain.transform.position;
                Vector3 pointWorldSpacePos = new Vector3(0,0,0);
                pointWorldSpacePos.x = terrainWorldSpacePos.x + ((i * closestTerrain.terrainData.size.x) / terrainWidth);
                pointWorldSpacePos.y = terrainWorldSpacePos.y;
                pointWorldSpacePos.z = terrainWorldSpacePos.z + ((i * closestTerrain.terrainData.size.x) / terrainLength);
                //Raycast upwards and get the distance
                terrainHeights[j,i] = structure.transform.position.y / closestTerrain.terrainData.size.y;
                //Update heightmap
            }
        }
        //Set after updating
        closestTerrain.terrainData.SetHeights(0,0,terrainHeights);
    }
}
