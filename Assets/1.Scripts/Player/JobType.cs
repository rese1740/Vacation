using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerJob
{
    None,   
    Medic,
    Archer,
    ChairMan,
    FireMan
    // 필요한 직업 추가
}

public class JobType : MonoBehaviour
{
    public PlayerJob job;
}
