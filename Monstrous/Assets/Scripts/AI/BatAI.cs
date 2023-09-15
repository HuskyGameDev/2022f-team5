using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class BatAI : EnemyBase{
        [Header("Bat AI")]
        [SerializeField] private int bobbingStrength = 10;
        [SerializeField] private int bobbingFrequency = 3;
        void FixedUpdate(){
            transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
            transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Sin(bobbingFrequency * Time.time) / bobbingStrength);
        }

        public override void onAttack(){}
        public override void onDeath(){}
    }
}