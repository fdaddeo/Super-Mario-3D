using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int coinCount;

    // Start is called before the first frame update
    void Start()
    {
        coinCount = 0;
    }

    public void addCoin()
    {
        coinCount++;
    }
}
