using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System.Collections;

public class BrickWall : MonoBehaviour
{
    // * Unity does not serialize a List of Lists, so we can't setup this from the inspector *
    private List<List<int>> wall = new List<List<int>>{
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
    [SerializeField] private TextMeshProUGUI puzzleDescriptionText;

    [Space(15)]
    [SerializeField] private int wallHeightMax = 1000;
    [SerializeField] private int wallWidthMax = 6;

    [Space(15)]
    [Tooltip("Use with small values for wallHeight and Width")]
    [SerializeField] private bool drawWall = false;
    [SerializeField] private GameObject loadingIcon;

    private void Start()
    {
        ApproachNewWall();
    }

    public IEnumerator DrawWall(List<List<int>> wall, TextMeshProUGUI wallText)
    {
        loadingIcon.SetActive(true);
        string newWallText = "";
        // Draw rows
        foreach (List<int> row in wall)
        {
            // print * for each tile width unit. In the last one add "|" and the start a new tile. At the end of the row, jump a line
            for (int tile = 0; tile < row.Count; tile++)
            {
                newWallText += "|";

                for (int i = 1; i <= row[tile]; i++)
                {
                    newWallText += "*";

                    if (i != row[tile])
                    {
                        newWallText += " ";
                    }
                    yield return null;
                }

            }
            newWallText += "|";
            newWallText += "\n";
        }
        loadingIcon.SetActive(false);
        wallText.text = newWallText;
    }

    // Create a new random wall, following the restrictions
    public List<List<int>> GenerateNewWall(int wallHeight, int wallWidth)
    {
        List<List<int>> newWall = new List<List<int>>();

        System.Random newRandom = new System.Random();

        for (int i = 0; i < wallHeight; i++)
        {
            List<int> row = new List<int>();
            newWall.Add(row);
        }

        foreach (List<int> row in newWall)
        {
            int availableSlots = wallWidth;

            do
            {
                // define tile width
                int tile = newRandom.Next(1, availableSlots + 1);
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

        // Avoid case where there is no elements in the dict of gaps
        if (dictOfGaps.Count == 0)
        {
            return wall.Count;
        }
        // min number of bricks cut = number of rows - max gaps used * Avoid case where there is no elements in the dict
        return wall.Count - dictOfGaps.OrderByDescending(p => p.Value).First().Value;
    }

    public void ApproachNewWall()
    {
        StopAllCoroutines();

        System.Random random = new System.Random();
        int wallWidth = random.Next(6, wallWidthMax + 1);
        int wallHeight = random.Next(3, wallHeightMax + 1);

        List<List<int>> randomWall = GenerateNewWall(wallHeight, wallWidth);
        puzzleDescriptionText.text = ($"Wall is {wallHeight} meters tall and {wallWidth} meters wide");

        puzzleAnwerText.text = ($"The minimum number of bricks cut is <color=yellow>{GetMinCuts(randomWall)}");

        if (drawWall)
        {
            StartCoroutine(DrawWall(randomWall, wallDrawText));
        }
    }
}
