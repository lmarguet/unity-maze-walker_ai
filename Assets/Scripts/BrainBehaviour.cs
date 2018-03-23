using UnityEngine;

public class BrainBehaviour : MonoBehaviour
{
    public float TimeAlive;
    public DNA Dna;
    public GameObject eyes;
    public float TimeWalking;
    public GameObject EthanPrefab;
    
    
    private bool alive = true;
    private bool seeGround = true;
    private int dnaLength = 2;
    private GameObject ethanInstance;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "dead"){
            alive = false;
            
            TimeAlive = 0;
            TimeWalking = 0;
            
            Destroy(ethanInstance);
        }
    }

    void OnDestroy()
    {
        Destroy(ethanInstance);
    }

    public BrainBehaviour Init()
    {
        // 0  forward 1 left 2 right
        Dna = new DNA(dnaLength, 3);
        TimeAlive = 0;
        alive = true;
        
        return this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        
        seeGround = false;

        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit)){
            if (hit.collider.gameObject.tag == "platform"){
                seeGround = true;
            }
        }

        float turn = 0;
        float move = 0;
        
        TimeAlive = PopulationManager.ElapsedTime;
        
        var gene = seeGround ? Dna.GetGeneValue(0) : Dna.GetGeneValue(1);
        
        if (gene == 0){
            move = 1;
            TimeWalking++;
        }
        else if (gene == 1){ turn = -90; }
        else if (gene == 2){ turn = 90; }
        
        transform.Translate(0, 0, move * 0.1f);
        transform.Rotate(0, turn, 0); 
    }
}