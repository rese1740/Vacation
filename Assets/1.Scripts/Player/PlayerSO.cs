using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerSO", menuName = "Player/PlayerSO", order = 1)]
public class PlayerSO : ScriptableObject
{
   public static PlayerSO Instance;

    public PlayerJob playerJob = PlayerJob.None;

    public SpriteRenderer lastSelectedRenderer;
    public void Init()
    {
        Instance = this;
    }
}
