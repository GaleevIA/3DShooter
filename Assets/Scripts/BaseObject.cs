using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Базовый класс для всех объектов на сцене, кэширующий ссылки
/// </summary>
public class BaseObject : MonoBehaviour
{
    protected Transform gameObjectTransform;
    protected GameObject gameObjectInstance;
    protected String gameObjectName;
    protected Boolean isVisible;

    protected Vector3 position;
    protected Vector3 scale;
    protected Quaternion rotation;

    protected Material material;
    protected Color color;

    protected Rigidbody rigidBody;

    protected Camera mainCamera;

    protected Animator animator;
    protected virtual void Awake()
    {
        gameObjectInstance = gameObject;
        gameObjectTransform = gameObject.transform;
        gameObjectName = gameObject.name;

        mainCamera = Camera.main;

        if (GetComponent<Rigidbody>())
            rigidBody = GetComponent<Rigidbody>();

        if (GetComponent<Animator>())
            animator = GetComponent<Animator>();

        if (GetComponent<Renderer>())
            material = GetComponent<Renderer>().material;
    }

    public GameObject InstanceObject
    {
        get { return gameObjectInstance; }
    }
    public String Name
    {
        get { return name; }
        set
        {
            name = value;
            gameObjectInstance.name = name;
        }
    }
    public Boolean IsVisible
    {
        get { return isVisible; }
        set
        {
            isVisible = value;

            if (gameObjectInstance.GetComponent<Renderer>())
                gameObjectInstance.GetComponent<Renderer>().enabled = isVisible;
        }
    }
    public Vector3 Position
    {
        get
        {
            if (gameObjectTransform)
                position = gameObjectTransform.position;

            return position;
        }
        set
        {
            position = value;

            if (gameObjectTransform)
                gameObjectTransform.position = position;
        }
    }
    public Vector3 Scale
    {
        get
        {
            if (gameObjectTransform)
                scale = gameObjectTransform.localScale;

            return scale;
        }
        set
        {
            scale = value;

            if (gameObjectTransform)
                gameObjectTransform.localScale = scale;
        }
    }
    public Quaternion Rotation
    {
        get
        {
            if (gameObjectTransform)
                rotation = gameObjectTransform.rotation;

            return rotation;
        }
        set
        {
            rotation = value;

            if (gameObjectTransform)
                gameObjectTransform.rotation = rotation;
        }
    }
    public Material GetMaterial
    {
        get { return material; }
    }
    public Rigidbody GetRigidBody
    {
        get { return rigidBody; }
    }
    public Animator GetAnimator
    {
        get { return animator; }
    }
    public Camera MainCamera
    {
        get { return mainCamera; }
    }
    public Int32 ChildCount
    {
        get { return gameObjectTransform.childCount; }
    }
}
