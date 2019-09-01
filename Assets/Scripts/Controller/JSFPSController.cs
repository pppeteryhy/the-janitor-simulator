﻿using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class JSFPSController : MonoBehaviour
{
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_StickToGroundForce;
    [SerializeField] private float m_GravityMultiplier;
    [SerializeField] private MouseLook m_MouseLook;
    [SerializeField] private bool m_UseFovKick;
    [SerializeField] private FOVKick m_FovKick = new FOVKick();
    [SerializeField] private bool m_UseHeadBob;
    [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
    [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
    [SerializeField] private float m_StepInterval;
    [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
    [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
    [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.
    [SerializeField] private PlayerRaycaster m_pRaycaster;
    [SerializeField] private Transform m_ToolGrabPoint;
    [SerializeField] private SkinnedMeshRenderer[] m_HandRenderes;
    [SerializeField] private Mesh[] m_meshes;
    [SerializeField] private Material material;

    private Mesh leftMesh;
    private Mesh rightMesh;
    private Material defaultMaterial;

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
    private Animator[] m_Animators;
    private IOutline previousOutlineObj;
    private GameObject currentTool;

    // Use this for initialization
    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        m_OriginalCameraPosition = m_Camera.transform.localPosition;
        m_FovKick.Setup(m_Camera);
        m_HeadBob.Setup(m_Camera, m_StepInterval);
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        m_Jumping = false;
        m_AudioSource = GetComponent<AudioSource>();
        m_Animators = GetComponentsInChildren<Animator>();
        m_MouseLook.Init(transform, m_Camera.transform);

        leftMesh = m_HandRenderes[0].sharedMesh;
        rightMesh = m_HandRenderes[1].sharedMesh;
        defaultMaterial = m_HandRenderes[0].material;
    }


    // Update is called once per frame
    private void Update()
    {
        if (JSGameManager.Instance.gameState != GameState.InGame)
            return;
        RotateView();
        UpdateAnimator();
        // the jump state needs to read here to make sure it is not missed
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
        {
            StartCoroutine(m_JumpBob.DoBobCycle());//跳跃时的颠簸
            PlayLandingSound();
            m_MoveDir.y = 0f;
            m_Jumping = false;
            //当角色落地时，重置跳跃，y方向的移动置0
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

    private void UpdateAnimator()
    {
        for (int i = 0; i < m_Animators.Length; i++)
        {
            m_Animators[i].SetBool("IsWalking", m_Input.sqrMagnitude > 0.1f);
        }
    }


    private void FixedUpdate()
    {
        if (JSGameManager.Instance.gameState != GameState.InGame)
            return;
        float speed;
        GetInput(out speed);
        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        //在斜坡上的移速会受到约束
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.z = desiredMove.z * speed;


        if (m_CharacterController.isGrounded)
        {
            m_MoveDir.y = -m_StickToGroundForce;
            // 在地面上才能跳跃
            if (m_Jump)
            {
                for (int i = 0; i < m_Animators.Length; i++)
                {
                    m_Animators[i].SetBool("Jump", m_Input.sqrMagnitude > 0.1f);
                }
                m_MoveDir.y = m_JumpSpeed;
                PlayJumpSound();
                m_Jump = false;
                m_Jumping = true;
            }
        }
        else
        {
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime; // 加速度 m/s2; 加速度 * 时间 = 速度 m/s
        }
        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
        UpdateCameraPosition(speed);

        //m_MouseLook.UpdateCursorLock();

        //搜索垃圾
        GarbageBase garbage;
        GarbageCollectorCar garbageCar;
        IOutline outlineObj;
        m_pRaycaster.RaycastToSearch(1.5f, out garbage, out garbageCar, out outlineObj);
        if (previousOutlineObj != null && previousOutlineObj.GetTransform() != null && previousOutlineObj != outlineObj)
        {
            EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnBreakPickUp);
            previousOutlineObj.DisableOutlineColor();
        }

        if (outlineObj != null)
        {
            EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnLockGarbage, outlineObj.GetTransform().position, outlineObj.GetName());
            previousOutlineObj = outlineObj;
            outlineObj.EnableOutlineColor();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //捡垃圾
            if (garbage != null)
            {
                JanitorTool cTool = InventoryManager.Instance.GetCurrentTool();
                bool canClean = true;
                //当所持工具类型不匹配
                if (garbage.toolType != cTool.toolType)
                {
                    canClean = false;
                    UIManager.Instance.ShowMessage<UIMessageInGame>(UIDepthConst.TopDepth, "Please choose suitable janitor tool！", 1.0f);
                }
                //当工具耐久度不够
                if (!cTool.IsUseable)
                {
                    canClean = false;
                    UIManager.Instance.ShowMessage<UIMessageInGame>(UIDepthConst.TopDepth, "Your tool doesn't have enough cleanliness!", 1.0f);
                }
                //当垃圾袋容量不够
                if (!PackageManager.Instance.HasEnoughCapacity(garbage.pacCapcityCost))
                {
                    canClean = false;
                    UIManager.Instance.ShowMessage<UIMessageInGame>(UIDepthConst.TopDepth, "Your package bag doesn't have enough capacity!", 1.0f);
                }
                if (canClean)
                {
                    Action tmp;
                    tmp = garbage.OnCleaned;
                    tmp += () =>
                    {
                        JSGameManager.Instance.GarbageCountLeft--;
                        print(JSGameManager.Instance.GarbageCountLeft);
                        InventoryManager.Instance.UseTool();
                        PackageManager.Instance.OnPackageUse(garbage.pacCapcityCost);
                        EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnPickedUp);
                        garbage = null;
                    };
                    EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnStartPickUp, garbage.cleaningTimeNeeded, tmp);
                    for (int i = 0; i < m_Animators.Length; i++)
                    {
                        m_Animators[i].SetBool(cTool.animTrigger, m_Input.sqrMagnitude > 0.1f);
                    }
                }
            }
            //到垃圾车处清理
            else if(garbageCar != null)
            {
                Action tmp = null;
                JanitorTool cTool = InventoryManager.Instance.GetCurrentTool();
                int toolIndex = InventoryManager.Instance.currentTool;
                tmp += () =>
                {
                    EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnToolUse, toolIndex, (float)cTool.currentClean, (float)cTool.maxClean);
                    InventoryManager.Instance.CleanCurrentTools();
                    PackageManager.Instance.ResetCapacity();
                    UIManager.Instance.ShowMessage<UIMessageInGame>(UIDepthConst.TopDepth, "Cleaned your tool and package successfully", 1.0f);
                };
                EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnStartPickUp, GameSetting.TimeNeedForCleaningTools, tmp);
            }
        }

        //切换装备
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InventoryManager.Instance.SwitchTool();
            OnSwicthTool();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InventoryManager.Instance.SwitchTool(0);
            OnSwicthTool();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.Instance.SwitchTool(1);
            OnSwicthTool();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InventoryManager.Instance.SwitchTool(2);
            OnSwicthTool();
        }
    }

    //在切换工具时，手上的工具保持切换
    private void OnSwicthTool()
    {
        if(currentTool != null)
        {
            Destroy(currentTool);
        }
        JanitorTool toolIns = InventoryManager.Instance.GetCurrentTool();
        switch (toolIns.ID)
        {
            case 1001:
                m_HandRenderes[0].sharedMesh = m_meshes[0]; //ResourceLoader.Instance.Load<Mesh>("111");
                m_HandRenderes[1].sharedMesh = m_meshes[1]; //ResourceLoader.Instance.Load<Mesh>("111");
                m_HandRenderes[0].material = material;
                m_HandRenderes[1].material = material;
                break;
            case 1002:
            case 1003:
            case 1004:
                m_HandRenderes[0].sharedMesh = leftMesh;
                m_HandRenderes[1].sharedMesh = rightMesh;
                m_HandRenderes[0].material = defaultMaterial;
                m_HandRenderes[1].material = defaultMaterial;
                currentTool = Instantiate(ResourceLoader.Instance.Load<GameObject>(toolIns.prefabPath));
                currentTool.transform.SetParent(m_ToolGrabPoint);
                currentTool.transform.localPosition = toolIns.posOffset;
                currentTool.transform.localEulerAngles = toolIns.rotOffset;
                break;
        }
    }

    private void PlayJumpSound()
    {
        m_AudioSource.clip = m_JumpSound;
        m_AudioSource.Play();
    }

    //调整步长
    private void ProgressStepCycle(float speed)
    {
        //向量的长度是用勾股定理计算出来，计算机计算两次方和开根的运算量比加减法要费时的多。所以如果是想比较两个向量的长度，用sqrMagnitude可以快出很多。
        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
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


    private void UpdateCameraPosition(float speed)
    {
        Vector3 newCameraPosition;
        if (!m_UseHeadBob)
        {
            return;
        }
        if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
        {
            //根据通用类curveControlledBob的返回值，设置相机的位置，实现人物走路时的颠簸效果。
            m_Camera.transform.localPosition =
                m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                  (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
        }
        else
        {
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
        }
        m_Camera.transform.localPosition = newCameraPosition;
    }


    private void GetInput(out float speed)
    {
        // Read input
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
        // On standalone builds, walk/run speed is modified by a key press.
        // keep track of whether or not the character is walking or running
        m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
        // set the desired speed to be walking or running
        speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
        m_Input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }

        // handle speed change to give an fov kick
        // only if the player is going to a run, is running and the fovkick is to be used
        if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
        {
            StopAllCoroutines();
            StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
        }
    }

    //旋转视角
    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
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
        body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}
