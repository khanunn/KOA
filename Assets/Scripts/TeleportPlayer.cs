using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] GameObject Player;
   async void Start()
    {
        GameObject PlayerTemp = GameObject.FindGameObjectWithTag("Player");
        Player = PlayerTemp;
        NavMeshAgent agent = Player.GetComponent<PlayerController>().agent;
        this.transform.SetParent(null);
        //this.transform.position = Player.transform.position;
        Task.Delay(1000);

        RaycastHit hit;
        Vector3 MousePosition = new Vector3();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) MousePosition = hit.point; // Mouse position in scene coordinates
        agent.SetDestination(MousePosition);
       
        Player.transform.position = MousePosition;
        
       
    }

}
