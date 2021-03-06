using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class TwoOptFirst4TSP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			int[] assignment = TSPUtils.GreedySolution(instance);
			TSPUtils.LocalSearch2OptFirst(instance, assignment);
			TSPSolution solution = new TSPSolution(instance, assignment);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "2-opt (first improvement) for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SH;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TSP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}
	}
}
