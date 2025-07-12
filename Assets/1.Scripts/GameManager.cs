using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public PlayerSO playerSO;

    private void Start()
    {
        playerSO.Init();
    }
}
