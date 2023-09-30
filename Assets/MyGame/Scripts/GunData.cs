using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]

public class GunData : ScriptableObject {
    [Header("Info")]
    public new string name;

    [Header("Ballistics")]
    public float damage;
    public float maxDistance;
    
    [Header("Handling")]
    public int magSize = 4;
    public int currentAmmo = 4;
    public float fireRate;
    public float reloadTime;
    public bool reloading;
}
