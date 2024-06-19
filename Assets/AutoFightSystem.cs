using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFightSystem : MonoBehaviour
{
    public RaycastHit[] targets;
    public Transform nearsestTarget;
   [SerializeField]private float _radius;
   [SerializeField]private LayerMask _targetLayer;
   [SerializeField]private Vector3 _rayDir;
   private InteractableType targetype;
   


    void FixedUpdate()
    {
        targets = Physics.SphereCastAll(transform.position, _radius , _rayDir, 0 , _targetLayer);
        if(targets != null){
            nearsestTarget = GetNearest();
        }
    }

    Transform GetNearest(){
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit target in targets){
            Vector3 p_Pos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(p_Pos, targetPos);

             if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;

    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_radius);
    }
}
