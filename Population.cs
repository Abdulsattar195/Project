using System;
using System.Collections;

namespace GeneticAlgorithm
{
	/// <summary>
	/// Summary description for Population.
	/// </summary>
	public class Population
	{

		const int kLength =16;
		const int kCrossover = kLength/2;
		const int kInitialPopulation =50;
        const int kPopulationLimit = 50;
            
            
            
        

		
		const float  kMutationFrequency = 0.3f;
		const float  kDeathFitness = 0.0f;
		const float  kReproductionFitness = 0.0f;

		ArrayList Genomes = new ArrayList();
		ArrayList GenomeReproducers  = new ArrayList();
		ArrayList GenomeResults = new ArrayList();
		ArrayList GenomeFamily = new ArrayList();

		int		  CurrentPopulation = kInitialPopulation;
		int		  Generation = 1;
		bool	  Best2 = true;

		public Population()
		{
			//
			// TODO: Add constructor logic here
			//
			for  (int i = 0; i < kInitialPopulation; i++)
			{
				ListGenome aGenome = new ListGenome(kLength);
				aGenome.SetCrossoverPoint(kCrossover);
				aGenome.CalculateFitness();
				Genomes.Add(aGenome);
			}

		}

		private void Mutate(Genome aGene)
		{
			if (ListGenome.TheSeed.NextDouble() < kMutationFrequency )
			{
			  	aGene.Mutate();
			}
		}

		public void NextGeneration()
		{
			// increment the generation;
			Generation++;


			// check who can die
			for (int i = 0; i < Genomes.Count; i++)
			{
				if (((Genome)Genomes[i]).CanDie(kDeathFitness))
				{
					Genomes.RemoveAt(i);
					i--;
				}
			}


			// determine who can reproduce
			GenomeReproducers.Clear();
			GenomeResults.Clear();
			for  (int i = 0; i < Genomes.Count; i++)
			{
				if (((Genome)Genomes[i]).CanReproduce(kReproductionFitness))
				{
					GenomeReproducers.Add(Genomes[i]);			
				}
			}
			
			// do the crossover of the genes and add them to the population
			DoCrossover(GenomeReproducers);

			Genomes = (ArrayList)GenomeResults.Clone();

			// mutate a few genes in the new population
			for  (int i = 0; i < Genomes.Count; i++)
			{
				Mutate((Genome)Genomes[i]);
			}

			// calculate fitness of all the genes
			for  (int i = 0; i < Genomes.Count; i++)
			{
				((Genome)Genomes[i]).CalculateFitness();
			}


			Genomes.Sort();
			
			// kill all the genes above the population limit
			if (Genomes.Count > kPopulationLimit)
				Genomes.RemoveRange(kPopulationLimit, Genomes.Count - kPopulationLimit);
			
			CurrentPopulation = Genomes.Count;
			
		}
		

		public void CalculateFitnessForAll(ArrayList genes)
		{
			foreach(ListGenome lg in genes)
			{
			  lg.CalculateFitness();
			}
		}

		public void DoCrossover(ArrayList genes)
		{
			ArrayList GeneMoms = new ArrayList();
			ArrayList GeneDads = new ArrayList();

			for (int i = 0; i < genes.Count; i++)
			{
				// randomly pick the moms and dad's
				if (ListGenome.TheSeed.NextDouble() < 0.5)
				{
					GeneMoms.Add(genes[i]);
				}
				else
				{
					GeneDads.Add(genes[i]);
				}
			}

			//  now make them equal
			if (GeneMoms.Count > GeneDads.Count)
			{
				while (GeneMoms.Count > GeneDads.Count)
				{
					GeneDads.Add(GeneMoms[GeneMoms.Count - 1]);
					GeneMoms.RemoveAt(GeneMoms.Count - 1);
				}

				if (GeneDads.Count > GeneMoms.Count)
				{
					GeneDads.RemoveAt(GeneDads.Count - 1); // make sure they are equal
				}

			}
			else
			{
				while (GeneDads.Count > GeneMoms.Count)
				{
					GeneMoms.Add(GeneDads[GeneDads.Count - 1]);
					GeneDads.RemoveAt(GeneDads.Count - 1);
				}

				if (GeneMoms.Count > GeneDads.Count)
				{
					GeneMoms.RemoveAt(GeneMoms.Count - 1); // make sure they are equal
				}
			}

			// now cross them over and add them according to fitness
			for (int i = 0; i < GeneDads.Count; i ++)
			{
				// pick best 2 from parent and children
				ListGenome babyGene1 = (ListGenome)((ListGenome)GeneDads[i]).Crossover((ListGenome)GeneMoms[i]);
			    ListGenome babyGene2 = (ListGenome)((ListGenome)GeneMoms[i]).Crossover((ListGenome)GeneDads[i]);
			
				GenomeFamily.Clear();
				GenomeFamily.Add(GeneDads[i]);
				GenomeFamily.Add(GeneMoms[i]);
				GenomeFamily.Add(babyGene1);
				GenomeFamily.Add(babyGene2);
				CalculateFitnessForAll(GenomeFamily);
				GenomeFamily.Sort();

				if (Best2 == true)
				{
					// if Best2 is true, add top fitness genes
					GenomeResults.Add(GenomeFamily[0]);					
					GenomeResults.Add(GenomeFamily[1]);					

				}
				else
				{
					GenomeResults.Add(babyGene1);
					GenomeResults.Add(babyGene2);
				}
			}

		}

public bool er()
		{
			bool r = false;
			for (int i = 0; i < Genomes.Count; i++)
			{
				if (((Genome)Genomes[i]).CurrentFitness==1)
				{
					r = true;
				}
			}return r;
		}
		public void WriteNextGeneration()
		{
			// just write the top 20
			Console.WriteLine("Generation {0}\n", Generation);
			for  (int i = 0; i <  CurrentPopulation ; i++)
			{
				Console.WriteLine(((Genome)Genomes[i]).ToString());
			}

			Console.WriteLine("Hit the enter key to continue...\n");
			//Console.ReadLine();
		}
	}
}
