namespace IsingModelGraphicalUnity{
using System.Collections.Generic;
using UnityEngine;

public unsafe class IsingModelGraphicalUnity : IsingModel.IsingModel
{
    public GameObject[,,] cubes;
    public float transparency_white;
    public float transparency_black;


    //Constructors

    public IsingModelGraphicalUnity(float* beta_, float* ambientMagneticField_, Material material, int n = 60, int m = 60, int p = 1) : base(beta_, ambientMagneticField_, n, m, p)
    {
        cubes = new GameObject[n, m, p];
        if (p == 1) 
        {
            transparency_white = 1f;
            transparency_black = 1f;
        }
        else 
        {
            transparency_white = 0.1f;
            transparency_black = .6f;
        } 

        float centerX = n/2f;
        float centerY = m/2f;
        for (int i = 0; i < n; i++) 
        {
            for (int j = 0; j < m; j++)
            {
                for (int k = 0; k < p; k++)
                {
                    cubes[i, j, k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubes[i, j, k].GetComponent<Renderer>().material = material;
                    cubes[i, j, k].transform.position = new Vector3(i - centerX, j - centerY, k); 
                }
            }
        }
    }


    //Graphical update methods

    public void UpdateSquareColor(int i, int j, int k)
    {
        if (ferromagneticChunk[i, j, k]) {cubes[i, j, k].GetComponent<Renderer>().material.color = new Color(1, 1, 1, transparency_white);}
        else {cubes[i, j, k].GetComponent<Renderer>().material.color = new Color(0, 0, 0, transparency_black);}
    }

    public void UpdateSquareColors()
    {
        int n = ferromagneticChunk.GetLength(0);
        int m = ferromagneticChunk.GetLength(1);
        int p = ferromagneticChunk.GetLength(2);
        for (int i = 0; i < n; i++) 
        {
            for (int j = 0; j < m; j++)
            {
                for (int k = 0; k < p; k++)
                {
                    UpdateSquareColor(i, j, k);
                }
            }
        }
    }


    //Destructor

    public void DestroyCubes()
    {
        foreach (GameObject cube in cubes) {GameObject.Destroy(cube);}
    }

    ~IsingModelGraphicalUnity() {DestroyCubes();}
}
}