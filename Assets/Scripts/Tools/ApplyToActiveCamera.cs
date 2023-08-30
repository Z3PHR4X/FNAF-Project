using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Tools
{
    public class ApplyToActiveCamera : MonoBehaviour
    {
        public Camera m_Camera;
        public VideoPlayer video;

        void Update()
        {
            m_Camera = Camera.current.GetComponent<Camera>();
            video.targetCamera = m_Camera;
        }
    }
}
