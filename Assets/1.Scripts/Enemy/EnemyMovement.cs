using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Patrol,
    Chase
}

public class EnemyMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float moveTime = 0.5f;
    [SerializeField] private float moveInterval = 1f;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private Transform player;

    private bool isMoving = false;
    private int currentWaypointIndex = 0;
    private EnemyState currentState = EnemyState.Patrol;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            // 상태 전이 판단
            float distToPlayer = Vector3.Distance(transform.position, player.position);
            if (distToPlayer < chaseRange)
            {
                currentState = EnemyState.Chase;
            }
            else
            {
                currentState = EnemyState.Patrol;
            }

            // 상태별 이동
            if (!isMoving)
            {
                Vector3 targetDir = Vector3.zero;

                switch (currentState)
                {
                    case EnemyState.Patrol:
                        targetDir = GetPatrolDirection();
                        break;
                    case EnemyState.Chase:
                        targetDir = GetMoveDirectionToPlayer();
                        break;
                }

                if (targetDir != Vector3.zero)
                {
                    Vector3 targetPos = transform.position + targetDir;
                    yield return StartCoroutine(SmoothMove(targetPos));
                }
            }

            yield return new WaitForSeconds(moveInterval);
        }
    }

    private Vector3 GetPatrolDirection()
    {
        if (Waypoint.Points.Length == 0) return Vector3.zero;

        Transform target = Waypoint.Points[currentWaypointIndex];
        Vector3 dir = target.position - transform.position;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= Waypoint.Points.Length)
                currentWaypointIndex = 0;
        }

        Vector3 moveDir = (target.position - transform.position).normalized;
        return new Vector3(Mathf.Round(moveDir.x), Mathf.Round(moveDir.y), 0);
    }

    private Vector3 GetMoveDirectionToPlayer()
    {
        Vector3 diff = player.position - transform.position;

        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            return new Vector3(Mathf.Sign(diff.x), 0, 0);
        else if (Mathf.Abs(diff.y) > 0.01f)
            return new Vector3(0, Mathf.Sign(diff.y), 0);

        return Vector3.zero;
    }

    private IEnumerator SmoothMove(Vector3 end)
    {
        isMoving = true;
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsed / moveTime);
            transform.position = Vector3.Lerp(start, end, percent);
            yield return null;
        }

        isMoving = false;
    }
}
