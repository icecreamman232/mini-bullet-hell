using System;

[Serializable]
public class WeightObject
{
    public float Weight;
    public float RangeFrom;
    public float RangeTo;

    public static void ComputeSpawnArray(WeightObject[] spawns)
    {
        float totalWeight = 0;
        for (int i = 0; i < spawns.Length; i++)
        {
            totalWeight += spawns[i].Weight;
        }
            
        float curPercent = 0;
        for (int i = 0; i < spawns.Length; i++)
        {
            spawns[i].RangeFrom = curPercent;
            spawns[i].RangeTo = spawns[i].RangeFrom + spawns[i].Weight / totalWeight * 100;
            curPercent = spawns[i].RangeTo;
        }
    }

    public static int GetWeightIndex(WeightObject[] spawns ,float percent)
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            if (spawns[i].RangeFrom <= percent
                && spawns[i].RangeTo >= percent)
            {
                return i;
            }
        }

        return 0;
    }
}
