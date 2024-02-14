using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossBehaviourScript : MonoBehaviour
{    
    Animator animator;
    MonsterInfoSO monsterInfoSO;
    Actor actor;    

    [Header("Property Setting")]
    public int HitCount = 0; //Added when entity attack (in PatrolController.cs)    
    public int EntityPhase = 1;
    public Vector3 TargetPosition;

    private float animationTimer;
   
    void LunchAttackAnimation()
    {
        if (EntityPhase == 1) //Now for using with RabbitReindeer
        {
            if (HitCount == 5)
            {
                HitCount = 0;
                animator.SetInteger("SkillID", 1); //Roaring
            }
            else animator.SetInteger("SkillID", 0);            
        }
        if (EntityPhase == 2) //Now for using with RabbitReindeer
        {
            switch (HitCount)
            {                
                case 3:                    
                    animator.SetInteger("SkillID", 1); //Roaring                                      
                    animationTimer = 5.15f;  // Animation Length                                      
                    HitCount++;
                    this.GetComponent<NavMeshAgent>().isStopped = true;                                      
                    break;
                case 7:                    
                    animator.SetInteger("SkillID", 2); //Jumping Attack
                    animationTimer = 3.24f;                
                    HitCount = 0;
                    this.GetComponent<NavMeshAgent>().isStopped = true;
                    break;
                default: animator.SetInteger("SkillID", 0);
                    break;
            }          
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        monsterInfoSO = this.GetComponent<PatrolController>().monsterInfoSO;
        actor = this.GetComponent<Actor>();          
    }
    
    // Update is called once per frame
    void Update()
    {
        if(animationTimer > 0)
        {
            animationTimer -= Time.deltaTime;
        }

        if (animationTimer <= 0)
        {
            this.GetComponent<NavMeshAgent>().isStopped = false;
        }

        if (actor.CurrentHealth <= actor.MaxHealth / 2) EntityPhase = 2; // When this Entity have current hp lower than 50%        
        LunchAttackAnimation();
    }
}
