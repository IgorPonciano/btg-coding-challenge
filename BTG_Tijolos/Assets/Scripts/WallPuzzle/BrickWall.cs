using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BrickWall : MonoBehaviour
{
    // * Unity does not serialize a List of Lists, so we can't setup this from the inspector *
    public List<List<int>> wall = new List<List<int>>{
        new List<int> {1,2,2,1},
        new List<int>{3,1,2},
        new List<int>{1,3,2},
        new List<int>{2,4},
        new List<int>{3,1,2},
        new List<int>{1,3,1,1}
    };

    [Header("UI Setup")]
    [SerializeField] private TextMeshProUGUI wallDrawText;
    [SerializeField] private TextMeshProUGUI puzzleAnwerText;

    private void Start()
    {
        ApproachNewWall();
    }

    public string DrawWall(List<List<int>> wall)
    {
        string wallText = "";
        // Draw rows
        foreach (List<int> row in wall)
        {
            // print * for each tile width unit. In the last one add "|" and the start a new tile. At the end of the row, jump a line
            for (int tile = 0; tile < row.Count; tile++)
            {
                wallText += "|";

                for (int i = 1; i <= row[tile]; i++)
                {
                    wallText += "*";

                    if (i != row[tile])
                    {
                        wallText += " ";
                    }
                }

            }
            wallText += "|";
            wallText += "\n";
        }
        return wallText;
    }

    // Create a new random wall, following the restrictions -> WIP
    public List<List<int>> GenerateNewWall(int wallHeightMax, int wallWidth)
    {
        List<List<int>> newWall = new List<List<int>>();

        for(int i = 0; i < Random.Range(3, wallHeightMax+1); i++)
        {
            List<int> row = new List<int>();
            newWall.Add(row);
        }

       foreach (List<int> row in newWall)
       {
        int availableSlots = wallWidth;
        
        do
        {
            int tile = Random.Range(1, availableSlots + 1);
            availableSlots -= tile;
            row.Add(tile);
        } while (availableSlots > 0);
       }
       
        return newWall;
    }
    
public int GetMinCuts(List<List<int>> wall)
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
        for (int tile = 0; tile < row.Count - 1; tile++)
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

public void ApproachNewWall()
{
    List<List<int>> randomWall = GenerateNewWall(10, 6);
    wallDrawText.text = DrawWall(randomWall);
    puzzleAnwerText.text = ($"The minimum number of bricks cut is <color=yellow>{GetMinCuts(randomWall)}");
}
}
