using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitController : MonoBehaviour, IChaseable
{
    public enum UnitState { Idle, Chase, Patrol }
    public UnitState unitState = UnitState.Patrol;

    public NavMeshAgent agent;
    public Transform point;

    Coroutine _chaseCo;

    void Awake()
    {
        agent.updateRotation = false;
    }

    void Start()
    {
        ApplyState(unitState);
    }

    void ApplyState(UnitState state)
    {
        // 코루틴 정리
        if (_chaseCo != null)
        {
            StopCoroutine(_chaseCo);
            _chaseCo = null;
        }

        unitState = state;

        switch (unitState)
        {
            case UnitState.Idle:
                agent.isStopped = true;
                break;

            case UnitState.Patrol:
                agent.isStopped = false;
                agent.updateRotation = true;
                agent.SetDestination(point.position);
                break;

            case UnitState.Chase:
                // NavMesh 쓸 거면 transform 직접 이동 대신 SetDestination으로 구현 권장
                agent.isStopped = true; 
                _chaseCo = StartCoroutine(ChaseRoutine());
                break;
        }
    }
    private float _tick = 0f;
    public IEnumerator ChaseRoutine()
    {
        // 여기서는 transform 직접 이동하니까 agent는 멈춘 상태 유지
        while (true)
        {
            _tick += Time.deltaTime;
            if(_tick < 3f)
            {
                transform.position += new Vector3(0,0,1) * 1f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if(_tick < 6f)
            {
                transform.position += new Vector3(-1,0,0) * 1f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            else if(_tick < 9f)
            {
                transform.position += new Vector3(0,0,-1) * 1f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if(_tick < 12f)
            {
                transform.position += new Vector3(1,0,0) * 1f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (_tick >= 12f)
            {
                _tick = 0f;
            }
            yield return null;
        }
    }
}
