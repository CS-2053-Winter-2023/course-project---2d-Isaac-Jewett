using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryController : MonoBehaviour
{

    public GameObject entry1;
    public GameObject entry2;
    public GameObject entry3;
    public GameObject entry4;
    public GameObject entry5;
    public GameObject entry6;
    public GameObject entry7;
    public GameObject entry8;

    public enum CurrentEntry { 
    
        entry1,
        entry2,
        entry3,
        entry4,
        entry5,
        entry6,
        entry7,
        entry8
    
    }

    public CurrentEntry currentEntry;

    // Start is called before the first frame update
    void Start()
    {
        entry1.SetActive(false);
        entry2.SetActive(false);
        entry3.SetActive(false);
        entry4.SetActive(false);
        entry5.SetActive(false);
        entry6.SetActive(false);
        entry7.SetActive(false);
        entry8.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        if (currentEntry == CurrentEntry.entry1)
        {
            entry1.SetActive(true);

        } else if (currentEntry == CurrentEntry.entry2) 
        {
            entry2.SetActive(true);

        }
        else if (currentEntry == CurrentEntry.entry3)
        {
            entry3.SetActive(true);

        }
        else if (currentEntry == CurrentEntry.entry4)
        {
            entry4.SetActive(true);

        }
        else if (currentEntry == CurrentEntry.entry5)
        {
            entry5.SetActive(true);

        }
        else if (currentEntry == CurrentEntry.entry6)
        {
            entry6.SetActive(true);

        }
        else if (currentEntry == CurrentEntry.entry7)
        {
            entry7.SetActive(true);

        }
        else if (currentEntry == CurrentEntry.entry8)
        {
            entry8.SetActive(true);

        }


    }
}
