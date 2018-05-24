using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittlePlayer : MonoBehaviour
{
    [Header("basic player Properties")]
    public float acceleration = 100.0f;
    public float maxSpeed = 100000000000f;
    public float groundDrag = 6f;
    public float airDrag = 1f;
    private Vector3 _movement;
    private Vector3 _jump;
    private Rigidbody _rb;
    private CapsuleCollider _cap;
    private Camera _cam;

    [Header("Jump Properties")]
    public float JumpForce = 25f;
    private bool _grounded = false;
    private RaycastHit Grounded;

    [Header("Slide Properties")]
    public float SlideForce = 100f;
    public float MaxSlideDistance = 120f;
    private Vector3 _slideVec;
    private float _curSlideDistance;
    private bool Slideing;

    [Header("WallRun Properties")]
    public bool _wallRun = false;
    public float wallrunAcceleration = 150f;
    private bool isWallR = false;
    private bool isWallL = false;
    private bool isWallF = false;
    private bool isWallB = false;
    private float _velocityFloat;
    private float _wallRunTimer = 1.5f;
    private RaycastHit hitR;
    private RaycastHit hitL;
    private RaycastHit hitF;
    private RaycastHit hitB;

    [Header("Sickest Lerps")]
    public float rotationSpeed = 4.0f;
    public Quaternion targetAngle;
    private Vector3 currentAngle;
    private Vector3 temp;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _cap = gameObject.GetComponent<CapsuleCollider>();
        _cam = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        VelocityFix();
        GroundedCheck();
        wallsCheck();

        Movement();
        Jumping();
        wallRunning();
        Sliding();

        Debug.Log(_velocityFloat);
    }

    // FixedUpdate does the final calculations so it isn't frame depended.

    void FixedUpdate()
    {
        _rb.AddForce(_movement);
        if (_rb.velocity.magnitude > maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * maxSpeed;
        }
    }


    //All the movement stuff:

    private void Movement()
    {
        _movement = Vector3.zero;

        if (!Slideing)
        {
            _movement.z = Input.GetAxis("Vertical");
            _movement.x = Input.GetAxis("Horizontal");

            if (isWallR)
            {
                if (_movement.x > 0)
                {
                    _movement.x = 0;
                    Debug.Log("jaih");
                }
            }

            if (isWallL)
            {
                if (_movement.x < 0)
                {
                    _movement.x = 0;
                }
            }
        }

        if (_grounded)
        {
            _movement = transform.rotation * (_movement * acceleration);
            _rb.drag = groundDrag;
        }
        else if (_wallRun)
        {
            _movement = transform.rotation * (_movement * wallrunAcceleration);
            _rb.drag = groundDrag;
        }
        else
        {
            _movement = transform.rotation * (_movement * (acceleration * 0.1f));
            _rb.drag = airDrag;
        }

    }

    private void Jumping()
    {
        if ((Input.GetButtonDown("Jump")) && _grounded)
        {
            _rb.AddForce((Vector3.up * JumpForce) + (_rb.velocity), ForceMode.Impulse);

        }
    }

    private void wallRunning()
    {
        if (!_grounded)
        {
            if (_velocityFloat > 2.0f)
            {
                if (isWallR)
                {
                    _rb.useGravity = false;

                    temp = Vector3.Cross(transform.up, -hitR.normal);
                    targetAngle = Quaternion.LookRotation(-temp);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotationSpeed * Time.deltaTime);

                    _wallRun = true;
                    WallRunTimer();

                    if ((Input.GetButtonDown("Jump")))
                    {
                        WallJump();
                    }
                }

                else if (isWallL)
                {
                    _rb.useGravity = false;

                    Vector3 temp = Vector3.Cross(transform.up, hitL.normal);
                    targetAngle = Quaternion.LookRotation(-temp);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotationSpeed * Time.deltaTime);

                    _wallRun = true;
                    WallRunTimer();

                    if ((Input.GetButtonDown("Jump")))
                    {
                        WallJump();
                    }
                }

                else if (isWallF)
                {
                    if ((Input.GetButtonDown("Jump")))
                    {
                        WallJump();
                    }
                }

                else if (isWallB)
                {
                    if ((Input.GetButtonDown("Jump")))
                    {
                        WallJump();
                    }
                }
                else
                {
                    _rb.useGravity = true;
                    _wallRun = false;
                }
            }
            else
            {
                if ((Input.GetButtonDown("Jump")))
                {
                    WallJump();
                }

            }
        }
    }

    private void WallJump()
    {
        _wallRunTimer = 1.5f;
        if (isWallR)
        {
            _rb.AddForce((-transform.right * (JumpForce * 0.7f)) + (transform.up * (JumpForce * 0.75f)), ForceMode.Impulse);
        }
        if (isWallL)
        {
            _rb.AddForce((transform.right * (JumpForce * 0.7f)) + (transform.up * (JumpForce * 0.75f)), ForceMode.Impulse);
        }
        if (isWallF)
        {
            _rb.AddForce((-transform.forward * (JumpForce * 0.7f)) + (transform.up * JumpForce * 0.75f), ForceMode.Impulse);
        }
        if (isWallB)
        {
            _rb.AddForce((transform.forward * (JumpForce * 0.7f)) + (transform.up * JumpForce * 0.75f), ForceMode.Impulse);
        }
    }

    private void WallRunTimer()
    {
        _wallRunTimer -= Time.deltaTime;
        if (_wallRunTimer < 0)
        {
            isWallL = false;
            isWallR = false;
            isWallF = false;
            isWallB = false;
            _wallRun = false;
            _rb.useGravity = true;
        }
    }

    private void Sliding()
    {
        if (_wallRun != true)
        {
            if ((Input.GetKeyDown(KeyCode.LeftShift)) && _grounded)
            {
                groundDrag = 0;
                _cap.height = 0.5f;
                _rb.velocity += _rb.velocity;
                Slideing = true;
            }
            if ((Input.GetKeyUp(KeyCode.LeftShift)))
            {
                Slideing = false;
                groundDrag = 6;
                _cap.height = 2;
            }

            if (Slideing)
            {

            }
            else
            {

            }
            if (_curSlideDistance >= MaxSlideDistance)
            {
                Slideing = false;
                _curSlideDistance = 0;
            }
        }
    }


    //Before all the movement stuff is applied:

    private void VelocityFix()
    {
        if (_rb.velocity.y > 0)
        {
            _velocityFloat = _rb.velocity.magnitude - _rb.velocity.y;
        }
        else
        {
            _velocityFloat = _rb.velocity.magnitude + _rb.velocity.y;
        }
    }

    private void GroundedCheck()
    {
        if (Physics.Raycast(transform.position - new Vector3(0.49f, 0.2f, 0), -transform.up, out Grounded, 1))
        {
            if (Grounded.transform.tag == "Wall")
            {
                _grounded = true;
                _wallRunTimer = 1.5f;
                if (!Slideing)
                {
                    groundDrag = 6f;
                }
            }
            if (Grounded.transform.tag == "Slope")
            {
                groundDrag = 0f;
            }
        }
        else if (Physics.Raycast(transform.position - new Vector3(-0.49f, 0.2f, 0), -transform.up, out Grounded, 1))
        {
            if (Grounded.transform.tag == "Wall")
            {
                _grounded = true;
                _wallRunTimer = 1.5f;
                if (!Slideing)
                {
                    groundDrag = 6f;
                }
            }
            if (Grounded.transform.tag == "Slope")
            {
                groundDrag = 0f;
            }
        }
        else if (Physics.Raycast(transform.position - new Vector3(0, 0.2f, -0.49f), -transform.up, out Grounded, 1))
        {
            if (Grounded.transform.tag == "Wall")
            {
                _grounded = true;
                _wallRunTimer = 1.5f;
                if (!Slideing)
                {
                    groundDrag = 6f;
                }
            }
            if (Grounded.transform.tag == "Slope")
            {
                groundDrag = 0f;
            }
        }
        else if (Physics.Raycast(transform.position - new Vector3(0, 0.2f, 0.49f), -transform.up, out Grounded, 1))
        {
            if (Grounded.transform.tag == "Wall")
            {
                _grounded = true;
                _wallRunTimer = 1.5f;
                if (!Slideing)
                {
                    groundDrag = 6f;
                }
            }
            if (Grounded.transform.tag == "Slope")
            {
                groundDrag = 0f;
            }
        }
        else
        {
            _grounded = false;
        }
    }

    private void wallsCheck()
    {
        if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
        {
            if (hitR.transform.tag == "Wall")
            {
                isWallR = true;
                isWallL = false;
                isWallF = false;
                isWallB = false;
            }
        }

        else if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
        {
            if (hitL.transform.tag == "Wall")
            {
                isWallR = false;
                isWallL = true;
                isWallF = false;
                isWallB = false;
            }
        }

        else if (Physics.Raycast(transform.position, transform.forward, out hitF, 1))
        {
            if (hitF.transform.tag == "Wall" || hitF.transform.tag == "Floor")
            {
                isWallR = false;
                isWallL = false;
                isWallF = true;
                isWallB = false;
            }
        }

        else if (Physics.Raycast(transform.position, -transform.forward, out hitB, 1))
        {
            if (hitB.transform.tag == "Wall" || hitB.transform.tag == "Floor")
            {
                isWallR = false;
                isWallL = false;
                isWallF = false;
                isWallB = true;
            }
        }
        else
        {
            isWallR = false;
            isWallL = false;
            isWallF = false;
            isWallB = false;
            _wallRun = false;
            _rb.useGravity = true;
        }
    }
}