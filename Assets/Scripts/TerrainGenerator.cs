using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    //Terrain related variables
    public int terrainScale = 30;
    public int terrainHeight = 30;
    public int terrainWidth = 1000;
    public int terrainLength = 1000;
    
    //Mountain related variables
    public int mountainWidthOffset = 100;
    public int mountainLengthOffset = 100;
    public int mountainScale = 100;

    //Boundary related variables.
    void Start()
    {
        //Generate terrain data
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    // Update is called once per frame
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
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

    float CalculateNoise(int xOrg, int yOrg, int scale){
        float xCoord = (float) xOrg  / terrainWidth * scale;
        float yCoord = (float) yOrg / terrainLength * scale;
        return Mathf.PerlinNoise(xCoord,yCoord);
    }

    GameObject CreateBoundaryObject(Vector3 position, Quaternion rotation){
        GameObject boundary = new GameObject("Boundary");
        boundary.transform.position = position;
        boundary.transform.rotation = rotation;
        boundary.AddComponent<BoxCollider>();
        return boundary;
    }
}
