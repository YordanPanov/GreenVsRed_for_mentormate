using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GreenVsRed
{
    class Program
    {

        static void Main(string[] args)
        {
            
            do
            {
                Console.WriteLine("Input Matrix width:");
                MatrixD.Width = Convert.ToInt32(Console.ReadLine());

            } while (MatrixD.Width <= 0 && MatrixD.Width >= 1000);

            do
            {
                Console.WriteLine("Input Matrix height:");
                MatrixD.Height = Convert.ToInt32(Console.ReadLine());

            } while (MatrixD.Height <= 0 && MatrixD.Height >= 1000 && MatrixD.Height < MatrixD.Width );

          
            
            do
            {
                Console.WriteLine("Input Matrix String:");
                MatrixD.InputString = Console.ReadLine();
            } while (!ValidateMatrix());

            // filling the 2 matrixes so they are not empty
            MatrixD.MatrixZero = MatrixD.MatrixNextGen = new int[MatrixD.Width,MatrixD.Height];

            FillMatrix();
            PrintMatrix();


            do
            {
                Console.WriteLine("Input x1:");
                MatrixD.X1 = Convert.ToInt32(Console.ReadLine());
            } while ((MatrixD.X1 <= 0) && (MatrixD.X1 > MatrixD.Width ));

            do
            {
                Console.WriteLine("Input y1:");
                MatrixD.Y1 = Convert.ToInt32(Console.ReadLine());
            } while ((MatrixD.Y1 <= 0) && (MatrixD.Y1 > MatrixD.Height));
            do
            {
                Console.WriteLine("Input Itterations \"N\":");
                MatrixD.IterN = Convert.ToInt32(Console.ReadLine());
            } while (MatrixD.IterN <= 0);


            // Starting the iterations.


          
            for (int k = 0; k < MatrixD.IterN; k++)
            {
                if (MatrixD.MatrixZero[MatrixD.Y1, MatrixD.X1] == 1)
                {
                    MatrixD.Green++;
                }

                for (int i = 0; i < MatrixD.Height; i++)
                {
                    
                    for (int j = 0; j < MatrixD.Width; j++)
                    {
                        int count = CheckBoxes(i,j);
                        Rules(count,i,j);
                    }
                }
                MatrixD.MatrixZero = MatrixD.MatrixNextGen;
                

                PrintMatrix();
                Console.WriteLine(MatrixD.Green);

            }


            //Printing results

            Console.WriteLine("For cell ["+ MatrixD.X1 + ", "+MatrixD.Y1 + "] after " + MatrixD.IterN + " itterations");
            Console.WriteLine("Expected green states: " + MatrixD.Green);


        }
        // validate if the matrix is the right size and if contains only 
        public static bool ValidateMatrix()
        {
            // Check for length is sufficient 
            if (MatrixD.InputString.Length != (MatrixD.Width * MatrixD.Height))
            {
                Console.WriteLine("String lenght not equal to the elements of the matrix!");
                return false;
            }
            // Check if the string is composed of 0s and 1s
            else if (Regex.IsMatch(MatrixD.InputString, "/^[01]+$/g"))
            {
                Console.WriteLine("The string is not only 0s and 1s");
                return false;
            }
            else
            {
                return true;
            }
        }
        //Fill the matrix from the string
        private static void FillMatrix()
        {
            int z = 0;
            for (int i = 0; i < MatrixD.Width; i++)
            {
                for (int j = 0; j < MatrixD.Height; j++)
                {
                    MatrixD.MatrixZero[i, j] = CharUnicodeInfo.GetDecimalDigitValue(MatrixD.InputString[z]);

                    z++;
                }
            }
        }
        // Print the matrix on screen
        private static void PrintMatrix()
        {
            Console.WriteLine("The Matrix is:");
            for (int i = 0; i < MatrixD.Width; i++)
            {
                for (int j = 0; j < MatrixD.Height; j++)
                {
                    Console.Write(MatrixD.MatrixZero[i, j] + " ");
                }
         
                Console.WriteLine();
            }
        }
        // Checking the count for greens and reds surrounding boxes for single box
        public static int CheckBoxes (int i, int j)
        {
            int green=0;

            int[][] jaggedArray = new int[8][];
            jaggedArray[0] = new int[] { j - 1, i - 1 };    //box1 x1-1,  y1-1
            jaggedArray[1] = new int[] { j ,    i - 1 };    //box2 x1,    y1-1 
            jaggedArray[2] = new int[] { j + 1, i - 1 };    //box3 x1+1,  y1-1
            jaggedArray[3] = new int[] { j - 1, i };        //box4 x1-1 , y1
            jaggedArray[4] = new int[] { j + 1, i };        //box5 x1+1 , y1
            jaggedArray[5] = new int[] { j - 1, i + 1 };    //box6 x1-1 , y1+1     
            jaggedArray[6] = new int[] { j ,    i + 1  };   //box7 x1 , y1+1  
            jaggedArray[7] = new int[] { j + 1, i + 1 };    //box8 x1+1 , y1+1
          
                     
           
                for (int m = 0; m < 7; m++)
                {
                  
                    if ((jaggedArray[m][0] >= 0) 
                        && (jaggedArray[m][1] >=0) 
                        && (jaggedArray[m][0] < MatrixD.Width)
                        && (jaggedArray[m][1] < MatrixD.Height))
                    {
                        if (MatrixD.MatrixZero[jaggedArray[m][0], jaggedArray[m][1]] == 1)
                        {
                            green++;
                        }
                    }
                }

            return green;
        }
        // Im skipping rule 2 and 4 since they are redundent 
        public static void Rules(int green,int i,int j)
        {
                    // First rule checking if the cell is red and surrounded by 3 or 6 greens
            if (MatrixD.MatrixZero[j, i] == 0)  
            {
                if ((green == 3) || (green == 6))
                {
                    MatrixD.MatrixNextGen[j, i] = 1;
                }
            }
            else     // Rule 3 checking if the cell is green and is surrounded by 0,1,4,5,7,8 greens                    
            {

                if ((green == 0) || (green == 1) 
                    || (green == 4) || (green == 5) 
                    || (green == 7) || (green == 8))
                {
                    MatrixD.MatrixNextGen[j, i] = 0;
                }

            }
 
        }

        // To DO
    
    }   
        
}
