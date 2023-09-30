using System;
using System.Collections;
using UnityEngine;

namespace MyGame {
    public class BeamRifle : MonoBehaviour {
        [Header("References")]
        [SerializeField] private GunData gunData;
        [SerializeField] private Transform muzzle;

        [SerializeField] public ParticleSystem muzzleFX;

        [SerializeField] public AudioClip gunshotSound;
        [SerializeField] public AudioClip reloadSound;
        [SerializeField] public LineRenderer beam;


        float timeSinceLastShot;

        private bool isAiming = false;
        private Vector3 initialPos;
        private Quaternion initialRot;

        private void Awake() {
            beam.enabled = false;
        }

        private void Start() { // event subscriptions
            
            initialPos = transform.localPosition;
            initialRot = transform.localRotation;

            gunData.currentAmmo = gunData.magSize;
            PlayerInputs.shootInput += Shoot;
            PlayerInputs.reloadInput += StartReload;
            PlayerInputs.aimInput += Aim;
        }

        void OnDestroy() {
            PlayerInputs.shootInput -= Shoot;
            PlayerInputs.reloadInput -= StartReload;
            PlayerInputs.aimInput -= Aim;
        }

        private void Aim() {
            isAiming = !isAiming;
            if (isAiming) {
                transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            } else {
                transform.SetLocalPositionAndRotation(initialPos, initialRot);
            }
        }

        public void StartReload() {
            if (gunData.reloading) { return; }
            StartCoroutine(Reload());
        }

        public IEnumerator Reload() {
            gunData.reloading = true;

            AudioSource.PlayClipAtPoint(reloadSound, transform.position, 0.2f);
            yield return new WaitForSeconds(gunData.reloadTime);

            gunData.currentAmmo = gunData.magSize;
            gunData.reloading = false;
        }


        //arent reloading, and within the fire rate
        public bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

        float alpha = 0;
        void AddRecoil() {
            transform.localRotation = 
                Quaternion.Slerp(initialRot, Quaternion.Euler(-30, 0, 0), alpha);
            alpha = Mathf.Clamp(alpha + 1 / gunData.magSize, 0, 1);
        }

        public void Shoot() {
            if (gunData.currentAmmo <= 0) { return; }
            if (!CanShoot()) { return; }

            beam.enabled = true;
            AudioSource.PlayClipAtPoint(gunshotSound, muzzle.position);

            muzzleFX.Play();
            bool wasHit = Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance);
            Vector3 hitPosition = wasHit ? hitInfo.point : muzzle.position + muzzle.forward * gunData.maxDistance;
            if (wasHit) {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                damageable?.Damage(gunData.damage);
            }
            beam.SetPosition(0, muzzle.position + muzzle.forward * (1));
            beam.SetPosition(1, hitPosition - (Vector3.up * 2));

            alpha += 1f / 3f;
            AddRecoil();
            gunData.currentAmmo--;
            timeSinceLastShot = 0;
            transform.localRotation = initialRot;
        }

        private void Update() {
            alpha = Mathf.Clamp(alpha - Time.deltaTime, 0f, 1);
            Debug.DrawRay(muzzle.position, muzzle.forward, Color.blue);
            AddRecoil();
            timeSinceLastShot += Time.deltaTime;
        }
    }
}