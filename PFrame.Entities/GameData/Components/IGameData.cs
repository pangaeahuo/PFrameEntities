﻿using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace PFrame.Entities
{
    public interface IGameData
    {
        ushort DataId { get; }
        byte DataType { get; }
        string DataName { get; }
    }

    //public struct GameData : IGameData
    //{
    //    public ushort Id;
    //    public byte Type;
    //    public NativeString32 Name;
    //    public ushort DataId => Id;
    //    public byte DataType => Type;
    //    public string DataName => Name.ToString();
    //}

    //public class GameDataManager
    //{
    //    //private static Dictionary<int, IGameData>[] itemDicts;
    //    //private static NativeHashMap<int, IGameData>[] itemMaps;
    //    private static List<IGameData>[] itemLists;

    //    public static void Initialize(int typeNum)
    //    {
    //        //itemDicts = new Dictionary<int, IGameData>[typeNum];
    //        itemLists = new List<IGameData>[typeNum];
    //    }

    //    public static void Release()
    //    {

    //    }

    //    public static void AddGameData<T>(byte typeId, DynamicBuffer<T> buffer)
    //        where T : struct, IBufferElementData, IGameData
    //    {
    //        //var itemDict = new Dictionary<int, IGameData>();
    //        var itemList = new List<IGameData>();
    //        foreach (T item in buffer)
    //        {
    //            //itemDict.Add(item.DataId, item);
    //            itemList.Add(item);
    //        }

    //        //itemDicts[typeId] = itemDict;
    //        itemLists[typeId] = itemList;
    //    }

    //    public static T GetGameData<T>(byte type, ushort id)
    //        where T : IGameData
    //    {
    //        //var dict = itemDicts[type];
    //        //if (dict == null)
    //        //    return default;

    //        //dict.TryGetValue(id, out var item);
    //        var list = itemLists[type];
    //        if (list == null)
    //            return default;

    //        T item = default;
    //        for(int i = 0; i < list.Count; i++)
    //        {
    //            item = (T)list[i];
    //            if (item.DataId == id)
    //                break;
    //        }
    //        return (T)item;
    //    }

    //    public static List<IGameData> GetGameDataList(byte type)
    //    {
    //        return itemLists[type];
    //    }
    //}
}