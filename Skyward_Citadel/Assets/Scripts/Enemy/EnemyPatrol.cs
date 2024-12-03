using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header("Player Detection")]
    // [SerializeField] private Transform player;
    [SerializeField] private float detectionRange;
    [SerializeField] private float chaseSpeed;

    private GameObject player;
    private bool isChasing;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No Player Tag");
        }
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (player != null && PlayerInDetectionRange())
        {
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                // stop chasing and reset to patrol
                isChasing = false;
            }

            Patrol();
        }
    }

    private void Patrol()
    {
        
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private bool PlayerInDetectionRange()
    {
        return Vector2.Distance(enemy.position, player.transform.position) <= detectionRange;
    }

    private void ChasePlayer()
    {
        isChasing = true;
        anim.SetBool("moving", true);

        // determine direction to move towards player
        int direction = player.transform.position.x > enemy.position.x ? 1 : -1;

        // enemy face player
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction,
            initScale.y, initScale.z);

        // move towards the player
        enemy.position = Vector2.MoveTowards(enemy.position,
            new Vector2(player.transform.position.x, enemy.position.y),
            chaseSpeed * Time.deltaTime);
    }
}
