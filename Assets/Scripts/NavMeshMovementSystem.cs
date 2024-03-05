using SystemEvents;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementSystem : MonoBehaviour
{
    public LayerMask rayLayer;
    private Camera _mainCamera;
    private NavMeshAgent _agent;
    private Animator _animator;
    public bool canMove;
    private static readonly int HasMoveTarget = Animator.StringToHash("HasMoveTarget");

    private void OnEnable()
    {
        canMove = true;
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
        if (Input.GetMouseButtonDown(1) && canMove)
        {
            if (MouseManager.GetMousePositionOnNavmesh(rayLayer, transform.position, out var pos))
            {
                _agent.isStopped = false;
                _agent.SetDestination(pos);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.S) && _agent.hasPath)
            _agent.isStopped = true;
        _animator.SetBool(HasMoveTarget, _agent.hasPath && !_agent.isStopped);
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellCast);
    }
}
