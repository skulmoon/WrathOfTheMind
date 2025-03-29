public enum HorizontalSize
{
    Long,
    Normal,
    Short
}

static class HorizontalSizeExtension
{
    public static float AsMultiplier(this HorizontalSize size) 
    {
        return size switch
        {
            HorizontalSize.Short => 4,
            HorizontalSize.Normal => 6,
            HorizontalSize.Long => 25/2f,
            _ => 0,
        } ;
    }
}