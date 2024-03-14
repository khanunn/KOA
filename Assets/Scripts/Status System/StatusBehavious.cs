using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.AddressableAssets.GUI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class StatusBehavious : MonoBehaviour
{       
    public GameObject target;
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
                    target.GetComponent<Interactable>().myActor.DamageOnHealthBar();
                    EventManager.instance.playerEvents.AttackPopUp(target.transform.position, statusIntensity.ToString(), Color.blue);
                    Debug.Log("CheckTime: " + CheckTime);
                    await Task.Delay(1000);
                    CheckTime -= 1;
                }
                break;
            case 6:
                //Slient                 
                if(target.GetComponent<Interactable>().interactionType == InteractableType.PLAYER)
                {
                    target.GetComponent<PlayerSkill>().CanUseSkill = false;
                    Debug.Log("target.GetComponent<PlayerSkill>().isSkillPlaying: " + target.GetComponent<PlayerSkill>().CanUseSkill);                    
                    
                    await Task.Delay(milliseconds);

                    Debug.Log("target.GetComponent<PlayerSkill>().isSkillPlaying: " + target.GetComponent<PlayerSkill>().CanUseSkill);
                    target.GetComponent<PlayerSkill>().CanUseSkill = true;
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
