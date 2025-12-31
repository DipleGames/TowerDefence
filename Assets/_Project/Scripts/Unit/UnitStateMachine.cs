using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class UnitStateMachine : MonoBehaviour, IChaseable
{
    public enum UnitState { Idle, Chase, Patrol }
    public UnitState unitState = UnitState.Patrol;

    public NavMeshAgent agent;
    public Transform point;

    Coroutine _chaseCoroutine;

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
        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
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
                _chaseCoroutine = StartCoroutine(ChaseRoutine());
                break;
        }
    }
    private float _chaseTick = 0f;
    public IEnumerator ChaseRoutine()
    {
        // 여기서는 transform 직접 이동하니까 agent는 멈춘 상태 유지
        while (true)
        {
            _chaseTick += Time.deltaTime;
            if(_chaseTick < 3f)
            {
                transform.position += new Vector3(0,0,1) * 1f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if(_chaseTick < 6f)
            {
                transform.position += new Vector3(-1,0,0) * 1f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            else if(_chaseTick < 9f)
            {
                transform.position += new Vector3(0,0,-1) * 1f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if(_chaseTick < 12f)
            {
                transform.position += new Vector3(1,0,0) * 1f * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (_chaseTick >= 12f)
            {
                _chaseTick = 0f;
            }
            yield return null;
        }
    }
}
