using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown = 2f;
    [SerializeField] private Image fireButtonImg;
    private float lastFireTime = -Mathf.Infinity;

    public Vector3 LookDirection { get; private set; } = Vector3.down;
    public Vector3 MoveDirection { set; get; } = Vector3.zero;   // 이동 방향

    void Start()
    {
        
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0)
        {
            LookDirection = MoveDirection; // 방향 갱신
            MoveDirection = new Vector3(x, 0, 0);
        }
        else if (y != 0)
        {
            LookDirection = MoveDirection; // 방향 갱신
            MoveDirection = new Vector3(0, y, 0);
        }
        else
            MoveDirection = Vector3.zero;
        #region Projectile
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastFireTime + fireCooldown)
        {
            switch(PlayerSO.Instance.playerJob)
            {
                case PlayerJob.Archer:
                    FireArrow();
                    break;
                case PlayerJob.Medic:
                    // Medic specific action
                    break;
                case PlayerJob.ChairMan:
                    // ChairMan specific action
                    break;
                case PlayerJob.FireMan:
                    // FireMan specific action
                    break;
            }
            lastFireTime = Time.time;
        }

        float cooldownElapsed = Time.time - lastFireTime;
        fireButtonImg.fillAmount = Mathf.Clamp01(cooldownElapsed / fireCooldown);
        #endregion
    }

    public void FireArrow()
    {
        if (LookDirection == Vector3.zero)
            return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Launch(LookDirection);
    }

    public void FireHeal()
    {
        if (LookDirection == Vector3.zero)
            return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Launch(LookDirection);
    }

    public void SmashChair()
    {
        if (LookDirection == Vector3.zero)
            return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Launch(LookDirection);
    }

    public void SpreadAsh()
    {
        if (LookDirection == Vector3.zero)
            return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Launch(LookDirection);
    }
}
