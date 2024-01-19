using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class DemoEquipment : MonoBehaviour
{
    [SerializeField] EquipmentSetting EquipmentSetting;
    [SerializeField] private ItemInfoSO itemInfoSO;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.5f, transform.forward, Mathf.Infinity);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, hit.point);

                if (itemInfoSO.Value == 1)
                {
                    EquipmentSetting.ItemID[0] = 1;
                }
            }
        }
    }
}
