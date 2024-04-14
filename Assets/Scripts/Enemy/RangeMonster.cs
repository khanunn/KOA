using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeMonster : MonoBehaviour
{
    [SerializeField] Transform SpawnPosition;
    [SerializeField] GameObject Bullet;
    [SerializeField] Interactable Target;
    [SerializeField] MonsterInfoSO monsterInfoSO;

    // Start is called before the first frame update
    void Start()
    {
        if(Bullet == null) Bullet = Resources.Load<GameObject>("/Items/Bullet");
        
        if (monsterInfoSO == null) monsterInfoSO = this.GetComponent<PatrolController>().monsterInfoSO;
    }

    public void CreateBullet()
    {
        GameObject CreateBullet = Instantiate(Bullet, SpawnPosition.transform);
        Bullet bulletSetting = CreateBullet.GetComponent<Bullet>();
        bulletSetting.Target = this.Target;        
        bulletSetting.Damage = monsterInfoSO.Damage;
        bulletSetting.IsFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) Target = GetComponent<PatrolController>().target;
    }
}
