using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayLink : MonoBehaviour
{

    void Start()
    {
        DisplayVillas();
    }


    private void Update()
    {
        if (Settings.instance.eventChanged)
        {
            Settings.instance.eventChanged = false;

            if (Settings.instance.displayedFlats != null)
                Settings.instance.displayedFlats.Clear();

            Settings.instance.displayedFlats = new List<Flat>();

            foreach (Flat flat in Settings.instance.flats)
            {
                if (Settings.instance.available)
                {
                    if (flat.availability == Flat.Availability.Available)
                        Settings.instance.displayedFlats.Add(flat);
                }
                if (Settings.instance.reserved)
                {
                    if (flat.availability == Flat.Availability.Reserved)
                        Settings.instance.displayedFlats.Add(flat);
                }
                if (Settings.instance.sold)
                {
                    if (flat.availability == Flat.Availability.Sold)
                        Settings.instance.displayedFlats.Add(flat);
                }
            }

            List<Flat> tempList = new List<Flat>(Settings.instance.displayedFlats);

            foreach (Flat flat in tempList)
            {
                if (!Settings.instance.townHouse)
                {
                    if (flat.fType == Flat.FlatType.DoubleRoom)
                        Settings.instance.displayedFlats.Remove(flat);
                }
                if (!Settings.instance.twinhome)
                {
                    if (flat.fType == Flat.FlatType.DoubleBedroom)
                        Settings.instance.displayedFlats.Remove(flat);
                }
            }

            tempList = new List<Flat>(Settings.instance.displayedFlats);

            foreach (Flat flat in tempList)
            {
                if (flat.price < Settings.instance.minPrice || flat.price > Settings.instance.maxPrice ||
                    flat.surface < Settings.instance.minSurface / 12 || flat.surface > Settings.instance.maxSurface / 12)
                {
                    Settings.instance.displayedFlats.Remove(flat);
                }
            }

            UpdateLinks();
        }            
    }


    public void DisplayVillas()
    {
        if (Settings.instance.displayedFlats == null)
        {
            Settings.instance.displayedFlats = new List<Flat>();
        }

        foreach (Flat flat in Settings.instance.flats)
        {
            if (flat.unitId.Equals(gameObject.name))
            {
                if (flat.availability == Flat.Availability.Available 
                       && flat.fType  == Flat.FlatType.DoubleRoom 
                       && flat.price   < BudgetSliderMax.Value * 1000000 && flat.price > BudgetSliderMin.Value * 1000000
                       && flat.surface < SquareSliderMax.Value /12 && flat.surface > SquareSliderMin.Value / 12)
                {
                    Settings.instance.displayedFlats.Add(flat);
                    Image link = gameObject.GetComponentInChildren<Image>();
                    link.gameObject.SetActive(true);
                }
                else
                {
                    //Settings.displayedVillas.Remove(villa);
                    Image link = gameObject.GetComponentInChildren<Image>();
                    link.gameObject.SetActive(false);
                }
            }
        }
    }


    public void UpdateLinks()
    {
        DisableLinks();

        foreach (Flat flat in Settings.instance.displayedFlats)
        {
            //GameObject go = GameObject.Find(flat.name);
            //Image link = go.GetComponentInChildren<Image>(true);
            //link.gameObject.SetActive(true);
        }
    }


    private void DisableLinks()
    {
        foreach (Flat flat in Settings.instance.flats)
        {
            //GameObject go = GameObject.Find(flat.name);
            //Image link = go.GetComponentInChildren<Image>(true);
            //link.gameObject.SetActive(false);
        }
    }

}
