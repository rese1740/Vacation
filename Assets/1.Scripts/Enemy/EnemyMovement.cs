using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveTime = 0.5f;
    [SerializeField] private float moveInterval = 1f;
    [SerializeField] private Transform player; // 플레이어 참조

    private bool isMoving = false;

    private void Start()
    {
        StartCoroutine(MoveRoutine());
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (!isMoving && player != null)
            {
                Vector3 dir = GetMoveDirectionToPlayer();
                Vector3 targetPos = transform.position + dir;

                yield return StartCoroutine(SmoothMove(targetPos));
            }

            yield return new WaitForSeconds(moveInterval);
        }
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

    private Vector3 GetMoveDirectionToPlayer()
    {
        Vector3 diff = player.position - transform.position;

        // 가장 먼 축을 먼저 따라간다 (단순 추적)
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            return new Vector3(Mathf.Sign(diff.x), 0, 0); // 좌우 우선
        }
        else if (Mathf.Abs(diff.y) > 0.01f)
        {
            return new Vector3(0, Mathf.Sign(diff.y), 0); // 상하
        }

        return Vector3.zero; // 같은 위치
    }
}
