using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioClips
{
    public AudioClip startGame;
    public AudioClip jump;
    public AudioClip collectCoin;
}

[System.Serializable]
public class Velocities
{
    public float forwardFactor;
    public float horizontalFactor;
    public float jump;
}

[RequireComponent(typeof(CharacterController))]
public class MarioController : MonoBehaviour
{
    // Public variables
    [SerializeField]
    private AudioClips clips;
    [SerializeField]
    private Velocities velocities;

    // Private objects for Unity Component
    private Animator animator;
    private CharacterController characterController;
    private AudioSource audioSource;

    // Provate variable holding Game Controller Game Object
    private GameController gameController;

    // Private variable used in the code
    private float horizontalRotation = 0.0f;
    private float verticalVelocity = 0.0f;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        animator = this.gameObject.GetComponent<Animator>();
        characterController = this.gameObject.GetComponent<CharacterController>();
        audioSource = this.gameObject.GetComponent<AudioSource>();

        // Audio for Start Game
        audioSource.PlayOneShot(clips.startGame);

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void Update()
    {
        // Retrieve speeds
        float forwardSpeed = Input.GetAxis("Vertical") * velocities.forwardFactor;

        forwardSpeed = Mathf.Max(0.0f, forwardSpeed);
        horizontalRotation += Input.GetAxis("Horizontal") * velocities.horizontalFactor;
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // Get space button for jump
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            verticalVelocity = velocities.jump;
            animator.SetTrigger("Jump");
            audioSource.PlayOneShot(clips.jump);
        }

        // Set speeds
        Vector3 speed = new Vector3(0.0f, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;

        // Set animations
        animator.SetFloat("Speed", forwardSpeed);

        // Move the character
        characterController.Move(speed * Time.deltaTime);
        characterController.transform.localRotation = Quaternion.Euler(0, horizontalRotation, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(clips.collectCoin);

            gameController.addCoin();
        }
    }
}
