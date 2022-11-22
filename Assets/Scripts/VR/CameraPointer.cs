//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    private RaycastHit hit;

    public void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, LayerMask.GetMask("Head")))
            InitRaycast();
        else if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, LayerMask.GetMask("Body")))
            InitRaycast();
        else if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, LayerMask.GetMask("Arm")))
            InitRaycast();
        else if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, LayerMask.GetMask("Leg")))
            InitRaycast();
        else if(Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance,LayerMask.GetMask("UI")))
            InitRaycast();
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit");
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }
    private void InitRaycast()
    {
        if (_gazedAtObject != hit.transform.gameObject)
        {
            _gazedAtObject?.SendMessage("OnPointerExit");
            _gazedAtObject = hit.transform.gameObject;
            _gazedAtObject.SendMessage("OnPointerEnter");
        }
    }
}
