using System;
using System.Collections.Generic;
using System.Text;

namespace Autodesk.Forge.DesignAutomation.Inventor.Utils.Helpers
{
    public class DataConverter
    {
        public bool TryGetValueFromObjectAs<T>(object value, out T outValue)
        {
            if (typeof(T).IsEnum)
            {
                try
                {
                    outValue = (T)Enum.Parse(typeof(T), value.ToString(), true);
                }
                catch (Exception)
                {
                    outValue = default;
                    return false;
                }

                return true;
            }

            if (value is T convertedValue)
            {
                outValue = convertedValue;
                return true;
            }

            try
            {
                outValue = (T)Convert.ChangeType(value, typeof(T));
                return true;
            }
            catch (Exception)
            {
                outValue = default;
                return false;
            }
        }
    }
}
