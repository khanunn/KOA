using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonDamage : MonoBehaviour
{
    [SerializeField] int Damage;    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {            
            other.GetComponent<Interactable>().myActor.TakeDamage(Damage);
            other.GetComponent<Interactable>().myActor.DamageOnHealthBar();
            Vector3 position = other.transform.position;
            EventManager.instance.playerEvents.AttackPopUp(position, Damage.ToString(), Color.red);
        }        
    }

}
