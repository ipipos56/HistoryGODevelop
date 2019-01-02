using UnityEngine;
using System.Collections;

public class GyroSet : MonoBehaviour {
    private Gyroscope gyro;
    private bool GyroSupp;
    private Quaternion rotFix;

    [SerializeField]
    private Transform worldObj;
    private float startY;

	void Start () {
        GyroSupp = SystemInfo.supportsGyroscope;

        GameObject camParent = new GameObject("camParent");
        camParent.transform.position = transform.position;
        transform.parent = camParent.transform;

        if (GyroSupp)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            camParent.transform.rotation = Quaternion.Euler(90f, 100f, 0f);
            rotFix = new Quaternion(0, 0, 1, 0);
        }
	}

	void Update () {
        if (GyroSupp && startY == 0)
        {
            ResetGyroRotation();
        }

        transform.localRotation = gyro.attitude * rotFix;
	}

    void ResetGyroRotation() {
        startY = transform.eulerAngles.y;
        worldObj.rotation = Quaternion.Euler(0f, startY, 0f);
    }
}
