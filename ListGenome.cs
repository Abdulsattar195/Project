using System;
using System.Collections;
namespace GeneticAlgorithm
{
	/// <summary>
	/// Summary description for ListGenome.
	/// </summary>
	public class ListGenome : Genome
	{
		ArrayList TheArray = new ArrayList();
		public static Random TheSeed = new Random((int)DateTime.Now.Ticks);
		
        int[] a = new int[] {39,65,117,234,494,975,2015,4056,8164,16445,32968,66001,132041,101548,40588,81202};
		public override int CompareTo(object a)
		{
			ListGenome Gene1 = this;
			ListGenome Gene2 = (ListGenome)a;
			return Math.Sign(Gene2.CurrentFitness  -  Gene1.CurrentFitness);
		}


		public override void SetCrossoverPoint(int crossoverPoint)
		{
			CrossoverPoint = 	crossoverPoint;
		}

		public ListGenome()
		{

		}


		public ListGenome(long length)
		{
			//
			// TODO: Add constructor logic here
			//
			Length = length;
			
			for (int i = 0; i < Length; i++)
			{
			   int nextValue =(int) GenerateGeneValue();
			   TheArray.Add(nextValue);
			}
		}

		public override void Initialize()
		{

		}

		public override bool CanDie(float fitness)
		{
			if (CurrentFitness <= (int)(fitness * 100.0f))
			{
				return true;
			}

			return false;
		}


		public override bool CanReproduce(float fitness)
		{
			if (ListGenome.TheSeed.NextDouble() >= fitness )
			{
				return true;
			}

			return false;
		}


		public override int GenerateGeneValue()
		{
            if (TheSeed.NextDouble() > 0.5)
                return 1;
            else 
                return 0;
		}

		public override void Mutate()
		{
			MutationIndex = TheSeed.Next((int)Length);
            int val;
           if((int)TheArray[MutationIndex]==0)
            val = 1;
                else
                    val = 0;

            TheArray[MutationIndex] =val;



        }

        // This fitness function calculates the closest product sum
        private float CalculateClosestProductSum()
        {
			//fitness for a perfect number
			float fitt = 0;
           float fullsum = 486952; float t = 198296; float maxdif;
			float sum = 0;
            //for (int i = 0; i < Length; i++)
            //{
            //   fullsum+=  a[i];

            //}
            for (int i = 0; i < Length; i++)
            {
                sum = sum + (((int)TheArray[i]) * a[i]);

            }
			maxdif = Math.Max(t, fullsum - t);
			float aa = (float)Math.Abs(t - sum);
			float bb = (float)aa / maxdif;
			
            if (sum <= t)
            {
              fitt= (float)(1 - (Math.Sqrt(aa / t)));
            }
            else
            {
                fitt= (float)(1 - (Math.Pow(bb, 0.16666666666666666666666666666667)));
            }
			
			return fitt;
        }


        // This fitness function calculates the closest product sum
        private float CalculateClosestSumTo10()
        {
            return 0.0f;
          //  //Double sum = 0.0f;
          //  //for (int i = 0; i < Length; i++)
          //  //{
          //  //    sum += (Double)TheArray[i];
          //  //}

          //  //if (sum == 10)
          //  //    return 1;
          //  //else
          //  //    return Math.Abs(Math.Abs(1.0f / (sum - 10)) - 0.02f);
          //  float fullsum=0.0f /*= 486952.0f*/; float t = 198296.0f; float maxdif=0.0f;
          // float sum = 0.0f; Double f1 = 0.0d; Double f2 = 0.0d; float abs = 0.0f;
          //  float fitt = 0.0f;
          //  for (int i = 0; i < Length; i++)
          //  {
          //      fullsum += a[i]/1.0f;

          //  }
          //  for (int i = 0; i < Length; i++)
          //  {
          //      sum = sum + (((int)TheArray[i]) * a[i]);

          //  }
           
          //abs= Math.Abs(t - sum);
          //  maxdif = Math.Max(t, fullsum - t);
          //  f1 = abs / t;
          //   f2 = abs / maxdif;
          //  if (sum <= t)
          //  {
          //      fitt = (1.0f - (float)Math.Pow(f1,0.5f));
          //  }
          //  else
          //  {
          //      fitt = 1.0f - (float)(Math.Pow(f2,(1/6.0f))) ;
          //  }
          //  return fitt;

        }

        public override float CalculateFitness()
        {
            CurrentFitness = CalculateClosestProductSum();
			//CurrentFitness = CalculateClosestSumTo10();
				
            return (float)CurrentFitness;
		}

		public override string ToString()
		{
			string strResult = "";
			for (int i = 0; i < Length; i++)
			{
			  strResult = strResult + (TheArray[i]).ToString() + " ";
			}

			strResult += "-->" +((float) CurrentFitness).ToString();

			return strResult;
		}

		public override void CopyGeneInfo(Genome dest)
		{
			ListGenome theGene = (ListGenome)dest;
			theGene.Length = Length;
			
		}


		public override Genome Crossover(Genome g)
		{
			ListGenome aGene1 = new ListGenome();
			ListGenome aGene2 = new ListGenome();
			g.CopyGeneInfo(aGene1);
			g.CopyGeneInfo(aGene2);


			ListGenome CrossingGene = (ListGenome)g;
			for (int i = 0; i <TheArray.Count; i++)
			{
				if (TheSeed.NextDouble() > 0.5)
				{
					aGene1.TheArray.Add(CrossingGene.TheArray[i]);
				}
				else
				{
					aGene1.TheArray.Add(TheArray[i]);
				}
			}

			//for (int j = CrossoverPoint; j < Length; j++)
			//{
			//	aGene1.TheArray.Add(TheArray[j]);
			//	aGene2.TheArray.Add(CrossingGene.TheArray[j]);
			//}

			// 50/50 chance of returning gene1 or gene2
			ListGenome aGene = null;
			//if (TheSeed.Next(2) == 1)			
			//{
				aGene = aGene1;
			//}
			//else
			//{
			//	aGene = aGene2;
			//}

			return aGene;
		}

	}
}
