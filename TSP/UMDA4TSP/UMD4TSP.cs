using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class UMD4TSP
	{
		public static string Algoritmo = "UMDA4TSP";
		
		public static string[] Integrantes = Team.Members;
		
		public static string Nombre_equipo = Team.Name;
		
		public static List<double> Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = 50 * instance.Dimension;
			double truncFactor = 0.3;
			int[] lowerBounds = new int[instance.Dimension];
			int[] upperBounds = new int[instance.Dimension];
			for (int i = 0; i < instance.Dimension; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.Dimension - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA4TSP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			List<double> solutions = umda.Run(timeLimit);
			TSPSolution solution = new TSPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
			
			return solutions;
		}
	}
	
	class DiscreteUMDA4TSP : DiscreteUMDA
	{
		public TSPInstance Instance { get; protected set; }
		
		public DiscreteUMDA4TSP(TSPInstance instance, int popSize, double truncFactor, int[] lowerBounds, int[] upperBounds)
			: base(popSize, truncFactor, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
		}
		
		protected override void Repair(int[] individual)
		{
			int visitedCitiesCount = 0;
			bool[] visitedCities = new bool[Instance.Dimension];
			bool[] repeatedPositions = new bool[Instance.Dimension];
				
			// Get information to decide if the individual is valid.
			for (int i = 0; i < Instance.Dimension; i++) {
				if (!visitedCities[individual[i]]) {
					visitedCitiesCount += 1;
					visitedCities[individual[i]] = true;
				}
				else {
					repeatedPositions[i] = true;
				}
			}
				
			// If the individual is invalid, make it valid.
			if (visitedCitiesCount != Instance.Dimension) {
				for (int i = 0; i < repeatedPositions.Length; i++) {
					if (repeatedPositions[i]) {
						int count = Statistics.RandomDiscreteUniform(1, Instance.Dimension - visitedCitiesCount);
						for (int c = 0; c < visitedCities.Length; c++) {
							if (!visitedCities[c]) {
								count -= 1;
								if (count == 0) {
									individual[i] = c;
									repeatedPositions[i] = false;
									visitedCities[c] = true;
									visitedCitiesCount += 1;
									break;
								}
							}
						}							
					}
				}
			}
		}
		
		protected override double Fitness(int[] individual)
		{
			double fitness = 0;
			
			for (int i = 1; i < individual.Length; i++) {
				fitness += Instance.Costs[individual[i-1],individual[i]];
			}
			fitness += Instance.Costs[individual[individual.Length-1],individual[0]];

			return fitness;
		}
	}
}