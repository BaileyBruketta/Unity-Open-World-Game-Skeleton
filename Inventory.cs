﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] Itemslots[] itemSlots;
    

    public event Action<Item> OnItemLeftClickedEvent;




    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnLeftClickEvent += OnItemLeftClickedEvent;
        }

        //RefreshUI();
    }

    public bool AddItem(Item item) //as bool to see if it could be added
    {

        if (IsFull())
            return false;

        items.Add(item);//adds item to item list

        RefreshUI();
        return true;
    }


    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;

        }
        return false;
    }
    //tells us if the inventory is full
    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
        //inventory amount is equal or less than total itemslots 
    }
    //refreshes ui and 
    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<Itemslots>();
        }
        RefreshUI();
    }

    //refreshes and ties itemslots to items list
    private void RefreshUI()
    {
        int i = 0;

        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }


}
