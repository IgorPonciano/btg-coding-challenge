using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrickWall : MonoBehaviour
{
    public List<List<int>> wall = new List<List<int>>{
        new List<int> {2,2,2},
        new List<int>{3,1,1,1},
        new List<int>{2,4},
        new List<int>{3,1,2},
        new List<int>{1,3,1,1},
        new List<int>{3,3}
    };

    private void Start()
    {
        Debug.Log($"The minimum number of bricks cut is {GetMinCuts(wall)}");
    }
    public int GetMinCuts(List<List<int>> wall) // maybe change to an array of arrays (?)
    {
        if (wall == null || wall.Count == 0) // handles not valid input values
        {
            return 0;
        }
        // Dictionary to store gaps between bricks in a row. [position, numberOfGaps]
        Dictionary<int, int> dictOfGaps = new Dictionary<int, int>(wall.Count);

        foreach (List<int> row in wall)
        {
            int pGap = 0;

            // After each tile, count 1 gap at that position. Don't count for the last, because it would be the right border
            for (int tile = 1; tile < row.Count; tile++)
            {
                // Example: 2 | 2 | 2 -> gaps are in p2 and p4
                pGap += row[tile];

                try
                {
                    dictOfGaps[pGap] += 1;
                }
                catch (KeyNotFoundException ex)
                {
                    dictOfGaps.Add(pGap, 1);
                }
                
            }
        }
        // min number of bricks cut = number of rows - max gaps used
        return wall.Count - dictOfGaps.OrderByDescending(p => p.Value).First().Value;
    }
}
