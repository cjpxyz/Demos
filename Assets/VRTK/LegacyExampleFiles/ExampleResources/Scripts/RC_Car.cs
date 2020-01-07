namespace VRTK.Examples
{
    using NetBase;
    using UnityEngine;

    public class RC_Car : MonoBehaviour
    {
        public float maxAcceleration = 3f;
        public float jumpPower = 10f;
        [Tooltip("Adjust player movement speed.")]
        public float playerMoveMultiplier;
        [Tooltip("Adjust player sprint speed.")]
        public float playerSprintMultiplier;

        private float acceleration = 0.05f;
        private float movementSpeed = 0f;
        private float rotationSpeed = 180f;
        private bool isJumping = false;
        private Vector2 touchAxis;
        private float triggerAxis;
        private Rigidbody rb;
        private Vector3 defaultPosition;
        private Quaternion defaultRotation;

        public void SetTouchAxis(Vector2 data)
        {
            touchAxis = data;
            //Debug.Log("touchAxis: " + touchAxis);
        }

        public void SetTriggerAxis(float data)
        {
            triggerAxis = data;
            //Debug.Log("triggerAxis: " + triggerAxis);
        }

        public void ResetCar()
        {
            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            defaultPosition = transform.position;
            defaultRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            if (isJumping)
            {
                touchAxis.x = 0f;
            }
            CalculateSpeed();
            Move();
            Turn();
            //Jump();
        }

        private void CalculateSpeed()
        {
            if (touchAxis.y != 0f)
            {
                movementSpeed += (acceleration * touchAxis.y);
                movementSpeed = Mathf.Clamp(movementSpeed, -maxAcceleration, maxAcceleration);
            }
            else
            {
                Decelerate();
            }
        }

        private void Decelerate()
        {
            if (movementSpeed > 0)
            {
                movementSpeed -= Mathf.Lerp(acceleration, maxAcceleration, 0f);
            }
            else if (movementSpeed < 0)
            {
                movementSpeed += Mathf.Lerp(acceleration, -maxAcceleration, 0f);
            }
            else
            {
                movementSpeed = 0;
            }
        }

        private void Move()
        {
            //Vector3 movement = transform.forward * movementSpeed * Time.deltaTime;
            //rb.MovePosition(rb.position + movement);

            /*float moveMod = Time.deltaTime * playerMoveMultiplier * playerSprintMultiplier;

            if(touchAxis.x == 0 && touchAxis.y == 0)
            {
                moveMod = 0;
                //return;
            }

            if (touchAxis.y > 0.0)
            {
                transform.Translate(transform.forward * moveMod, Space.World);
            }
            else if (touchAxis.y < 0.0)
            {
                transform.Translate(-transform.forward * moveMod, Space.World);
            }
            if (touchAxis.x < 0.0)
            {
                transform.Translate(-transform.right * moveMod, Space.World);
            }
            else if (touchAxis.x > 0.0)
            {
                transform.Translate(transform.right * moveMod, Space.World);
            }*/

            bool setBool = false;

            if (touchAxis.x == 0 && touchAxis.y == 0)
            {
                setBool = true;
            }

            if (VRFPS_NetworkController.instance.hasGO)
            {
                VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerMesh.GetComponent<Animator>().SetBool("returnToIdle", setBool);
                VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerMesh.GetComponent<Animator>().SetFloat("floatX", touchAxis.x);
                VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerMesh.GetComponent<Animator>().SetFloat("floatY", touchAxis.y);
            }
        }

        private void Turn()
        {
            /*float turn = touchAxis.x * rotationSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);*/
        }

        private void Jump()
        {
            if (!isJumping && triggerAxis > 0)
            {
                //float jumpHeight = (triggerAxis * jumpPower);
                //rb.AddRelativeForce(Vector3.up * jumpHeight);
                //triggerAxis = 0f;
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            isJumping = false;
        }

        private void OnTriggerExit(Collider collider)
        {
            isJumping = true;
        }
    }
}