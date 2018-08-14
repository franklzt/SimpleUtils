using System.Collections.Generic;

public class DogItem
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string breed { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int count { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string twoFeet { get; set; }
}

public class Cat
{
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
}

public class Animals
{
    /// <summary>
    /// 
    /// </summary>
    public List<DogItem> dog { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Cat cat { get; set; }
}

public class AnimalsJsonRoot
{
    /// <summary>
    /// 
    /// </summary>
    public Animals animals { get; set; }
}