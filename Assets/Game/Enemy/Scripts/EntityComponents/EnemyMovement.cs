using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour, IEntityMovement, IEntityPathfinder
{
    [InspectorName("Speed")]
    [SerializeField] private float _speed;
    public float speed => _speed;

    [InspectorName("Acceleration Curve")]
    [SerializeField] private AnimationCurve _accelerationCurve;
    public AnimationCurve accelerationCurve => _accelerationCurve;

    [InspectorName("Deceleration Curve")]
    [SerializeField] private AnimationCurve _decelerationCurve;
    public AnimationCurve decelerationCurve => _decelerationCurve;

    public bool isMoving { get; private set; }

    public Vector2 velocity { get => rig.velocity; set => rig.velocity = value; }
    public Vector2 direction => pathfinderManager.GetDirection();

    public float currentTime => Time.time;

    public Seeker seeker { get; private set; }
    private Transform target;

    public Vector2 position => transform.position;
    public Vector2 targetPosition => target.position;

    private Rigidbody2D rig;
    private EntityPathfinderManager pathfinderManager;
    private EntityMovementManager movementManager;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
        seeker = GetComponent<Seeker>();

        pathfinderManager = new EntityPathfinderManager(this);
        movementManager = new EntityMovementManager(this);
    }

    private void FixedUpdate()
    {
        pathfinderManager.Update();
        movementManager.Update();
    }

    public void OnStartMovement()
    {
        isMoving = true;
    }
    public void OnStopMovement()
    {
        isMoving = false;
        if (pathfinderManager.reachedTarget) GetComponent<EnemyAttack>().Attack();
    }
}