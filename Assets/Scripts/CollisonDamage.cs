using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollisonDamage : MonoBehaviour
{
    [SerializeField] int Damage;
    [SerializeField] StatusInfoSO StatusOnDamage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {            
            other.GetComponent<Interactable>().myActor.TakeDamage(Damage);
            other.GetComponent<Interactable>().myActor.DamageOnHealthBar();
            Vector3 position = other.transform.position;
            EventManager.instance.playerEvents.AttackPopUp(position, Damage.ToString(), Color.red);

            if(StatusOnDamage != null) other.GetComponent<StatusManager>().AddStatus(StatusOnDamage);

            //Debug.Log("Player MaxHealth: " + other.GetComponent<Interactable>().myActor.MaxHealth);
            //Debug.Log("Player CurrentHealth: " + other.GetComponent<Interactable>().myActor.CurrentHealth);
        }        
    }
    
}
