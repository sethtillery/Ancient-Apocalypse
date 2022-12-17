using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    // holds 2D points; used to represent a character's location in 2D space, or where it's moving to
    public Vector2 movement = new Vector2();
    [SerializeField] CharacterStats character;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        lastHorizontalVector = 1f;
        lastVerticalVector = 1f;
    }

    void FixedUpdate()
    {
        if(character.isAlive)
            MoveCharacter();
    }

    private void MoveCharacter()
    {
        // get user input
        // GetAxisRaw parameter allows us to specify which axis we're interested in
        // Returns 1 = right key or "d" (up key or "w")
        //        -1 = left key or "a"  (down key or "s")
        //         0 = no key pressed
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0)
            lastHorizontalVector = movement.x;
        if (movement.y != 0)
            lastVerticalVector = movement.y;

        // keeps player moving at the same rate of speed, no matter which direction they are moving in
        movement.Normalize();

        // set velocity of RigidBody2D and move it
        m_body2d.velocity = movement * m_speed;
    }

    // Update is called once per frame
    void Update ()
    {
        if (character.isAlive)
        {
            // Increase timer that controls attack combo
            m_timeSinceAttack += Time.deltaTime;

            // Swap direction of sprite depending on walk direction
            if (movement.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            
            else if (movement.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            // -- Handle Animations --
            //Attack
            
            if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
            {
                m_currentAttack++;

                // Loop back to one after third attack
                if (m_currentAttack > 3)
                    m_currentAttack = 1;

                // Reset Attack combo if time since last attack is too large
                if (m_timeSinceAttack > 1.0f)
                    m_currentAttack = 1;

                // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                m_animator.SetTrigger("Attack" + m_currentAttack);

                // Reset timer
                m_timeSinceAttack = 0.0f;
            }
            

            //Run
            else if (Mathf.Abs(movement.x) > Mathf.Epsilon)
            {
                // Reset timer
                m_delayToIdle = 0.05f;
                m_animator.SetInteger("AnimState", 1);
            }

            //Idle
            else
            {
                // Prevents flickering transitions to idle
                m_delayToIdle -= Time.deltaTime;
                    if(m_delayToIdle < 0)
                        m_animator.SetInteger("AnimState", 0);
            }
        }
    }
}
