using System.IO;

namespace AoC2021Tests
{
    public abstract class UnitTest
    {
        protected string[] ReadTestInput(string name)
        {
            return File.ReadAllLines(@".\TestData\" + name);
        }
    }
}
