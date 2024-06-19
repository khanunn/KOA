using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class playerControl : MonoBehaviour
{
    [SerializeField]protected NavMeshAgent p_Agent;
    [SerializeField]private float groundOffset = 0.1f;
    ActorAction p_actor;
    ActorAction target_Actor;
    bool isAutoCombat = false;
    void Awake()
    {
        p_Agent = GetComponent<NavMeshAgent>();
        p_Agent.updatePosition = true;
        p_Agent.updateRotation = true;
        p_actor = gameObject.GetComponent<ActorAction>();
    }
    void OnCollisionStay(Collision coll){
        if(p_actor.o_target != null)
        {
            if (coll.gameObject.name == p_actor.o_target.name)
            {
                coll.gameObject.transform.LookAt(gameObject.transform);
                p_actor.o_target = null;
                p_Agent.SetDestination(gameObject.transform.position);
            }
        }
    }
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.F)){
            if(!isAutoCombat){
                isAutoCombat = true;
            }else{
                isAutoCombat = false;
            }
            
        }*/
        if (p_actor.o_target != null)
        {
            p_Agent.SetDestination(p_actor.o_target.transform.position);
            if(p_Agent.remainingDistance <= 1){
                p_actor.o_target = null;
                Debug.Log(target_Actor.o_type);
            }
        }
        if(isAutoCombat){Debug.Log("Auto is on");}else{Debug.Log("Auto is off");}
    }

    public void Move(InputAction.CallbackContext context){
        if(context.started){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) == true)
            {
                if(hit.transform.CompareTag("Interactable")){
                    p_actor.o_target = hit.collider.gameObject;
                    p_Agent.SetDestination(p_actor.o_target.transform.position);
                    target_Actor = p_actor.o_target.GetComponent<ActorAction>();
                }else{
                     GameObject hitObject = new GameObject();
                    hitObject.transform.position = hit.point + hit.normal * groundOffset;
                     p_actor.o_target = null;

                p_Agent.SetDestination(hitObject.transform.position);
                Destroy(hitObject, 0.2f);
                }
               
            }
        }
    }
}
