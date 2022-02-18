using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WoodWorker : MonoBehaviour
{
    public GameObject WoodStack;
    private Vector3 stackPos; 
    public Transform dropZone;
    public Transform dropPoint;
    public Transform home;
    public List<GameObject> trees;
    private int teleports;
    private float speed = 10f;
    private Vector3 treePos;
    private bool killedWood;
    public int collectedWood = 0;
    private Vector3 newdirection;
    private Vector3 targetDir;
    public int energy;
    private bool drop;
    private void Start()
    {
        MoveToTree();
    }

    private void OnEnable()
    {
        trees = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tree"));
        energy = 4;
    }

    private void Update()
    {
        //speed
        float step = speed * Time.deltaTime;
        
        if (trees.Count > 0 && energy > 0)
        {
            //Move towards tree
            transform.position = 
                Vector3.MoveTowards(transform.position, treePos,step);
        
            //Rotate towards tree
            targetDir = treePos - transform.position;
            newdirection = 
                Vector3.RotateTowards
                    (transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newdirection);
        }
        
        //Chop one tree and move on to the next
        if (transform.position == treePos)
        {
            if (!killedWood)
            {
                ChopWood();
            }
            MoveToTree();
        }
        //When no trees left move to drop point
        if (trees.Count == 0 || energy == 0 && !drop)
        {
            //Rotate towrds drop point
            targetDir = dropZone.position - transform.position;
            newdirection = 
                Vector3.RotateTowards
                    (transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newdirection);
            //Move towards drop point
            transform.position = 
                Vector3.MoveTowards(transform.position, dropZone.position, step);
            if (transform.position == dropZone.position)
            {
                DropStack();
            }

            if (trees.Count < energy)
            {
                energy = 0;
            }
        }
        
        if (drop)
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, home.position, step);
        }
        if (transform.position == home.position)
        {
            drop = false;
            enabled = false;
        }
    }

    void DropStack()
    {
        if (collectedWood > 0)
        {
            WoodStack.GetComponent<Pickup>().pickupAmount = collectedWood;
            GameObject.Instantiate(WoodStack, dropPoint.position, WoodStack.transform.rotation);
            collectedWood = 0;
            drop = true;
        }
    }
    public void CollectWood(int amount)
    {
        collectedWood += amount;
    }
    private void MoveToTree()
    {
        
        foreach (GameObject tree in trees)
        {
            Vector3 vector = new Vector3(tree.transform.position.x, 0, tree.transform.position.z);
            treePos = vector;
            break;
        }
    }

    void ChopWood()
    {
        foreach (GameObject tree in trees)
        {
            int t = tree.GetComponent<Tree>().dropAmount;
            CollectWood(t);
            tree.GetComponent<Tree>().LoseHealth(50);
            energy--;
            killedWood = true;
            if (killedWood)
            {
                trees.Remove(tree);
                killedWood = false;
            }
            break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pickup>().dropType == dType.wood)
        {
            Destroy(other.gameObject);
        }
    }
}
