//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using Valve.VR;

public struct ClickedEventArgs
{
	public uint controllerIndex;
	public uint flags;
	public float padX, padY;
}

public delegate void ClickedEventHandler(object sender, ClickedEventArgs e);

public class SteamVR_TrackedController : MonoBehaviour
{
	public uint controllerIndex;
	public VRControllerState_t controllerState;
	public bool triggerPressed = false;
	public bool steamPressed = false;
	public bool menuPressed = false;
	public bool padPressed = false;
	public bool padTouched = false;
	public bool gripped = false;

	public event ClickedEventHandler MenuButtonClicked;
	public event ClickedEventHandler MenuButtonUnclicked;
	public event ClickedEventHandler TriggerClicked;
	public event ClickedEventHandler TriggerUnclicked;
	public event ClickedEventHandler SteamClicked;
	public event ClickedEventHandler PadClicked;
	public event ClickedEventHandler PadUnclicked;
	public event ClickedEventHandler PadTouched;
	public event ClickedEventHandler PadUntouched;
	public event ClickedEventHandler Gripped;
	public event ClickedEventHandler Ungripped;

	// Use this for initialization
	protected virtual void Start()
	{
		if (this.GetComponent<SteamVR_TrackedObject>() == null)
		{
			gameObject.AddComponent<SteamVR_TrackedObject>();
		}

		if (controllerIndex != 0)
		{
			this.GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)controllerIndex;
			if (this.GetComponent<SteamVR_RenderModel>() != null)
			{
				this.GetComponent<SteamVR_RenderModel>().index = (SteamVR_TrackedObject.EIndex)controllerIndex;
			}
		}
		else
		{
			controllerIndex = (uint)this.GetComponent<SteamVR_TrackedObject>().index;
		}
	}

	public void SetDeviceIndex(int index)
	{
		this.controllerIndex = (uint)index;
	}

	public virtual void OnTriggerClicked(ClickedEventArgs e)
	{
        TriggerClicked?.Invoke(this, e);
    }

	public virtual void OnTriggerUnclicked(ClickedEventArgs e)
	{
        TriggerUnclicked?.Invoke(this, e);
    }

	public virtual void OnMenuClicked(ClickedEventArgs e)
	{
        MenuButtonClicked?.Invoke(this, e);
    }

	public virtual void OnMenuUnclicked(ClickedEventArgs e)
	{
        MenuButtonUnclicked?.Invoke(this, e);
    }

	public virtual void OnSteamClicked(ClickedEventArgs e)
	{
        SteamClicked?.Invoke(this, e);
    }

	public virtual void OnPadClicked(ClickedEventArgs e)
	{
        PadClicked?.Invoke(this, e);
    }

	public virtual void OnPadUnclicked(ClickedEventArgs e)
	{
        PadUnclicked?.Invoke(this, e);
    }

	public virtual void OnPadTouched(ClickedEventArgs e)
	{
        PadTouched?.Invoke(this, e);
    }

	public virtual void OnPadUntouched(ClickedEventArgs e)
	{
        PadUntouched?.Invoke(this, e);
    }

	public virtual void OnGripped(ClickedEventArgs e)
	{
        Gripped?.Invoke(this, e);
    }

	public virtual void OnUngripped(ClickedEventArgs e)
	{
        Ungripped?.Invoke(this, e);
    }

	// Update is called once per frame
	protected virtual void Update()
	{
		var system = OpenVR.System;
		if (system != null && system.GetControllerState(controllerIndex, ref controllerState, (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(VRControllerState_t))))
		{
			ulong trigger = controllerState.ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_SteamVR_Trigger));
			if (trigger > 0L && !triggerPressed)
			{
				triggerPressed = true;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnTriggerClicked(e);

			}
			else if (trigger == 0L && triggerPressed)
			{
				triggerPressed = false;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnTriggerUnclicked(e);
			}

			ulong grip = controllerState.ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_Grip));
			if (grip > 0L && !gripped)
			{
				gripped = true;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnGripped(e);

			}
			else if (grip == 0L && gripped)
			{
				gripped = false;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnUngripped(e);
			}

			ulong pad = controllerState.ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_SteamVR_Touchpad));
			if (pad > 0L && !padPressed)
			{
				padPressed = true;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnPadClicked(e);
			}
			else if (pad == 0L && padPressed)
			{
				padPressed = false;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnPadUnclicked(e);
			}

			ulong menu = controllerState.ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_ApplicationMenu));
			if (menu > 0L && !menuPressed)
			{
				menuPressed = true;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnMenuClicked(e);
			}
			else if (menu == 0L && menuPressed)
			{
				menuPressed = false;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnMenuUnclicked(e);
			}

			pad = controllerState.ulButtonTouched & (1UL << ((int)EVRButtonId.k_EButton_SteamVR_Touchpad));
			if (pad > 0L && !padTouched)
			{
				padTouched = true;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnPadTouched(e);

			}
			else if (pad == 0L && padTouched)
			{
				padTouched = false;
				ClickedEventArgs e;
				e.controllerIndex = controllerIndex;
				e.flags = (uint)controllerState.ulButtonPressed;
				e.padX = controllerState.rAxis0.x;
				e.padY = controllerState.rAxis0.y;
				OnPadUntouched(e);
			}
		}
	}
}
