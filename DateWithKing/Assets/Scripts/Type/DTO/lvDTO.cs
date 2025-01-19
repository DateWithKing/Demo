public class lvDTO
{
    public int lvSan;
    public int lvHyun;
    public int lvPyo;
    
    public static lvDTO operator +(lvDTO a, lvDTO b)
    {
        return new lvDTO
        {
            lvSan = a.lvSan + b.lvSan,
            lvHyun = a.lvHyun + b.lvHyun,
            lvPyo = a.lvPyo + b.lvPyo
        };
    }
}
