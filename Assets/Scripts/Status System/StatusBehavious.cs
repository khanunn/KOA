using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class StatusBehavious : MonoBehaviour
{       
    public GameObject target;

    //First Effect
    public float duration;
    public int statusID;    
    public float statusIntensity; 

    public float previousSpeed;//store previous speed

    public async Task ActiveSkill()
    {
        int milliseconds = Mathf.RoundToInt(duration * 1000); // as the effect of duration              
        switch (statusID)
        {
            case 0:
                //Movement Status like: Slow, Stun , Root , Speed              
                previousSpeed = target.GetComponent<NavMeshAgent>().speed; //store previous speed

                target.GetComponent<NavMeshAgent>().speed = statusIntensity; //set speed as effect intensity
                
                await Task.Delay(milliseconds);

                target.GetComponent<NavMeshAgent>().speed = previousSpeed; // back to previous speed

                break;
            case 1:
                //Damage over Time (Specific Damage)
                //float CheckTime = duration;             
                for (int i = (int)duration; duration > 0; i--)
                {
                    target.GetComponent<Actor>().TakeDamage((int)statusIntensity);
                    target.GetComponent<Interactable>().myActor.DamageOnHealthBar();
                    EventManager.instance.playerEvents.AttackPopUp(target.transform.position, statusIntensity.ToString(), Color.red);
                    //Debug.Log("CheckTime: " + CheckTime);
                    await Task.Delay(1000);                    
                    //CheckTime -= 1;
                }
                break;
            case 2:
                //Damage over Time (Damage from MaxHP)
                //float CheckTime = duration;             

                if (target.GetComponent<Interactable>().interactionType == InteractableType.PLAYER)
                {
                    for (int i = (int)duration; duration > 0; i--)
                    {
                        float totalDamage = target.GetComponent<Actor>().MaxHealth * (statusIntensity / 100);
                        target.GetComponent<Actor>().TakeDamage((int)totalDamage);
                        target.GetComponent<Interactable>().myActor.DamageOnHealthBar();
                        EventManager.instance.playerEvents.AttackPopUp(target.transform.position, totalDamage.ToString(), Color.red);
                        //Debug.Log("CheckTime: " + CheckTime);
                        await Task.Delay(1000);
                        //CheckTime -= 1;
                    }
                }

                if (target.GetComponent<Interactable>().interactionType == InteractableType.ENEMY)
                {
                    for (int i = (int)duration; duration > 0; i--)
                    {
                        float totalDamage = target.GetComponent<Actor>().MaxHealth * (statusIntensity / 100);
                        target.GetComponent<Actor>().TakeDamage((int)totalDamage);                        
                        EventManager.instance.playerEvents.AttackPopUp(target.transform.position, totalDamage.ToString(), Color.green);
                        await Task.Delay(1000);
                        //CheckTime -= 1;
                    }
                }

                 
                break;
            case 3:
                //Damage over Time (Damage from CurrentHP)
                //float CheckTime = duration;             
                for (int i = (int)duration; duration > 0; i--)
                {
                    float totalDamage = target.GetComponent<Actor>().CurrentHealth * (statusIntensity / 100);                    
                    target.GetComponent<Actor>().TakeDamage((int)totalDamage);
                    target.GetComponent<Interactable>().myActor.DamageOnHealthBar();
                    EventManager.instance.playerEvents.AttackPopUp(target.transform.position, totalDamage.ToString(), Color.red);
                    //Debug.Log("CheckTime: " + CheckTime);
                    await Task.Delay(1000);
                    //CheckTime -= 1;
                }
                break;
            case 4:
                //Damage over Time (Damage from LoseHP)
                //float CheckTime = duration;             
                for (int i = (int)duration; duration > 0; i--)
                {
                    float totalDamage = (target.GetComponent<Actor>().MaxHealth - target.GetComponent<Actor>().CurrentHealth) * (statusIntensity / 100);                    
                    target.GetComponent<Actor>().TakeDamage((int)totalDamage);
                    target.GetComponent<Interactable>().myActor.DamageOnHealthBar();
                    EventManager.instance.playerEvents.AttackPopUp(target.transform.position, totalDamage.ToString(), Color.red);
                    //Debug.Log("CheckTime: " + CheckTime);
                    await Task.Delay(1000);
                    //CheckTime -= 1;
                }
                break;
            case 5:
                //Healing over Time like: Regeneration
                float CheckTime = duration;
                for (int i = (int)duration; duration > 0; i--)
                {
                    target.GetComponent<Actor>().UpHealth((int)statusIntensity);
                    if (target.GetComponent<Interactable>().interactionType == InteractableType.PLAYER) target.GetComponent<Interactable>().myActor.DamageOnHealthBar();
                    EventManager.instance.playerEvents.AttackPopUp(target.transform.position, statusIntensity.ToString(), Color.blue);
                    Debug.Log("CheckTime: " + CheckTime);
                    await Task.Delay(1000);
                    CheckTime -= 1;
                }
                break;
            case 6:
                //Slient                 
                //Debug.Log("target.GetComponent<PlayerSkill>().isSkillPlaying: " + target.GetComponent<PlayerSkill>().CanUseSkill);
                if (target.GetComponent<Interactable>().interactionType == InteractableType.PLAYER)
                {
                    target.GetComponent<PlayerSkill>().CanUseSkill = false;
                    Debug.Log("target.GetComponent<PlayerSkill>().isSkillPlaying: " + target.GetComponent<PlayerSkill>().CanUseSkill);                    
                    
                    await Task.Delay(milliseconds);

                    Debug.Log("target.GetComponent<PlayerSkill>().isSkillPlaying: " + target.GetComponent<PlayerSkill>().CanUseSkill);
                    target.GetComponent<PlayerSkill>().CanUseSkill = true;
                }
                break;
            case 7:
                // Add Defenese or Armor break
                if (target.GetComponent<Interactable>().interactionType == InteractableType.PLAYER)
                {
                    StatController PlayerStat = target.GetComponent<PlayerController>().statController;
                    PlayerStat.EditPhysicalDefend((int)statusIntensity);
                    await Task.Delay((int)duration * 1000);                    
                    PlayerStat.EditPhysicalDefend(-(int)statusIntensity);
                }

                if(target.GetComponent<Interactable>().interactionType == InteractableType.ENEMY)
                {                   
                    int TargetDef = target.GetComponent<PatrolController>().PhysicalDefend;
                    //Debug.Log(TargetDef);
                    TargetDef += (int)statusIntensity;
                    //Debug.Log(TargetDef);                    
                    await Task.Delay((int)duration * 1000);
                    TargetDef += -(int)statusIntensity;
                    //Debug.Log(TargetDef);
                }
                break;
            case 8:
                // Add Damage or Weakness
                if (target.GetComponent<Interactable>().interactionType == InteractableType.PLAYER)
                {
                    StatController PlayerStat = target.GetComponent<PlayerController>().statController;
                    PlayerStat.EditPhysicalDamage((int)statusIntensity);
                    await Task.Delay((int)duration * 1000);
                    PlayerStat.EditPhysicalDamage(-(int)statusIntensity);
                }

                if (target.GetComponent<Interactable>().interactionType == InteractableType.ENEMY)
                {
                    int TargetDef = target.GetComponent<PatrolController>().PhysicalDamage;
                    //Debug.Log(TargetDef);
                    TargetDef += (int)statusIntensity;
                    //Debug.Log(TargetDef);                    
                    await Task.Delay((int)duration * 1000);
                    TargetDef += -(int)statusIntensity;
                    //Debug.Log(TargetDef);
                }
                break;

            case 9:
                // Furious ( ID 8 + 6 )
                if (target.GetComponent<Interactable>().interactionType == InteractableType.PLAYER)
                {
                    StatController PlayerStat = target.GetComponent<PlayerController>().statController;
                    PlayerStat.EditPhysicalDamage((int)statusIntensity);
                    target.GetComponent<PlayerSkill>().CanUseSkill = false;
                    await Task.Delay((int)duration * 1000);
                    target.GetComponent<PlayerSkill>().CanUseSkill = true;
                    PlayerStat.EditPhysicalDamage(-(int)statusIntensity);                                
                }

                if (target.GetComponent<Interactable>().interactionType == InteractableType.ENEMY)
                {
                    int TargetDef = target.GetComponent<PatrolController>().PhysicalDamage;
                    //Debug.Log(TargetDef);
                    TargetDef += (int)statusIntensity;
                    //Debug.Log(TargetDef);                    
                    await Task.Delay((int)duration * 1000);
                    TargetDef += -(int)statusIntensity;
                    //Debug.Log(TargetDef);
                }
                break;
        }     
    }
    public void CancelStatus()
    {
        switch (statusID)
        {
            case 0: target.GetComponent<NavMeshAgent>().speed = previousSpeed; // back to previous speed
                break;
        }        
    }

    private void Start()
    {
        previousSpeed = target.GetComponent<NavMeshAgent>().speed;
        //Debug.LogWarning(previousSpeed);
    }


}
