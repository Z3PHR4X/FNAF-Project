using UnityEngine;

namespace Tools
{
    public class LookAtCurrentCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationOffset = new Vector3(0, 90f, 0);
        [SerializeField] private GameObject referencePoint;
        [SerializeField] private GameObject headObject;
        public bool lookAtCamera = false;

        private void Update()
        {
            //if (GameManagerV2.Instance.currentCam != null)
            //{
            //    LookAtObj(GameManagerV2.Instance.currentCam.transform, referencePoint.transform, lookAtCamera);
            //}
            //else
            //{
           //     print("no camera found: " + Camera.main);
            //}
        }

        private void LookAtObj(Transform target, Transform reference, bool enable)
        {
            float dist = Vector3.Distance(reference.position, target.position);
            Quaternion lookRot;
            Vector3 targetDir = new Vector3(0,0,0);

            if (dist <= 30 && lookAtCamera)
            {
                targetDir = target.position - reference.position;
                lookRot = Quaternion.LookRotation(targetDir);
                Quaternion lookOffset = Quaternion.AngleAxis(rotationOffset.x, Vector3.right);
                lookRot *= lookOffset;
                lookOffset = Quaternion.AngleAxis(rotationOffset.y, Vector3.up);
                lookRot *= lookOffset;
                lookOffset = Quaternion.AngleAxis(rotationOffset.z, Vector3.forward);
                lookRot *= lookOffset;
            }
            else
            {
                targetDir = new Vector3(0,0,0);
                lookRot = Quaternion.LookRotation(targetDir);
                Quaternion lookOffset = Quaternion.AngleAxis(rotationOffset.x, Vector3.right);
                lookRot *= lookOffset;
                lookOffset = Quaternion.AngleAxis(-rotationOffset.y, Vector3.up);
                lookRot *= lookOffset;
                lookOffset = Quaternion.AngleAxis(rotationOffset.z, Vector3.forward);
                lookRot *= lookOffset;
            }
            headObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(0.8f * Time.maximumDeltaTime));
        }
    }
}