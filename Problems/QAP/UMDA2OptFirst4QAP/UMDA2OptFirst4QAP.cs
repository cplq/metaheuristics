using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class UMDA2OptFirst4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double timePenalty = 100;
		protected double popFactor = 0.02;
		protected double truncFactor = 0.4;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = (int) Math.Ceiling(popFactor * instance.NumberFacilities);
			int[] lowerBounds = new int[instance.NumberFacilities];
			int[] upperBounds = new int[instance.NumberFacilities];
			for (int i = 0; i < instance.NumberFacilities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberFacilities - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA2OptFirst4QAP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			umda.Run(timeLimit - (int) timePenalty);
			QAPSolution solution = new QAPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "UMDA with 2-opt (first improvement) local search for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.EDA;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.QAP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}
		
		public void UpdateParameters(double[] parameters)
		{
			timePenalty = parameters[0];
			popFactor = parameters[1];
			truncFactor = parameters[2];
		}				
	}
}
