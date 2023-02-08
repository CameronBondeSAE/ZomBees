using Unity.Jobs;
using UnityEngine;

public struct CSharpJob : IJob
{
    public void Execute()
    {
        float answer = 0;

        for (int i = 0; i < 10000000; i++)
        {
            answer+=Mathf.Sqrt(i)+Mathf.PerlinNoise(i*1.24f,0);
        }
    
        Debug.Log("I did something! : "+answer);
    }
}
