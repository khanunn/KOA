using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollisonDamage : MonoBehaviour
{
    [SerializeField] int Damage;
    [SerializeField] bool isSlowEffect = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {            
            other.GetComponent<Interactable>().myActor.TakeDamage(Damage);
            other.GetComponent<Interactable>().myActor.DamageOnHealthBar();
            Vector3 position = other.transform.position;
            EventManager.instance.playerEvents.AttackPopUp(position, Damage.ToString(), Color.red);

            Debug.Log("Player MaxHealth: " + other.GetComponent<Interactable>().myActor.MaxHealth);
            Debug.Log("Player CurrentHealth: " + other.GetComponent<Interactable>().myActor.CurrentHealth);
        }        
    }

    private async void SLowDebuff(Collider player, int Duration)
    {
        Animator animator = player.GetComponent<Animator>();
        float defaultAnimatorSpeed = animator.speed;
        animator.speed = 2;        
        player.GetComponent<PlayerController>().MoveSpeed(7f);
        /* Animation animation = animator.GetComponent<Animation>();
        Debug.Log("Animation: " + animation);
        animation["Walk"].speed = 1.5f; */
        await Task.Delay(10000);

        animator.speed = defaultAnimatorSpeed;
        player.GetComponent<PlayerController>().MoveSpeed(3.5f);
    }
}
