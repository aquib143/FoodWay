using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FoodWay.Helpers
{
    public static class DropdownExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> Items, int selectedValue = 0)
        {
            List<SelectListItem> List = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem()
            {
                Text = "---Select---",
                Value = "0"
            };
            List.Add(sli);
            foreach (var Item in Items)
            {
                sli = new SelectListItem
                {
                    Text = Item.GetPropertyValue("Name"),
                    Value = Item.GetPropertyValue("Id")
                };
                List.Add(sli);
            }
            return List;
        }
    }

    public static class ReflectionExtension
    {
        public static string GetPropertyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}
