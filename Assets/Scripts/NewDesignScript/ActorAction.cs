using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
public enum ActorType
{
    PLAYER,
    ENEMY,
    ITEM_QUEST,
    NPC,
    ITEM_INVENTORY,
    SHOP
}


public class ActorAction : MonoBehaviour , Damageble
{
    CapsuleCollider o_capsule;
    playerControl p_playercontroller;
    public GameObject o_target;
    public ActorType o_type;
    private ActorAction targetType;
    [Header("Checking")]
    bool isAggo;
        void Awake(){
        switch(o_type){
            case ActorType.PLAYER:
                o_capsule = GetComponent<CapsuleCollider>();
                p_playercontroller = GetComponent<playerControl>();
                break;
            case ActorType.ENEMY:
                o_capsule = GetComponent<CapsuleCollider>();
                
                break;
            case ActorType.NPC:
                o_capsule = GetComponent<CapsuleCollider>();
                break;
            case ActorType.ITEM_QUEST:

                break;
            case ActorType.SHOP:
                o_capsule = GetComponent<CapsuleCollider>();

                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
    
    }
    public void doDamage(float dmgNum){
        //physicalDmg = (physic_atk * physic_dmgNum) / targetPhysicDef 
        //magicDmg = (magic_atk * magic_dmgNum) / targetMagicDef
        
        //elementDmg = (physic_dmgNum * (selfElement - targetelementRes)) || (magic_dmgNum * (selfElement - targetelementRes))
        //raceDmg = (physic_dmgNum * race bonus damage) || (magic_dmgNum * race bonus damage)

        //basicDmg = (physicalDmg || magicDmg) + elementDmg + raceDmg
        //crit = (basicDmg * (critDmgNum + (critDmg / 100)))
        //finalDmg = basicDmg + crit
    }
}
