using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    [TextArea(minLines: 2, maxLines: 50)]
    [SerializeField] public string text;
}
