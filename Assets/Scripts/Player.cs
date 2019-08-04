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

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public Sprite spriteOnAir;
    public Sprite spriteBase;

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
        initTonguePosition = tongue.transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            spriteRenderer.sprite = spriteBase;
        }

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
        if (collision.CompareTag("Surface") && Vector2.Distance(transform.position, tongue.transform.position) > 2.5f)
        {
            surfaceContactPosition = tongue.transform.position;

            Vector2 offset = (surfaceContactPosition - (Vector2)transform.position).normalized * 1f; //Offset for attaching to grid
            surfaceContactPosition -= offset;

            spriteRenderer.sprite = spriteOnAir;

            hasContactedSurface = true;
        }

        if (collision.CompareTag("Comestible"))
        {
            collision.transform.SetParent(this.transform.Find("tongue"));
        }
    }
}