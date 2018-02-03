using UnityEngine;
using System.Collections;
using System;

public class WandController : SteamVR_TrackedController
{

    public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)controllerIndex); } }
    public Vector3 velocity { get { return controller.velocity; } }
    public Vector3 angularVelocity { get { return controller.angularVelocity; } }

    public float GetTriggerAxis()
    {
        if (controller == null)
            return 0;
            
        return controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;
    }

    public Vector2 GetTouchpadAxis()
    {
        if (controller == null)
            return new Vector2();

        return controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
    }

    public void Pulse(ushort microseconds)
    {
        SteamVR_Controller.Input((int)controllerIndex).TriggerHapticPulse(microseconds);
    }

    public void Vibrate(ushort force, float time, int times)
    {
        StopAllCoroutines();
        StartCoroutine(VibrateTimes(force, time, times));
    }

    IEnumerator VibrateTimes(ushort force, float time, int times)
    {
        int t = 0;
        while (t < times)
        {
            StartCoroutine(StartVibrating(force, time));
            t+=1;
            yield return new WaitForSeconds(time+0.2f);
        }
    }

    IEnumerator StartVibrating(ushort force, float time)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            Pulse(force);
            yield return new WaitForFixedUpdate();
        }

    }
}