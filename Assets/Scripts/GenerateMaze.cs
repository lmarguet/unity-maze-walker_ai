using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    private const int Width = 40;
    private const int Depth = 40;
    private const int StartZoneSize = 10;

    public GameObject BlockPrefab;

    public void Awake()
    {
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Depth; j++)
            {
                var isPerimter = i == 0 || j == 0 || i == Width - 1 || j == Depth - 1;
                var isInStartZone = i < StartZoneSize && j < StartZoneSize;

                if (!isPerimter && isInStartZone)
                {
                    continue;
                }

                var createBlockInside = Random.Range(0, 5) < 1;

                if (isPerimter || createBlockInside)
                {
                    CreateBlock(i, j);
                }
            }
        }
    }

    private void CreateBlock(int w, int d)
    {
        var position = new Vector3(w + transform.position.x, transform.position.y, d + transform.position.z);
        Instantiate(BlockPrefab, position, Quaternion.identity);
    }
}