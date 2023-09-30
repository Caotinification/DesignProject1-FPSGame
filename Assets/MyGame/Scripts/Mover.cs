using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class Mover : MonoBehaviour {
        [SerializeField] float moveSpeed;
        Vector3 initPos;
        float ampl = 0 ;
        private void Start() {
            initPos = transform.position;
            ampl = initPos.y;
        }

        void Update() {
            transform.position = initPos +
                new Vector3(Mathf.Cos(Time.time),Mathf.Sin(Time.time), Mathf.Cos(Time.time)) * (ampl * moveSpeed);
        }
    }
}