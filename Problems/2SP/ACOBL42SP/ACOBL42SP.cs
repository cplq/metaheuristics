using System;

namespace Metaheuristics
{
	public class ACOBL42SP : IMetaheuristic
	{
		protected int timePenalty = 250;
		protected double rho = 0.02;
		protected double alpha = 1;
		protected double beta = 3;
		protected int maxReinit = 5;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(inputFile);
			MaxMinAntSystem aco = new MaxMinAntSystemBL42SP(instance, instance.NumberItems, rho, alpha, beta, maxReinit);
			// Solving the problem and writing the best solution found.
			aco.Run(timeLimit - timePenalty);
			int[,] coordinates = TwoSPUtils.BLCoordinates(instance, aco.BestSolution);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(outputFile);
		}

		public string Name {
			get {
				return "ACO using the BL heuristic for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ACO;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TwoSP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}
	}
}