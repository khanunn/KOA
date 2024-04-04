using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    /*[SerializeField] PlayerController controller;
    [SerializeField] Vector3 TargetPos;
    bool IsStart = false;

    [Header("Projectile Setting")]

    [SerializeField] float Speed = 1;
    [SerializeField] int DelayTime = 0;

    // Start is called before the first frame update
    async void Start()
    {
        this.transform.SetParent(null);
        GameObject PlayerTemp = GameObject.FindGameObjectWithTag("Player").gameObject; 
        controller = PlayerTemp.GetComponent<PlayerController>();
        TargetPos = controller.mousePositionInScene;
        await Task.Delay(DelayTime);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        IsStart = true;
       // Debug.Log("Test");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStart)
        {
           // Debug.Log("Test Projectile");
            // Move our position a step closer to the target.
            //Speed = Speed * Time.deltaTime; // calculate distance to move            
           transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed);
        }

        if (Vector3.Distance(transform.position,TargetPos) <= 0.1f) Destroy(gameObject);

    }    */   
   
    public int Damage;
    [SerializeField] float LifeTime = 10.0f;
    [SerializeField] float Speed = 5.0f;
    [SerializeField] StatusInfoSO OnHitEffect;
    [SerializeField] PlayerController controller;

    //Transform CurrentTargetPosition;
    Vector3 CurrentTargetPosition;
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Check 0 ===========================");
        if (other.tag == "Interactable")
        {
            Debug.Log("Check 1 ===========================");
            if (other.GetComponent<Interactable>().interactionType == InteractableType.ENEMY)
            {
                Debug.Log("Check 2 ===========================");
                int TotalDamage = Damage - other.GetComponent<Interactable>().myPatrol.PhysicalDefend;
                other.GetComponent<Interactable>().myActor.TakeDamage(TotalDamage);
                EventManager.instance.playerEvents.AttackPopUp(other.transform.position, TotalDamage.ToString(), Color.green);

                if (OnHitEffect != null) other.GetComponent<StatusManager>().AddStatus(OnHitEffect);
            }
            
            //Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroySelf), LifeTime);        

        this.transform.SetParent(null);
        GameObject PlayerTemp = GameObject.FindGameObjectWithTag("Player").gameObject;
        controller = PlayerTemp.GetComponent<PlayerController>();

        CurrentTargetPosition = controller.mousePositionInScene;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * 1000 * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, CurrentTargetPosition, Speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, CurrentTargetPosition) <= 1) Invoke(nameof(DestroySelf), 0.1f);

    }
}
