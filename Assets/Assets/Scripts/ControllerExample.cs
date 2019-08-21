using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class ControllerExample : TNBehaviour
{
    public float sendRate = 0.1f;
    private float sendRateReset;

    public float moveSpeed;

    private Vector2 lastInput;
    private Vector3 _nCurrentPos;

    void Start()
    {
        sendRateReset = sendRate;
    }

    void Update()
    {
        CheckIfMyObject();

        if (tno.isMine)
        {
            Move();
            CheckSendRate();
        }

    }

    private void CheckIfMyObject()
    {
        if (!tno.isMine)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _nCurrentPos, step);
        }
    }

    private void Move()
    {
        lastInput.y = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        lastInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(lastInput.x, 0, lastInput.y));
    }

    private void CheckSendRate()
    {
        sendRate -= Time.deltaTime;
        if (sendRate <= 0)
        {
            sendRate = sendRateReset;
            tno.SendQuickly(1, Target.OthersSaved, transform.position);
        }
    }

    [RFC(1)]
    public void NetworkPositionUpdate(Vector3 updatedPosition)
    {
        _nCurrentPos = updatedPosition;
    }
}
