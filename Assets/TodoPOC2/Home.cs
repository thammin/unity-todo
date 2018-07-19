using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using POC;

public class Home : POCComponent
{
    public void Jump()
    {
        POCRouter.To("Gacha");
    }
}
