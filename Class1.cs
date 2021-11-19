using System;

namespace GeneticAlgorithm
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Population TestPopulation = new Population();
			TestPopulation.WriteNextGeneration();
			
			//for (int i = 0; i <1000; i++)
			while(TestPopulation.er()!=true)
			{
				TestPopulation.NextGeneration();
				TestPopulation.WriteNextGeneration();
				
			}

			Console.ReadLine();
			//
			// TODO: Add code to start application here
			//
		}
	}
}
