public static class Common
{
     public static CampType GetBaseObjectCamp(this BaseObject baseObject)
     {
          if (baseObject is Solider)
          {
               var slider = baseObject as Solider;
               return slider.CampType;
          }
          else if(baseObject is Town)
          {
               var town = baseObject as Town;
               return town.Camp();
          }
          return CampType.None;
     }
}