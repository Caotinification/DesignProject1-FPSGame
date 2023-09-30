using System;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {
    
    public static Action shootInput;
    public static Action aimInput;
    public static Action reloadInput;


    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            shootInput?.Invoke();
        }

        if (Input.GetMouseButtonDown(1)) {
            aimInput?.Invoke();
        }

        if (Input.GetMouseButtonUp(1)) {
            aimInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey)) {
            reloadInput?.Invoke();
        }

    }
}
