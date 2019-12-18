using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Weapon : MonoBehaviour
{
    private SteamVR_TrackedController controller1;
    private SDK_InputSimulator controllerSimulator;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject muzzleflashPrefab;
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform muzzlePoint;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

#if UNITY_EDITOR || UNITY_EDITOR_WIN
        controllerSimulator = GetComponentInParent<SDK_InputSimulator>();
#else
        controller1 = GetComponentInParent<SteamVR_TrackedController>();
        controller1.TriggerClicked += FireWeapon;
#endif

    }

    private void FireWeapon(object sender, ClickedEventArgs e)
    {
        StartCoroutine(FireWeaponSimulator());
    }

    IEnumerator FireWeaponSimulator()
    {
        audioSource.Play();

        var muzzleFlash = Instantiate(muzzleflashPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Destroy(muzzleFlash.gameObject, .5f);

        var bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Destroy(bullet.gameObject, 2.0f);
        bullet.transform.parent = null;

        yield return new WaitForSeconds(0.05f); 
        bullet.gameObject.GetComponent<Rigidbody>().AddForce(muzzlePoint.forward * 40, ForceMode.Impulse);
    }

    void Update()
    {
        if (Input.GetKeyDown(controllerSimulator.buttonOneAlias))
        {
            StartCoroutine(FireWeaponSimulator());
        }
    }
}
