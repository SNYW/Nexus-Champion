using SystemEvents;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementSystem : MonoBehaviour
{
    private Camera _mainCamera;
    private NavMeshAgent _agent;
    private Animator _animator;
    private static readonly int HasMoveTarget = Animator.StringToHash("HasMoveTarget");

    private void OnEnable()
    {
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }

    private void OnSpellCast(object obj)
    {
        _agent.isStopped = true;
    }

    void Start()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            // Cast a ray from the mouse position
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits something
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit point is on the NavMesh
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(hit.point, out navMeshHit, 1.0f, NavMesh.AllAreas))
                {
                    _agent.isStopped = false;
                    // Set the agent's destination to the hit point on the NavMesh
                    _agent.SetDestination(navMeshHit.position);
                }
            }
        }
        
        _animator.SetBool(HasMoveTarget, _agent.hasPath && !_agent.isStopped);
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }
}
