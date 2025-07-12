using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]  private float	moveTime = 0.5f;								
	public	Vector3	MoveDirection	{ set; get; } = Vector3.zero;   // 이동 방향
   
    public	bool IsMove	{ set; get; } = false;          

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown = 2f;
    [SerializeField] private Image fireButtonImg;
    private float lastFireTime = -Mathf.Infinity;
    public Vector3 LookDirection { get; private set; } = Vector3.down;

    [Header("Debug")]
    [SerializeField] private PlayerSO playerSO;

    private void Update()
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
            FireProjectile();
            lastFireTime = Time.time;
        }

        float cooldownElapsed = Time.time - lastFireTime;
        fireButtonImg.fillAmount = Mathf.Clamp01(cooldownElapsed / fireCooldown);
        #endregion
    }

    public void FireProjectile()
    {
        if (LookDirection == Vector3.zero)
            return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Launch(LookDirection);
    }

    private IEnumerator Start()
	{
		while ( true )
		{
			if ( MoveDirection != Vector3.zero && IsMove == false )
			{
				Vector3 end = transform.position + MoveDirection;

				yield return StartCoroutine(GridSmoothMovement(end));
			}

			yield return null;
		}
	}

	private IEnumerator GridSmoothMovement(Vector3 end)
	{
		Vector3 start = transform.position;
		float	current = 0;
		float	percent = 0;

		IsMove = true;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / moveTime;

			transform.position = Vector3.Lerp(start, end, percent);

			yield return null;
		}

		IsMove = false;
	}
}

