
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MultiThreading.Threading
{

    public class ParamThreadStart
    {
        public void StartRun(object limit)
        {
            for (int i = 1; i <= Convert.ToInt32(limit); i++)
            {
                Console.WriteLine(i);
            }
        }
        public void ParamThreadStartMain()
        {
            ParamThreadStart paramThread = new ParamThreadStart();
            ParameterizedThreadStart paramThreadStart = new ParameterizedThreadStart(paramThread.StartRun);
            Thread thread = new Thread(paramThreadStart);
            thread.Start(5);
        }
    }
    public class ThreadUse
    {
        private void StartKill(object num)
        {
            for (int i = 1; i <= (int)num; i++)
            {
                Console.WriteLine(i);
            }
        }
        public void Caller()
        {
            StartKill(5);
        }
    }
    public class ThreadCallBack
    {
        private readonly int _number;
        private readonly Action<int> _delegate;
        public ThreadCallBack(int number, Action<int> delegat)
        {
            _number = number;
            _delegate = delegat;
        }
        public void StartKill()
        {
            int sum = 0;
            for (int i = 1; i <= _number; i++)
            {
                sum += i;
            }
            if (_delegate != null)
                _delegate(sum);
        }
    }
    public class ThreadCaller
    {
        public void ShowNumber(int num)
        {
            Console.WriteLine(num);
        }
        public void Caller()
        {
            Action<int> _delegate = new Action<int>(ShowNumber);
            ThreadCallBack caller = new ThreadCallBack(5, _delegate);
            Thread thread = new Thread(() => caller.StartKill());
            thread.Start();
        }
    }
}
