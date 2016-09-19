using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;


[System.Serializable]
public struct Item {
  public string itemEnum;
  public string itemName;
  public string itemPluralName;
  public PBPockets itemBagPocketType;
  public int itemPrice;
  public string itemDesc;
  public string itemInFieldUseMethod;
  public ItemUsageInField itemUsageTypeInField;
  public string itemInBattleUseMethod;
  public ItemUsageDuringBattle itemUsageTypeInBattle;
  public ItemSpecialTypes itemSpecialType;
  public string itemMachineMove;

  public Item(string p_internalName, string p_name, string p_pluralName, PBPockets p_curItemBagPocketType,
              int p_curItemPrice, string p_curItemDesc, string p_curItemInFieldUseMethod,
              ItemUsageInField p_curItemUsageTypeInField, string p_curItemInBattleUseMethod,
              ItemUsageDuringBattle p_curItemUsageTypeInBattle, ItemSpecialTypes p_curItemSpecialType,
              string p_curItemMachineMove) {
    itemEnum = p_internalName;
    itemName = p_name;
    itemPluralName = p_pluralName;
    itemBagPocketType = p_curItemBagPocketType;
    itemPrice = p_curItemPrice;
    itemDesc = p_curItemDesc;
    itemInFieldUseMethod = p_curItemInFieldUseMethod;
    itemUsageTypeInField = p_curItemUsageTypeInField;
    itemInBattleUseMethod = p_curItemInBattleUseMethod;
    itemUsageTypeInBattle = p_curItemUsageTypeInBattle;
    itemSpecialType = p_curItemSpecialType;
    itemMachineMove = p_curItemMachineMove;
  }

}


public class ItemManager : MonoBehaviour {

  public static List<Item> itemList = new List<Item>();


  public static void addItem(string p_internalName, string p_name, string p_pluralName, PBPockets p_curItemBagPocketType,
              int p_curItemPrice, string p_curItemDesc, string p_curItemInFieldUseMethod,
              ItemUsageInField p_curItemUsageTypeInField, string p_curItemInBattleUseMethod,
              ItemUsageDuringBattle p_curItemUsageTypeInBattle, ItemSpecialTypes p_curItemSpecialType,
              string p_curItemMachineMove)  {
    itemList.Add(new Item(p_internalName, p_name, p_pluralName, p_curItemBagPocketType,
                p_curItemPrice, p_curItemDesc, p_curItemInFieldUseMethod,
                p_curItemUsageTypeInField, p_curItemInBattleUseMethod,
                p_curItemUsageTypeInBattle, p_curItemSpecialType,
                p_curItemMachineMove)) ;
  }

  public static void clearList() {
    itemList.Clear();
  }

  public static void printEachItemName() {
    foreach(Item item in itemList) {
      Debug.Log(item.itemEnum);
    }
  }

  public static int getNumItems() {
    return itemList.Count;
  }

  public static void saveDataFile() {
    using (Stream stream = File.Open("Assets/Resources/Data/Items", FileMode.Create))
    {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        List<Item> newItemList = new List<Item>();
        string tempName;
        string tempPluralName;
        string tempItemDesc;


        foreach(Item item in itemList) {
          tempName = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(item.itemName));
          tempPluralName = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(item.itemPluralName));
          tempItemDesc = System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(item.itemDesc));


          newItemList.Add(new Item(item.itemEnum, tempName, tempPluralName, item.itemBagPocketType,
                          item.itemPrice, tempItemDesc, item.itemInFieldUseMethod, item.itemUsageTypeInField,
                          item.itemInBattleUseMethod, item.itemUsageTypeInBattle, item.itemSpecialType,
                          item.itemMachineMove));
        }

        binaryFormatter.Serialize(stream, newItemList);
    }
  }

  public static void loadDataFile() {
    List<Item> tempItemList;
    itemList.Clear();
    using (Stream stream = File.Open("Assets/Resources/Data/Items", FileMode.Open))
    {
        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        tempItemList = (List<Item>)binaryFormatter.Deserialize(stream);
    }

    string tempName;
    string tempPluralName;
    string tempItemDesc;

    foreach(Item item in tempItemList) {
      tempName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(item.itemName));
      tempPluralName = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(item.itemPluralName));
      tempItemDesc = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(item.itemDesc));

      itemList.Add(new Item(item.itemEnum, tempName, tempPluralName, item.itemBagPocketType,
                      item.itemPrice, tempItemDesc, item.itemInFieldUseMethod, item.itemUsageTypeInField,
                      item.itemInBattleUseMethod, item.itemUsageTypeInBattle, item.itemSpecialType,
                      item.itemMachineMove));
    }
  }

}
