using System;
using UnityEngine;
using UnityEngine.UI;
//using UnityStandardAssets.CrossPlatformInput;
//using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private bool m_IsSneaking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] private float m_SneakSpeed;
        [SerializeField] private float m_ogRunSpeed;
        [SerializeField] private float SprintDuration;
        [SerializeField] private float SprintCooldown;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;
        public static FirstPersonController instance; // Singleton instance

        private bool IsBookButton;

        public GameManager gameManager;
        private Vector3 initialPlayerPosition;

        public Slider staminaBar; // Reference to the stamina UI bar

        private void Awake()
        {
            // Singleton pattern to ensure only one instance of the FirstPersonController exists
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject); // Destroy any duplicate
            }
        }

        // Use this for initialization
        private void Start()
        {
            
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
            m_SneakSpeed = m_WalkSpeed;
            m_ogRunSpeed = m_RunSpeed;
            SprintDuration = 700;
            SprintCooldown = 0;

        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }

            //***********************************************************************

            //Stamina/Sprint Duration
            if (!m_IsWalking)
            {
                if (SprintDuration > 0 && ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow))))
                {
                    SprintDuration--;
                    staminaBar.value = SprintDuration;
                }
            }

            //Stamina/Sprint Cooldown
            if (SprintDuration == 0 && SprintCooldown < 700)
            {
                SprintCooldown++;
                staminaBar.value = SprintDuration;
            }
            //When the cooldown is full, reset it and the SprintDuration.
            if (SprintCooldown == 700)
            {
                SprintDuration = 700;
                SprintCooldown = 0;
                staminaBar.value = SprintDuration;
            }
            //This is what allows the SprintDuration to increase dynamically.
            if (m_IsWalking == true && ((SprintDuration < 700) && (SprintDuration > 0)))
            {
                SprintDuration++;
                staminaBar.value = SprintDuration;
            }

            //*************************************************************************

            if (m_IsSneaking)
            {
                m_WalkSpeed = (m_SneakSpeed) - 2;
                m_Camera.transform.localPosition = new Vector3(0, 0, 0);
                m_RunSpeed = m_WalkSpeed;
            }
            if (!m_IsSneaking)
            {
                m_WalkSpeed = m_SneakSpeed;
                m_Camera.transform.localPosition = m_OriginalCameraPosition;
                m_RunSpeed = m_ogRunSpeed;
            }

            if (IsBookButton)
            {
                //if player has collected the book object
                //{
                //  call method/s from encyclopedia
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }

        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
            m_IsSneaking = Input.GetKey(KeyCode.C);
            IsBookButton = Input.GetKey(KeyCode.B);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            //Player should not be sprinting if they are out of stamina.
            if (SprintDuration == 0)
            {
                speed = m_WalkSpeed;
                m_IsWalking = true;
            }

            if (SprintDuration > 0)
            {
                speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            }

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
        private void OnCollisionEnter(Collision collision)
        {
             // Check if the collided object has the "Player" tag
            if ((collision.gameObject.CompareTag("Enemy")) || (collision.gameObject.CompareTag("Trap")))
             {
                // Load the main menu scene
                GameManager.instance.GameOver();
             }
            if (collision.gameObject.CompareTag("Finish"))
             {
gameManager.score += 1000;
FindObjectOfType<PointsDisplay>().AddPoints(1000);
                m_CharacterController.enabled = false;;
                gameManager.ResetPositions();
                m_CharacterController.enabled = true;
         SprintDuration = 700;
         SprintCooldown = 0;
         staminaBar.value = SprintDuration;
             // Load the main menu scene
             gameManager.showContinueMenu();
            }
            if (collision.gameObject.CompareTag("Coin"))
            {
                gameManager.addScore(100);
            }
            if (collision.gameObject.CompareTag("Book"))
            {
                gameManager.addScore(500);
            }

        }

    }
}
