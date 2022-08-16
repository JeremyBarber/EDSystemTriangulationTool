using Figgle;

namespace EDSMTriangulationTool
{
    internal class UserInteractionHandler
    {
        private static Model _model;

        private static bool finished = false;

        private static readonly Dictionary<char, (string description, Action method)> Options = new()
        {
            { '1', ("ADD new source system", AddSystem) },
            { '2', ("REMOVE the most recent source system", DeleteSystem) },
            { '3', ("HELP with using the tool", Help) },
            { '4', ("CLOSE the program", Exit) }
        };

        public UserInteractionHandler(Model model)
        {
            _model = model;
        }

        public void EntryPoint()
        {
            while(!finished)
            {
                RefreshTopText();
                PostOptions();
            }
        }

        private void RefreshTopText()
        {
            var sources = _model.Sources;
            var targets = _model.Targets;

            var sourcesList = sources.Count > 0 ? sources.Select(x => $"{x.systemName} ({x.minRadius}-{x.radius}LY)").ToList() : new List<string> { "None" };

            var targetsList = targets.Count > 0 ? targets.ToList() : new List<string> { "None" };
            if (targetsList.Count > 5)
            {
                var notDisplayedCount = targetsList.Count - 5;
                targetsList = targetsList.Take(5).ToList();
                targetsList.Add($"... and {notDisplayedCount} others");
            }

            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("ED STT"));
            Console.WriteLine("========================================================================");
            Console.WriteLine("");
            Console.WriteLine("Greetings CMDR, welcome to the Elite Dangerous System Triangulation Tool");
            Console.WriteLine("");
            Console.WriteLine($"Currently locked {_model.Sources.Count} systems");
            Console.WriteLine(string.Join(Environment.NewLine, sourcesList));
            Console.WriteLine("");
            Console.WriteLine($"Currently triangulated {_model.Targets.Count} systems");
            Console.WriteLine(string.Join(Environment.NewLine, targetsList));
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------");
        }

        private void PostOptions()
        {
            Console.WriteLine("Please select from the following options, CMDR:");
            Console.WriteLine(string.Join(Environment.NewLine, Options.Select(kvp => $"{kvp.Key} - {kvp.Value.description}")));
            ProcessOption();
        }

        private void ProcessOption()
        {
            var option = Console.ReadKey().KeyChar;

            RefreshTopText();

            if (Options.ContainsKey(option))
            {
                Options[option].method();
            }
            else
            {
                Console.WriteLine("Apologies CMDR, but that option is not valid");
                WaitBeforeContinuing();
            }
        }

        private static void AddSystem()
        {
            Console.WriteLine("Enter the name of your source system");
            var sourceSystemName = Console.ReadLine();

            if (string.IsNullOrEmpty(sourceSystemName))
            {
                Console.WriteLine($"Apologies CMDR, but it appears you did not type anything");
                WaitBeforeContinuing();
                return;
            }

            var systemExistsTask = _model.CheckSourceExists(sourceSystemName);
            Console.WriteLine($"Locating system {sourceSystemName}");
            SpinDuringTask(systemExistsTask);

            if (!systemExistsTask.Result)
            {
                Console.WriteLine($"Apologies CMDR, but the system {sourceSystemName} does not exist");
                WaitBeforeContinuing();
                return;
            }

            Console.WriteLine($"Please input the minimum search radius in LY");
            var minRadiusString = Console.ReadLine();

            if (!int.TryParse(minRadiusString, out var minRadius))
            {
                Console.WriteLine($"Apologies CMDR, but the minimum search distance {minRadiusString} is not a valid integer");
                WaitBeforeContinuing();
                return;
            }

            Console.WriteLine($"Please input the maximum search radius in LY");
            var maxRadiusString = Console.ReadLine();

            if (!int.TryParse(maxRadiusString, out var maxRadius))
            {
                Console.WriteLine($"Apologies CMDR, but the maximum search distance {maxRadiusString} is not a valid integer");
                WaitBeforeContinuing();
                return;
            }

            var systemTriangulationTask = _model.AddSource(sourceSystemName, minRadius, maxRadius);
            Console.WriteLine("Triangulating target systems");
            SpinDuringTask(systemTriangulationTask);
        }

        private static void DeleteSystem()
        {
            var systemTriangulationTask = _model.DeleteLastSource();
            Console.WriteLine("Triangulating target systems");
            SpinDuringTask(systemTriangulationTask);
        }

        private static void Help()
        {
            Console.WriteLine("");

            Console.ReadLine();
        }


        private static void Exit()
        {
            Console.WriteLine("Goodbye CMDR, safe travels in the black...");
            finished = true;
            WaitBeforeContinuing();
        }

        private static void SpinDuringTask(Task task)
        {
            var spinnerIndex = 0;
            var spinnerPosition = new List<string> { "¦----", "-¦---", "--¦--", "---¦-", "----¦", "---¦-", "--¦--", "-¦---" };

            while (!task.IsCompleted)
            {
                Console.Write($"\r{spinnerPosition[spinnerIndex]}");

                spinnerIndex++;
                spinnerIndex = spinnerIndex % (spinnerPosition.Count - 1);

                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            Console.Write($"\r     \r");
        }

        private static void WaitBeforeContinuing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }
}
