using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BossBehaviourScript : MonoBehaviour
{    
    Animator animator;
    MonsterInfoSO monsterInfoSO;
    Actor actor;

    private float animationTimer;


    [Header("Property Setting")]
    public int HitCount = 0; //Added when entity attack (in PatrolController.cs)    
    public int EntityPhase = 1;
    [Header("UI Setting")]
    [SerializeField] Slider BossHpBar;
    [SerializeField] TMP_Text DisplayText;

    [Header("Sound Setting")]
    [SerializeField] AudioClip BossTheme;
    void LunchAttackAnimation()
    {
        if (EntityPhase == 1) //Now for using with RabbitReindeer
        {
            if (HitCount == 5)
            {
                HitCount = 0;
                animator.SetInteger("SkillID", 1); //Roaring
                animationTimer = 6f;
                this.GetComponent<NavMeshAgent>().isStopped = true;
            }
            else animator.SetInteger("SkillID", 0);            
        }
        if (EntityPhase == 2) //Now for using with RabbitReindeer
        {
            switch (HitCount)
            {                
                case 3:                    
                    animator.SetInteger("SkillID", 1); //Roaring                                      
                    animationTimer = 6f;  // Animation Length                                      
                    HitCount++;
                    this.GetComponent<NavMeshAgent>().isStopped = true;                                      
                    break;
                case 7:                    
                    animator.SetInteger("SkillID", 2); //Jumping Attack
                    animationTimer = 4f;                
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

        //Just In case forget to set false
        BossHpBar.gameObject.SetActive(false);
        DisplayText.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        LunchAttackAnimation();

        //Set Counter
        if (animationTimer > 0)
        {
            animationTimer -= Time.deltaTime;
        }

        if (animationTimer <= 0)
        {
            this.GetComponent<NavMeshAgent>().isStopped = false;
        }       


        if (actor.CurrentHealth <= actor.MaxHealth / 2) EntityPhase = 2; // When this Entity have current hp lower than 50%        
        if (actor.CurrentHealth <= 0 )
        {
            BossHpBar.gameObject.SetActive(false);
            DisplayText.gameObject.SetActive(false);
            
        }      

        BossHpBar.value = actor.CurrentHealth; //Set HP bar
    }
    public void DestroyAfterDead()
    {
        Destroy(this.gameObject);
    }
    bool AlreadyIn = false;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            DisplayText.text = monsterInfoSO.DisplayName + "( Lv: " + monsterInfoSO.Level + " )";
            BossHpBar.maxValue = actor.MaxHealth;
            BossHpBar.gameObject.SetActive(true);
            DisplayText.gameObject.SetActive(true);

            if(!AlreadyIn)
            {
                other.gameObject.GetComponent<AudioSource>().clip = BossTheme;
                other.gameObject.GetComponent<AudioSource>().Play();
                AlreadyIn = true;
            }
            
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {            
            BossHpBar.gameObject.SetActive(false);
            DisplayText.gameObject.SetActive(false);
        }
    }*/
}
