namespace IsingModelSim{
using IsingModel;
using System;


unsafe static class IsingModelSim
{
    static void Main()
    {
        float ambientMagneticField = 0f;
        float beta =.5f; 
        int n = 100;
        int m = 100;
        int p = 1;
        beta = .5f;
        ambientMagneticField = 0f;
        IsingModel IsingModel_ = new IsingModel(&beta, &ambientMagneticField, n, m, p);
        IsingModel_.InitializeRandomly();

        for (int i = 0; i < 100; i++)
        {
            IsingModel_.IsingGlobalUpdate();
            Console.WriteLine(IsingModel_);
        }
    }
}
}
