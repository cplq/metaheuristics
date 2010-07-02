using System;

namespace Metaheuristics
{
	public class DiscreteGRASP2OptFirst4QAP: DiscreteGRASP
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteGRASP2OptFirst4QAP ( QAPInstance instance, double rclThreshold)
			:base(rclThreshold)
		{
			Instance = instance;
		}
		
		protected override double Fitness (int[] solution)
		{
			return QAPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] GRCSolution ()
		{
			return QAPUtils.GRCSolution(Instance, RCLThreshold);
		}
		
		protected override void LocalSearch (int[] solution)
		{
			QAPUtils.LocalSearch2OptFirst(Instance, solution);
		}
	}
}
