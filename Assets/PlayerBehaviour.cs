using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] Animator _animator;

    public bool facingRight;
    Rigidbody2D rb;
    public float speed;

    public GameObject playerObject;

    public ParticleSystem particles;
    public ParticleSystem attackParticles;


    // Start is called before the first frame update
    void Start()
    {
        // inputVector = new Vector2();
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        speed = 5;

        StartCoroutine(PlayerJumpLerp());
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxisRaw("Horizontal");

        if (movement > 0)
        {
            if (!facingRight)
            {
                Flip();
            }
            transform.position += (Vector3.right * movement * speed * Time.deltaTime);
            _animator.SetFloat("Horizontal_Right", Mathf.Abs(movement));
        }

        if (movement < 0)
        {
            if (facingRight)
            {
                Flip();
            }
            transform.position += (Vector3.right * movement * speed * Time.deltaTime);
            _animator.SetFloat("Horizontal_Right", Mathf.Abs(movement));
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            _animator.SetFloat("Horizontal_Right", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("Jump", true);
            StartCoroutine(PlayerJumpLerp());
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _animator.SetBool("Jump", false);
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetBool("attack", true);
        }
        else
        {
            _animator.SetBool("attack",false);
        }
      }
  
        public void Flip()
        {
            //   transform.Rotate(0f, 180f, 0f);
            GetComponent<RectTransform>().Rotate(0f, 180f, 0f);
            //particles
            //attackParticles.GetComponent<Renderer>().
            
            facingRight = !facingRight;
        }

        public IEnumerator PlayerJumpLerp()
        {
            Vector3 targetPosition = transform.position + Vector3.up * 2;
            float elapsedTime = 0;
            float duration = 5;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                transform.position = Vector3.Lerp(transform.position, targetPosition, t);
                elapsedTime += Time.deltaTime;
            }
            elapsedTime = duration;
            yield return null;
        }

    public void EventForJump()
    {
        Debug.Log("Player Jumped");
        //particles.Emit(transform.position,new Vector3(-10f,0f,0f),5,1f,Color.red);
        particles.Emit(50);

    }

    public void AttackParticlesEvent()
    {
        //Instantiate(attackParticles);
       attackParticles.Emit(50);
        //attackParticles.GetComponent<Renderer>().
    }
    }

