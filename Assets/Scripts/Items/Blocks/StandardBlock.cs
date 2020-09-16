using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block
{
    protected override void Start()
    {
        //Set scorePoints
        scorePoints = ConfigurationUtils.StandartBlockPoints;
        base.Start();
    }

    protected override void SetColor()
    {
        Color RandomColor = Color.white;
        switch (Random.Range(0, 3))
        {
            case 0:
                RandomColor = Color.green;
                break;
            case 1: 
                RandomColor = Color.magenta;
                break;
            case 2:
                RandomColor = Color.white;
                break;
        }
        GetComponent<SpriteRenderer>().color = RandomColor;
    }
}
