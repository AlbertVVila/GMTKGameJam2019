using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float tongueRange = 10f;
    public float tongueSpeed = 20f;
    public Vector2 defaultTongueSize;

    [HideInInspector]
    public bool isGrabbed = false;

    [HideInInspector]
    public Vector2 surfaceContactPosition = Vector2.negativeInfinity;

    [HideInInspector]
    public bool hasContactedSurface = false;

    [HideInInspector]
    public GameObject tongue;

    [HideInInspector]
    public Vector3 initTonguePosition;

    public PlayerState currentState;

    public IdleState idleState;
    public ThrowingTongueState throwingTongue;
    public HoldingTongueState holdingTongue;
    public BackingTongueState backingTongue;
    public JumpingToSurfaceState jumpToSurface;

    private void Awake()
    {
        idleState = new IdleState(this);
        throwingTongue = new ThrowingTongueState(this);
        holdingTongue = new HoldingTongueState(this);
        backingTongue = new BackingTongueState(this);
        jumpToSurface = new JumpingToSurfaceState(this);

        currentState = idleState;
    }
    // Start is called before the first frame update
    void Start()
    {
        tongue = transform.GetChild(0).gameObject;
        initTonguePosition = tongue.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerState nextState = currentState.Update();
        if(nextState != currentState)
        {
            currentState.OnExit();
            nextState.OnEnter();
            currentState = nextState;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Surface"))
        {
            surfaceContactPosition = tongue.transform.position;

            Vector2 offset = (surfaceContactPosition - (Vector2)transform.position).normalized * 1f; //Offset for attaching to grid
            surfaceContactPosition -= offset;

            hasContactedSurface = true;
        }

        if (collision.CompareTag("Comestible"))
        {
            collision.transform.SetParent(this.transform.Find("tongue"));
        }

    }
}