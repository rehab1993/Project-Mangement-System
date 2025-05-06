using System.ComponentModel;
using System.Reflection;

namespace Project_Mangement_System.Helpers
{
    public static class EnumHelper
    {
        

        public static string GetDescription(this object obj)
        {
            if (obj is Enum enumValue)
            {
                return enumValue.GetDescription();
            }

            return obj.ToString() ?? string.Empty;
        }

        public static IEnumerable<ItemListViewModel> ToItemList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new ItemListViewModel
                {
                    ID = Convert.ToInt32(x),
                    Name = x.GetDescription()
                });
        }



    }
}
