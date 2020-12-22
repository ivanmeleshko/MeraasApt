using System.Globalization;
using UnityEngine;
using System;


public class Flat : MonoBehaviour
{

    public string unitId;
    public int price;
    public int surface;
    public int bathroomCount;
    public int bedroomCount;
    public bool maidsroom;
    public Availability availability;
    public FlatType fType;

    public enum Availability { Available, Reserved, Sold   }
    public enum FlatType    { OneRoom, DoubleRoom, DoubleBedroom }


    public Flat() { }


    public Flat(string _unitId, int _price, int _surface, int _bathroomCount, int _bedroomCount, bool _maidsroom, Availability _availability, FlatType _flatType)
    {
        unitId = _unitId;
        price = _price;
        surface = _surface;
        bathroomCount = _bathroomCount;
        bedroomCount = _bedroomCount;
        maidsroom = _maidsroom;
        availability = _availability;
        fType = _flatType;
    }


    public Flat(Flat flat)
    {
        unitId = flat.unitId;
        price = flat.price;
        surface = flat.surface;
        bathroomCount = flat.bathroomCount;
        bedroomCount = flat.bedroomCount;
        maidsroom = flat.maidsroom;
        availability = flat.availability;
        fType = flat.fType;
    }


    public static string VillaInfo(Flat flat)
    {
        string N = Environment.NewLine;
        return flat.fType.ToString() + N +
               "<b>" + flat.unitId + "</b>" + N +
               flat.bedroomCount + N +
               flat.surface + N +
               "<b>" + flat.price.ToString("0,0", CultureInfo.InvariantCulture) + "</b>";
    }


    public static Material ChangeMaterial(Availability a)
    {
        switch (a)
        {
            case Availability.Available:
                return Resources.Load("WallAvailableMat", typeof(Material)) as Material;
            case Availability.Reserved:
                return Resources.Load("WallReservedMat", typeof(Material)) as Material;
            case Availability.Sold:
                return Resources.Load("WallSoldMat", typeof(Material)) as Material;
        }
        return null;
    }
}
