namespace IsingModelSim{
using UnityEngine;
using IsingModelGraphicalUnity;
using System;


public unsafe class IsingModelSim : MonoBehaviour
{
    public IsingModelGraphicalUnity IsingModel_;
    public Material transparentMaterial;
    [SerializeField] [Range(-5f, 5f)] public float ambientMagneticField;
    [SerializeField] [Range(0.0f, 1f)] public float beta; 
    [SerializeField] public Vector3Int dimensions;
    
    

    void Start()
    {
        beta = .5f;
        ambientMagneticField = 0f;
        dimensions = new Vector3Int(60, 60, 1);
        fixed(float* betaPtr = &beta, ambientMagneticFieldPtr = &ambientMagneticField)
        {
            IsingModel_ = new IsingModelGraphicalUnity(betaPtr, ambientMagneticFieldPtr, transparentMaterial, dimensions.x, dimensions.y, dimensions.z);
            IsingModel_.InitializeRandomly();
            IsingModel_.UpdateSquareColors();
        }
    }

    void Update()
    {
        IsingModel_.IsingGlobalUpdate();
        IsingModel_.UpdateSquareColors();

        if (Input.GetKey (KeyCode.Return))
        {
            IsingModel_.DestroyCubes();
            if (dimensions.z == 1) 
            {
                IsingModel_.transparency_white = 1f;
                IsingModel_.transparency_black = 1f;
            }
            else 
            {
                IsingModel_.transparency_white = 0f;
                IsingModel_.transparency_black = .6f;
            }
            fixed(float* betaPtr = &beta, ambientMagneticFieldPtr = &ambientMagneticField)
            {
                IsingModel_ = new IsingModelGraphicalUnity(betaPtr, ambientMagneticFieldPtr, transparentMaterial, dimensions.x, dimensions.y, dimensions.z);
                IsingModel_.InitializeRandomly();
                IsingModel_.UpdateSquareColors();
            }
        }
    }
}
}
