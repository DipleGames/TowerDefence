using UnityEngine;
using UnityEngine.AI;

public class MonsterStateMachine : MonoBehaviour
{
    public enum MonsterState { Idle }
    [SerializeField] private MonsterState _monsterState = MonsterState.Idle;
    [SerializeField] private NavMeshAgent _agent;

    private int _currentWayPointIndex = 1;   // 시작 웨이포인트

    void Start()
    {
        MoveToCurrentWayPoint();
    }

    void Update()
    {
        switch (_monsterState)
        {
            case MonsterState.Idle:
                UpdateIdleState();
                break;
        }
    }

    void UpdateIdleState()
    {
        // 아직 경로 계산 중이면 패스
        if (_agent.pathPending) return;

        // 도착 판정
        if (_agent.remainingDistance <= _agent.stoppingDistance + 0.5f)
        {
            MoveToNextWayPoint();
            // 실제로 멈췄는지 한 번 더 체크
            // if (!_agent.hasPath || _agent.velocity.sqrMagnitude < 0.01f)
            // {
            //     MoveToNextWayPoint();
            // }
        }
    }

    void MoveToCurrentWayPoint()
    {
        _agent.SetDestination(WayPointManager.wayPoints[_currentWayPointIndex].transform.position);
    }

    void MoveToNextWayPoint()
    {
        _currentWayPointIndex++;

        //순환 (0 ~ Count-1)
        if (_currentWayPointIndex >= WayPointManager.wayPoints.Count)
        {
            _currentWayPointIndex = 0;
        }

        MoveToCurrentWayPoint();
    }
}
