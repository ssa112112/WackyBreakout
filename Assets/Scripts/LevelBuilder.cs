using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject paddlePrefab;
    [SerializeField]
    GameObject BlockPrefab;

    float blockHeight;
    float blockWidth;

    //Support with blocks spawning
    float distanceBetweenBlocksInProcents = 0.2f; //[0f...1f]
    float distanceFromTopInProcents = 0.1f; //[0f...1f]
    static int amountBlocksInStart; //autocalculate

    public static int AmountBlocksInStart
    {
        get { return amountBlocksInStart;}
    }


    void Awake()
    {
        SaveShapeOfBlock();
        Instantiate(paddlePrefab);
        CreateBlocks();
    }

    /// <summary>
    /// Need to do it before create blocks
    /// </summary>
    private void SaveShapeOfBlock()
    {
        var tempBlock = Instantiate(BlockPrefab);
        var tempBlockCollider = tempBlock.GetComponent<BoxCollider2D>();
        blockHeight = tempBlockCollider.size.y;
        blockWidth = tempBlockCollider.size.x;
        DestroyImmediate(tempBlock);
    }

    private void CreateBlocks()
    {
        //Calculate needly value
        var screenWidth = ScreenUtils.ScreenRight * 2;
        var blockWidthAndDistanceBetween = blockWidth * (1f + distanceBetweenBlocksInProcents);
        int blocksInRow = (int)(screenWidth / blockWidthAndDistanceBetween);
        var distanceFromEdgeLeft = (screenWidth - blocksInRow*blockWidthAndDistanceBetween)/2;
        var screenHeight = ScreenUtils.ScreenTop * 2;
        var distanceFromEdgeTop = screenHeight*distanceFromTopInProcents;
        var startPositionX = ScreenUtils.ScreenLeft + distanceFromEdgeLeft + (blockWidth / 2) + blockWidth * distanceBetweenBlocksInProcents;
        var startPositionY = ScreenUtils.ScreenTop - distanceFromEdgeTop - (blockHeight / 2);

        //Calculate static field "amountBlocksInStart"
        amountBlocksInStart = ConfigurationUtils.AmountRow * blocksInRow;

        //Set start spawm position 
        var currentPlaceSpawn = new Vector2(startPositionX,startPositionY);

        //Spawn {AmountRow} rows of blocks
        for (var indexRow = 0; indexRow < ConfigurationUtils.AmountRow; indexRow++)
        {
            for (var indexBlockInRow = 0; indexBlockInRow < blocksInRow; indexBlockInRow++)
            {
                Instantiate(BlockPrefab, currentPlaceSpawn, Quaternion.identity);
                currentPlaceSpawn.x += blockWidthAndDistanceBetween;
            }
            currentPlaceSpawn.x = startPositionX;
            currentPlaceSpawn.y -= blockHeight + distanceBetweenBlocksInProcents*blockHeight;
        }
    }
}
