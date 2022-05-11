//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Collider dangling from the player's head
//
//=============================================================================

using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class BodyCollider : MonoBehaviour
    {
        [SerializeField] private Transform _head;

        private void FixedUpdate()
        {
            transform.localRotation = _head.localRotation;
        }
    }
}