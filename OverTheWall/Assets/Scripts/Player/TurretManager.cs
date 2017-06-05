﻿using OverTheWall.Enums;
using OverTheWall.SharedFunctions;
using OverTheWall.TurretBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    public TurrentBase arrowTurret;
    public TurrentBase catapultTurret;
    public TurrentBase repeaterCrossbowTurret;
    public TurrentBase archersTurret;

    private TurrentBase currentTurret;
    private TurretType currentTurretType;
    private int currentTurretTypeId;
    private bool canAttack = true;
    private bool canSwitchTurrets = true;
    private Vector2 startDragPosition = Vector2.zero;
    private Vector2 endDragPosition = Vector2.zero;
    private float currentTimeBetweenDrag = 0;
    private float totalTimeBetweenDrag = 0;
    private float angle = 0;
    private bool dragging = false;
    private bool dragStartedInSwitchSpace = false;
    private Rect TurretSwitchArea;

    // Use this for initialization
    void Start()
    {
        SelectCurrentTurret(TurretType.BigBow);
    }

    // Update is called once per frame

    void Update()
    {
        if (HasInputStarted())
        {
            if (InTurretSwitchSpace() || dragStartedInSwitchSpace)
            {
                if (UsedAttackInput() && canSwitchTurrets)
                {
                    SwitchTurret();
                }
            }
            else
            {
                if (UsedAttackInput() && canAttack)
                {
                    Attack();
                }
            }
        }

        if (dragging)
        {
            currentTimeBetweenDrag += Time.deltaTime;
        }
    }

    bool UsedAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && dragging)
        {
            endDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = false;

            totalTimeBetweenDrag = currentTimeBetweenDrag;
            currentTimeBetweenDrag = 0;
            angle = Vector2.Angle(endDragPosition, startDragPosition);

            return true;
        }

        return false;
    }

    bool HasInputStarted()
    {
        if (Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonUp(0))
        {
            return true;
        }

        return false;
    }

    bool InTurretSwitchSpace()
    {
        Vector2 pointToCheck;

        if (Input.GetMouseButtonDown(0))
        {
            pointToCheck = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            return false;
        }

        if (TurretSwitchArea.Contains(pointToCheck))
        {
            return true;
        }

        return false;
    }

    void Attack()
    {
        float speed = SharedFunctions.CalculateSpeed(startDragPosition, endDragPosition, totalTimeBetweenDrag);

        totalTimeBetweenDrag = 0;

        currentTurret.AddProjectile(endDragPosition, angle, speed);

        StartCoroutine(AttackRoutine());
    }

    void SwitchTurret()
    {
        currentTurretTypeId++;
        if (currentTurretTypeId > 3)
            currentTurretTypeId = 0;

        SelectCurrentTurret((TurretType)currentTurretTypeId);
    }

    void SelectCurrentTurret(TurretType turretType)
    {
        currentTurretType = turretType;
        currentTurretTypeId = (int)turretType;

        switch (turretType)
        {
            case TurretType.BigBow:
                currentTurret = arrowTurret;
                break;
            case TurretType.Catapult:
                currentTurret = catapultTurret;
                break;
            case TurretType.RepeaterCrossbow:
                currentTurret = repeaterCrossbowTurret;
                break;
            case TurretType.ArcherGroup:
                currentTurret = archersTurret;
                break;
            default:
                break;
        }
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(currentTurret.GetCooldown());

        canAttack = true;
    }
}