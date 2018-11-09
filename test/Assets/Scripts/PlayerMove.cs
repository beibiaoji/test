using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [SerializeField]
    private CharacterController cc;
    bool ismove = false;
    Vector3 targetPos = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
         if(   Physics.Raycast(ray, out hit))
            {
                Vector3 pos = hit.point;
               
                pos.y = transform.position.y;
                targetPos = pos;
                transform.LookAt(pos);
                ismove = true;
            }
        }
       
        if(ismove)
        {
            cc.SimpleMove(transform.forward * 4);
            if(Vector3.Distance(transform.position,targetPos)<0.1f)
            {
                ismove = false;

            }
        }
		
	}
}
