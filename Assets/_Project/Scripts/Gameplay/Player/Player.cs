using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TOW.Core;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    Animator animator;
    Vector2 lastMoveDir = Vector2.down;

    private bool canInteract = false;
    private string interactType = "";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0) y = 0;

        Vector2 v = new Vector2(x, y) * moveSpeed;
        rb.velocity = v;

        if (animator != null)
        {
            animator.SetFloat("MoveX", x);
            animator.SetFloat("MoveY", y);

            bool isMoving = v.magnitude > 0.1f;
            animator.SetBool("IsMoving", isMoving);

            if (isMoving)
                lastMoveDir = new Vector2(x, y);

            animator.SetFloat("LastMoveX", lastMoveDir.x);
            animator.SetFloat("LastMoveY", lastMoveDir.y);
        }

        // -------- INTERAÇÃO (PRESSIONAR E) --------
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            // salva de onde o player veio
            GameManager.Instance.lastSceneBeforeVerses =
                SceneManager.GetActiveScene().name;

            // abre a tela de versículos
            SceneManager.LoadScene("VerseSelection");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Verses"))
        {
            canInteract = true;
            interactType = "verses";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Verses"))
        {
            canInteract = false;
            interactType = "";
        }
    }
}
