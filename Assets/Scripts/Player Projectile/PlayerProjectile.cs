using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] Vector3 TargetPos;
    bool IsStart = false;

    [Header("Projectile Setting")]

    [SerializeField] float Speed = 1;    

    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(null);
        GameObject PlayerTemp = GameObject.FindGameObjectWithTag("Player").gameObject;
        controller = PlayerTemp.GetComponent<PlayerController>();
        TargetPos = controller.mousePositionInScene;        
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) TargetPos = hit.point; // Mouse position in scene coordinates

        Vector3 direction = (this.transform.position - TargetPos).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));
        this.transform.rotation = lookRotation;

        IsStart = true;        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStart)
        {          
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed);
        }

        if (Vector3.Distance(transform.position, TargetPos) <= 0.1f) Destroy(gameObject);

    }    
}
