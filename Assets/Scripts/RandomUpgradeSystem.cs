using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintRandomElement : MonoBehaviour
{
    List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

    List<T> GetRandomElements<T>(List<T> inputList, int count)
    {
        List<T> available = new List<T>(inputList); // Copy origonal list
        List<T> outputList = new List<T>();

        count = Mathf.Min(count, available.Count);

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, available.Count);
            outputList.Add(available[index]);
            available.RemoveAt(index); // Prevent duplicates
        }
        return outputList;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var randomList = GetRandomElements(list, 3);

            Debug.Log("All elements => " + string.Join(", ", list));
            Debug.Log("Random elements => " + string.Join(", ", randomList));
            Debug.Log("*****************************");
        }
    }
}