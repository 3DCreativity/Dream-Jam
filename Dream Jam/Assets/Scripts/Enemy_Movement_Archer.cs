//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy_Movement : MonoBehaviour
//{

//    public CharacterController2D controller;
//    public Transform attackVision;
//    public Vector2 visionSize;
//    public float visionAngle;
//    public float searchTime = 4f;
//    public LayerMask PlayerMask;
//    public GameObject arrowPrefab;
//    public LayerMask Ground;
//    bool targetLocated = false;
//    GameObject player;
//    float horizontalMove;
//    public float speed = 0.5f;
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }
//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        Collider2D[] Playercolliders = Physics2D.OverlapBoxAll(attackVision.position, visionSize, visionAngle, PlayerMask);
//        Collider2D[] Groundcolliders = Physics2D.OverlapBoxAll(attackVision.position, visionSize, visionAngle, Ground);
//        for (int i=0;i<Groundcolliders.Length; i++)
//        {
//            Vector2 objectPos = gameObject.transform.position;
//            Vector2 playerPos = Playercolliders[0].gameObject.transform.position;
//            Vector2 groundPos = Groundcolliders[i].gameObject.transform.position;
//            if (objectPos.x - playerPos.x <  objectPos.x - groundPos.x || objectPos.y - playerPos.y < objectPos.y - groundPos.y)
//        }
//        if (targetLocated)
//        {
            
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (targetLocated == false)
//        {
//            StartCoroutine(Patrolling());
//        }
//        else
//        {
//            return;
//        }
//    }
//    IEnumerator Patrolling()
//    {
//        yield return new WaitForSeconds(searchTime);
//    }
//}
