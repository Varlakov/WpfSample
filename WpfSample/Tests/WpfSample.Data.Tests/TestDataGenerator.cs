using System.Collections.Generic;
using WpfSample.Data.Model;

namespace WpfSample.Services.Tests
{
    internal static class TestDataGenerator
    {
        internal static IEnumerable<Equipment> GetTestEquipments(int count = 100)
        {
            var ret = new List<Equipment>();
            for (int i = 1; i < count + 1; i++)
            {
                ret.Add(new Equipment
                {
                    Name = $"Equipment - {i}",
                    Id = i,
                });
            }

            return ret;
        }

        internal static IEnumerable<Activity> GetTestActivities(int count = 10)
        {
            var ret = new List<Activity>();
            for (int i = 1; i < count + 1; i++)
            {
                ret.Add(new Activity
                {
                    Name = $"Equipment - {i}",
                    Id = i,
                    CreatedBy = "user"
                });
            }

            return ret;
        }
    }
}
