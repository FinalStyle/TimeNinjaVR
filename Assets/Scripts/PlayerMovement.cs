using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public WandController[] wands;
    public float minWalkMovementPerFrame;
    public float minJumpMovementPerFrame;

    public float jumpForce;
    public float walkForce;

    private Vector3[] LastFrameWandPosition = new Vector3[2];
    private Vector3[] WandMovementVector = new Vector3[2];

    private Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        for (int i = 0; i < wands.Length; i++)
        {
            var currentPos = wands[i].transform.localPosition;
            LastFrameWandPosition[i] = currentPos;
        }
    }

    private void Update()
    {

        if (OverThresholdWalk() && IsGesturingWalk())
        {
            Walk();
        }

        if (OverThresholdJump() && IsGesturingJump())
        {
            Jump();
        }

        UpdateWandsPositions();
    }

    private void Walk()
    {
        _rb.AddForce(new Vector3(0, jumpForce, 0));
    }

    private void Jump()
    {
        _rb.AddForce(new Vector3(jumpForce, 0, 0));
    }

    private bool OverThresholdWalk()
    {
        return WandMovementVector[0].magnitude > minWalkMovementPerFrame && WandMovementVector[1].magnitude > minWalkMovementPerFrame;
    }

    private bool OverThresholdJump()
    {
        return WandMovementVector[0].magnitude > minJumpMovementPerFrame && WandMovementVector[1].magnitude > minJumpMovementPerFrame;
    }

    private void UpdateWandsPositions()
    {
        for (int i = 0; i < wands.Length; i++)
        {
            var currentPos = wands[i].transform.localPosition;
            WandMovementVector[i] = currentPos - LastFrameWandPosition[i];

            LastFrameWandPosition[i] = currentPos;
        }
    }

    public bool IsGesturingWalk()
    {
        float firstDirection, secondDirection;

        firstDirection = WandMovementVector[0].normalized.y;
        secondDirection = WandMovementVector[1].normalized.y;

        return (Mathf.Sign(firstDirection) != Mathf.Sign(secondDirection));
    }

    public bool IsGesturingJump()
    {
        float firstDirection, secondDirection;

        firstDirection = WandMovementVector[0].normalized.y;
        secondDirection = WandMovementVector[1].normalized.y;

        return (Mathf.Sign(firstDirection) == Mathf.Sign(secondDirection));
    }
}
