using UnityEngine;

public class BrainBehaviour : MonoBehaviour
{
    public GameObject eyes;
    public DNA Dna { get; private set; }

    private bool alive = true;
    private bool seeWall = true;
    private Vector3 startPosition;
    

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "dead"){
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

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;

        seeWall = false;

        RaycastHit hit;
        if (Physics.SphereCast(eyes.transform.position, 0.1f, eyes.transform.forward, out hit, 0.5f)){
            if (hit.collider.gameObject.tag == "wall"){
                seeWall = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!alive) return;
        
        float turn = seeWall ? Dna.GetGeneValue(1) : 0;
        float move = Dna.GetGeneValue(0);
        
        transform.Translate(0, 0, move * 0.0004f);
        transform.Rotate(0, turn, 0);
    }

    public float DistanceTravelled
    {
        get
        {
            return !alive ? 0 : Vector3.Distance(startPosition, transform.position);
        }
    }


}