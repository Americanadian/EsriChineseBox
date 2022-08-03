namespace ChineseBox
{
    public sealed class ChineseBox
    {
        private static int refCount = 0;
        private static readonly object lockObj = new object();
        private static ChineseBox? instance = null;

        private ChineseBox() { }

        public static ChineseBox GetInstance()
        {
            
            lock (lockObj)
            {
                if (instance == null)
                {
                    try
                    {
                        instance = new ChineseBox();
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine("Caught the following Exception: " + e.Message);
                        throw;
                    }
                }

                return instance;
            }
        }

        public ChineseBox Nest(ChineseBox nest)
        {
            instance = nest;
            refCount++;
            return instance;
        }

        public int NumSubBoxes
        {
            get
            {
                return refCount;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Number of internal boxes: " + ChineseBox.GetInstance().Nest(
                                                             ChineseBox.GetInstance().Nest(
                                                             ChineseBox.GetInstance().Nest(
                                                             ChineseBox.GetInstance()))).NumSubBoxes);

            Console.ReadKey();
        }
    }
}