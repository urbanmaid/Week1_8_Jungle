using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LaserBossEnemy : BossEnemy
{

    [Header("Characteristics")]
    [SerializeField] private GameObject laserPrefab;
    private GameObject laser;

    protected override void Start()
    {
        base.Start();

    }
}
