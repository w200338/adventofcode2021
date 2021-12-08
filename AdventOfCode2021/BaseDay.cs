namespace AdventOfCode2021
{
    using System.IO;
    using System.Threading.Tasks;

    public abstract class BaseDay
    {
		protected string Input;

        public async Task ReadInput()
        {
            using (StreamReader reader = new StreamReader($"Days/{GetType().Name}/input.txt"))
            {
                Input = await reader.ReadToEndAsync();
            }
        }

        public abstract string Part1();

        public abstract string Part2();
	}
}