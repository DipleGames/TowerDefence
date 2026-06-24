using UnityEngine;
using UnityEngine.AI;

public class MonsterStateMachine : MonoBehaviour
{
    public enum MonsterState { Idle }
    [SerializeField] private MonsterState _monsterState = MonsterState.Idle;
    public NavMeshAgent agent;

    private int _currentWayPointIndex = 1;   // 시작 웨이포인트

    void Awake()
    {
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

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
        if (agent.pathPending) return;

        // 도착 판정
        if (agent.remainingDistance <= agent.stoppingDistance + 0.5f)
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
        agent.SetDestination(WayPointManager.Instance.wayPoints[_currentWayPointIndex].transform.position);
    }

    void MoveToNextWayPoint()
    {
        _currentWayPointIndex++;

        //순환 (0 ~ Count-1)
        if (_currentWayPointIndex >= WayPointManager.Instance.wayPoints.Count)
        {
            _currentWayPointIndex = 0;
        }

        MoveToCurrentWayPoint();
    }
}
