namespace IsingModel{
using System;

public unsafe class IsingModel
{
    public bool[,,] ferromagneticChunk; 
    public float* beta; 
    public float* ambientMagneticField;
    readonly Random random; 


    //Constructor

    public IsingModel(float* beta_, float* ambientMagneticField_, int n = 100, int m = 100, int p = 1)
    {
        ferromagneticChunk = new bool[n, m, p];
        beta = beta_;
        ambientMagneticField = ambientMagneticField_;
        random = new Random();
    }


    //Quick conversion methods

    public int ToPlusMinusOne(bool b) {if (b) return 1; return -1;}
    public string ToPlusMinusOneString(bool b) {if (b) return " "; return "0";}

    public override string ToString() 
    {
        string written = "";
        for (int k = 0; k < ferromagneticChunk.GetLength(2); k++)
        {
            for (int j = 0; j < ferromagneticChunk.GetLength(1); j++)
            {
                for (int i = 0; i < ferromagneticChunk.GetLength(0); i++) 
                {
                    written += ToPlusMinusOneString(ferromagneticChunk[i, j, k]);
                }
                written += "\n";
            }
            written += "\n";
        }
        return written;
    }


    //Methods generating random numbers

    public bool RandomBoolean() {return random.Next(2) == 1;}

    public float NormDistRandomVariable(float mean, float stdError)
    { 
        double u1 = 1.0 - random.NextDouble();                                                   //follows uniform(0,1]
        double u2 = 1.0 - random.NextDouble();
        float randStdNormal = (float) (Math.Sqrt(-2.0*Math.Log(u1))*Math.Sin(2.0*Math.PI*u2));   //follows normal(0,1)
        return mean + stdError*randStdNormal;                                                    //follows normal(mean, stdError^2)
    }


    //Simulation methods

    public void InitializeRandomly()
    {
        for (int i = 0; i < ferromagneticChunk.GetLength(0); i++) 
        {
            for (int j = 0; j < ferromagneticChunk.GetLength(1); j++)
            {
                for (int k = 0; k < ferromagneticChunk.GetLength(2); k++)
                {
                    ferromagneticChunk[i, j, k] = RandomBoolean();
                }
            }
        }
    }

    public void IsingLocalUpdate(int i, int j, int k)
    {
        float energy = *ambientMagneticField;
        int n = ferromagneticChunk.GetLength(0);
        int m = ferromagneticChunk.GetLength(1);
        int p = ferromagneticChunk.GetLength(2);

        if (p == 1)
        {
            for (int a = -1; a < 2; a++)
            {
                for (int b = -1; b < 2; b++)
                { 
                    if (a != 0 | b != 0) {energy += ToPlusMinusOne(ferromagneticChunk[(n + i + a) % n, (m + b + j) % m, k]);}
                }
            }
        }
        else
        {
            for (int a = -1; a < 2; a++)
            {
                for (int b = -1; b < 2; b++)
                {
                    for (int c = -1; c < 2; c++)
                    {
                        if (a != 0 | b != 0 | c != 0) {energy += ToPlusMinusOne(ferromagneticChunk[(n + i + a) % n, (m + b + j) % m, (p + k + c) % p]);}
                    }
                }
            }
        }
        
        energy *= ToPlusMinusOne(ferromagneticChunk[i, j, k]);
        if (energy <= 0) {ferromagneticChunk[i, j, k] = !ferromagneticChunk[i, j, k];}
        else if (Math.Exp(-2*energy*(*beta)) > random.NextDouble()) {ferromagneticChunk[i, j, k] = !ferromagneticChunk[i, j, k];}
    }

    public void IsingGlobalUpdate() 
    {
        //The phase shifts in the first three for loops aim to emulate somewhat independant updates, 
        //so as to avoid a visible sweeping effect. 
        for (int nPhaseShift = 0; nPhaseShift < 2; nPhaseShift++)
        {
            for (int mPhaseShift = 0; mPhaseShift < 2; mPhaseShift++)
            {
                for (int pPhaseShift = 0; pPhaseShift < 2; pPhaseShift++)
                {
                    for (int i = 0; nPhaseShift + i < ferromagneticChunk.GetLength(0); i+=2) 
                    {
                        for (int j = 0; mPhaseShift + j < ferromagneticChunk.GetLength(1); j+=2)
                        {
                            for (int k = 0; pPhaseShift + k < ferromagneticChunk.GetLength(2); k+=2)
                            {
                                IsingLocalUpdate(nPhaseShift + i, mPhaseShift + j, pPhaseShift + k);
                            }
                        }
                    }
                }
            }
        }
    }

}
}
