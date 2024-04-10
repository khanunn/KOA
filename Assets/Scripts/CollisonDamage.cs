using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollisonDamage : MonoBehaviour
{
    [Header("Enemy Collison Damage")]
    [SerializeField] int Damage;
    [SerializeField] StatusInfoSO StatusOnDamage;
    [Header("Player Collison Damage")]
    [SerializeField] bool IsTargetPlayer = true;
    [SerializeField] PlayerSkill playerSkill;
    bool IsAlreadyDamage = false;    

    private void Start()
    {
        GameObject PlayerTemp = GameObject.FindGameObjectWithTag("Player").gameObject;

        playerSkill = PlayerTemp.GetComponent<PlayerSkill>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!IsTargetPlayer) return;

            other.GetComponent<Interactable>().myActor.TakeDamage(Damage);
            other.GetComponent<Interactable>().myActor.DamageOnHealthBar();
            Vector3 position = other.transform.position;
            EventManager.instance.playerEvents.AttackPopUp(position, Damage.ToString(), Color.red);

            if(StatusOnDamage != null) other.GetComponent<StatusManager>().AddStatus(StatusOnDamage);

            //Debug.Log("Player MaxHealth: " + other.GetComponent<Interactable>().myActor.MaxHealth);
            //Debug.Log("Player CurrentHealth: " + other.GetComponent<Interactable>().myActor.CurrentHealth);
        }   
        
        if(other.tag == "Interactable")
        {
            if (IsTargetPlayer) return;
            if (other.GetComponent<Interactable>().interactionType != InteractableType.ENEMY) return;
            //if (IsAlreadyDamage) return;            

            playerSkill = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerSkill>();
            Debug.LogWarning("Test playerSkill: " + playerSkill);            

            //Damage = playerSkill.statController.v_patk.statValue * 2; 
            Damage = playerSkill.SkillData[playerSkill.Skill_ButtonID].BaseSkillValue + (playerSkill.SkillData[playerSkill.Skill_ButtonID].SkillLevel * 2) + playerSkill.statController.v_patk.statValue * 2;
            Debug.LogWarning("Test Collison Damage: " + Damage);
            Damage = Damage - other.GetComponent<Interactable>().myPatrol.MagicDefend;
            Debug.LogWarning("Test Collison Damage: " + Damage);
            other.GetComponent<Interactable>().myActor.TakeDamage(10);            
            Vector3 position = other.transform.position;
            EventManager.instance.playerEvents.AttackPopUp(position, Damage.ToString(), Color.green);

            if (StatusOnDamage != null) other.GetComponent<StatusManager>().AddStatus(StatusOnDamage);

            //IsAlreadyDamage = true;

        }
    }
    
}
