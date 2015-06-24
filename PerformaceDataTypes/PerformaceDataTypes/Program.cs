using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Diagnostics;
using System.Reflection;

namespace PerformaceDataTypes {
    class Program {
        static void Main(string[] args) {
            const int maxAttempt = 1000000;
            var stopwatch = new Stopwatch();

            #region Normal Invocation
            var prog = new Program();
            stopwatch.Start();
            for (int i = 0; i < maxAttempt; i++) {
                prog.Foo();
            }
            stopwatch.Stop();
            Console.WriteLine("Toma {0} mill", stopwatch.ElapsedMilliseconds);
            #endregion

            #region Using Reflection
            Type t = prog.GetType();
            stopwatch.Restart();
            for (int i = 0; i < maxAttempt; i++) {
                t.InvokeMember("Foo", BindingFlags.InvokeMethod, null, prog, new object[] { });
            }
            stopwatch.Stop();
            Console.WriteLine("Toma {0} mill", stopwatch.ElapsedMilliseconds);
            #endregion

            #region Dynamic Invocation
            dynamic dynamicProg = prog;

            stopwatch.Restart();
            for (int i = 0; i < maxAttempt; i++) {
                dynamicProg.Foo();
            }
            stopwatch.Stop();
            Console.WriteLine("Toma {0} mill", stopwatch.ElapsedMilliseconds);
            #endregion

            Console.ReadKey();

        }

        public void Foo() {
            List<Cx> x = new List<Cx>();
            x.Add(new Cx {
                Id = 1
            });
        }
    }

    class Cx {
        private int id;

        public int Id {
            get { return id; }
            set { id = value; }
        }

    }
}
