using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PrizeType { COINS,GEMS,SHELLS,WEAPON}
[SerializeField]
public class PrizesListScriptableObject : ScriptableObject {

    [System.Serializable]
    public class PrizeListClass
    {
        public string nameId;
        public PrizeType categoryType;
        public string name;
        public string description;
        public Sprite itemSprite;
        public int itemValue;
        public int prizePercentaje;

    }

    [Header(" Coins Items List")]
    public List<PrizeListClass> coinsItemsList = new List<PrizeListClass>();
    [Header(" Gems Items List")]
    public List<PrizeListClass> gemsItemsList = new List<PrizeListClass>();
    [Header(" Tokens Items List")]
    public List<PrizeListClass> tokensItemsList = new List<PrizeListClass>();
    [Header(" Weapons Items List")]
    public List<PrizeListClass> weaponsItemsList = new List<PrizeListClass>();


}
