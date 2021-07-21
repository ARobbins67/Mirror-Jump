using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] UnityEvent JumpEvent;
    [SerializeField] UnityEvent GetStarEvent;
    [SerializeField] UnityEvent DeathEvent;

    public float speed = 4;
    public float jumpForce = 10;
    private StarManager starMan;
    private Rigidbody2D rigidbody;
    Vector2 moveVec = Vector2.zero;
    private bool onFloor;
    private float posVecY = 0f;    
    private GameObject BodyObj;
    private List<GameObject> Players = new List<GameObject>();
    private List<GameObject> BodyList = new List<GameObject>();
    private GameManager gameMan;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        BodyList.AddRange(GameObject.FindGameObjectsWithTag("Body"));
        gameMan = FindObjectOfType<GameManager>();
        starMan = FindObjectOfType<StarManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject body in BodyList)
        {
            if (body.transform.IsChildOf(gameObject.transform))
            {
                BodyObj = body;
            }
        }
        onFloor = true;        
    }

    // Update is called once per frame
    void Update()
    {
        moveVec.x = Input.GetAxis("Horizontal");
        if(gameObject.name == "RightCharacter")
        {
            moveVec.x = -moveVec.x;
        }
        if(moveVec.x != 0f)
        {
            rigidbody.transform.Translate(moveVec * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            // Jump
            JumpEvent.Invoke();
            rigidbody.AddForce(new Vector2(0,10f*jumpForce),ForceMode2D.Impulse);
            onFloor = false;
        }
    }

    public void SetState(bool bIsPaused)
    {
        enabled = !bIsPaused;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Star")
        {
            GetStarEvent.Invoke();
            StarManager.RemoveStar(collision);
            if (starMan.GetStarCount() <= 0)
            {
                Debug.Log("No stars remain");
                gameMan.EndLevel();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor" || collision.transform.tag == "Box")
        {
            // return true if character is on top of the collision
            onFloor = true;
        }

        if(collision.transform.tag == "Spike")
        {
            DeathEvent.Invoke();
            BodyList.Remove(gameObject);
            Destroy(gameObject);
            GameManager.RestartLevel();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor")
        {
            onFloor = false;
        }
    }
}