using System;

namespace Sequential_tasks.Services
{
    static class GenerateId
    {
        static public string generate()
        {
            string generated = "";
            int ch;
            
            Random random = new Random();

            int randonLoop = random.Next(5, 10);

            for(int i=0; i< randonLoop; i++)
            {
                ch =  random.Next(48, 125);
                generated += Convert.ToChar(ch);  
            }
            return generated;
        }

        static public string generate(int numberOfChars)
        {
            string generated = "";
            int ch;

            Random random = new Random();

            for (int i = 0; i < numberOfChars; i++)
            {
                ch = random.Next(48, 125);
                generated += Convert.ToChar(ch);
            }
            return generated;
        }

        static public string generate(int startInterval, int endInterval, int numberOfChars)
        {
            string generated = "";
            int ch;

            Random random = new Random();

            for (int i = 0; i < numberOfChars; i++)
            {
                ch = random.Next(startInterval);
                generated += Convert.ToChar(ch);
            }
            return generated;
        }
    }
}
