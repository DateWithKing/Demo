using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizingDTO
{
    public string name { get; set; }
    public StatDataDTO stats { get; set; }

    public CustomizingDTO(string name)
    {
        this.name = name;
    }

    public CustomizingDTO()
    {
        name = "손서희";
        stats = new StatDataDTO(); 
    }
}

