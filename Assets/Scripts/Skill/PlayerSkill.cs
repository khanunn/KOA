using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("Skill Settings")]
    Animator animator;
    [SerializeField] int Skill_ID;
    [SerializeField] bool isSkillPlaying = false;

    [Header("Equipment Settings")]
    public bool IsWeapon = false;
    [SerializeField] SkinnedMeshRenderer Sword;
    [SerializeField] SkinnedMeshRenderer Shield;

    [Header("Player Setting")]
    [SerializeField] PlayerController Player;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void SkillSystem()
    {        
        if (!isSkillPlaying)
        {
            if (IsWeapon) // Have Sword
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    Debug.Log(isSkillPlaying);
                    isSkillPlaying = true;
                    StartSkill(0);
                    Invoke("ResetSkill", 0.1f);
                }

                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    Debug.Log(isSkillPlaying);
                    isSkillPlaying = true;
                    StartSkill(1);
                    Invoke("ResetSkill", 0.1f);
                }
            }

            if (!IsWeapon) // Bare hand
            {
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    StartSkill(0);
                    Invoke("ResetSkill", 0.1f);
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                StartSkill(3);
                Invoke("ResetSkill", 0.1f);
            }

            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                StartSkill(4);
                Invoke("ResetSkill", 0.1f);
            }
        }
    }

    private void ResetSkill()
    {
        animator.SetInteger("Skill_ID", -1);        
    }

    void StartSkill(int skillId)
    {
        isSkillPlaying = true;
        Player.StopSequence();
        //animator.SetInteger("Skill_ID", skillId);
        animator.Play(skillId.ToString() + "- Sword");
        Debug.Log(skillId.ToString());
    }

    private void Update()
    {
        SkillSystem();

        Skill_ID = animator.GetInteger("Skill_ID");
        animator.SetBool("IsWeapon", IsWeapon);

        if (IsWeapon)
        {
            Sword.enabled = true;
            Shield.enabled = true;
        }
        else
        {
            Sword.enabled = false;
            Shield.enabled = false;
        }

        // Check if the animation has finished
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill")) //Don't forget to add tag in animator
        {
            isSkillPlaying = false;
            //ResetSkill();
        }

        if(isSkillPlaying)
        {
            if(Input.GetKey(KeyCode.Mouse1))
            {
                Player.StopSequence();
            }
        }

    }
}
