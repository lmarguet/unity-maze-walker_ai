using UnityEngine;

public class BrainBehaviour : MonoBehaviour
{
    private const string DeadTag = "dead";
    private const string WallTag = "wall";
    private const float ZSpeed = 0.0004f;

    public GameObject Eyes;
    public DNA Dna { get; private set; }

    private bool alive = true;
    private bool seeWall = true;
    private Vector3 startPosition;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(DeadTag))
        {
            alive = false;
        }
    }

    public BrainBehaviour Init()
    {
        // 0  forward 1 left 2 right
        Dna = new DNA(2, 360);
        alive = true;

        startPosition = transform.position;

        return this;
    }

    private void Update()
    {
        if (!alive) return;
        seeWall = false;

        RaycastHit hit;
        var isHittingWall = SphereCast(out hit);
        
        if (isHittingWall && hit.collider.gameObject.CompareTag(WallTag))
        {
            seeWall = true;
        }
    }

    private bool SphereCast(out RaycastHit hit)
    {
        return Physics.SphereCast
        (
            origin: Eyes.transform.position,
            radius: 0.1f,
            direction: Eyes.transform.forward,
            hitInfo: out hit,
            maxDistance: 0.5f
        );
    }


    private void FixedUpdate()
    {
        if (!alive) return;

        var turn = seeWall
                       ? Dna.GetGeneValue(1)
                       : 0;

        var move = Dna.GetGeneValue(0);
        var zMove = move * ZSpeed;
        transform.Translate(x: 0, y: 0, z: zMove);
        transform.Rotate(xAngle: 0, yAngle: turn, zAngle: 0);
    }

    public float DistanceTravelled
    {
        get { return !alive ? 0 : Vector3.Distance(startPosition, transform.position); }
    }
}