using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public Interactable Target;
    public bool IsFire = false;
    public int Damage;    
    [SerializeField] float LifeTime = 10.0f;
    [SerializeField] float Speed = 5.0f;
    NavMeshAgent agent;
    //Transform CurrentTargetPosition;
    Vector3 CurrentTargetPosition;
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {      
        if (other.tag == "Player")
        {
            other.GetComponent<Interactable>().myActor.TakeDamage(Damage);            
            EventManager.instance.playerEvents.AttackPopUp(other.transform.position, Damage.ToString(), Color.red);
            other.GetComponent<Interactable>().myActor.DamageOnHealthBar();
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroySelf), LifeTime);
        agent = this.GetComponent<NavMeshAgent>();
        CurrentTargetPosition = Target.transform.position;
        
        //agent.SetDestination(Target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * 1000 * Time.deltaTime);              

        transform.position = Vector3.MoveTowards(transform.position, CurrentTargetPosition, Speed * Time.deltaTime);        

    }
}
