using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("Skill Settings")]
    Animator animator;
    [SerializeField] int Skill_ID;
    bool isSkillPlaying = false;

    [Header("Equipment Settings")]
    public bool IsWeapon = false;
    [SerializeField] SkinnedMeshRenderer Sword;
    [SerializeField] SkinnedMeshRenderer Shield;

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
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    StartSkill(0);
                    Invoke("ResetSkill", 0.1f);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    StartSkill(1);
                    Invoke("ResetSkill", 0.1f);
                }
            }

            if (!IsWeapon) // Bare hand
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    StartSkill(0);
                    Invoke("ResetSkill", 0.1f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartSkill(3);
                Invoke("ResetSkill", 0.1f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
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
        animator.SetInteger("Skill_ID", skillId);
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill"))
        {
            isSkillPlaying = false;

        }
    }
}
