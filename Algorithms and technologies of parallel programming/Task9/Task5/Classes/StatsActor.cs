using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Classes
{
    class StatsActor : UntypedActor
    {
        public class StatsMessage
        {
            public StatsMessage(float time)
            {
                _time = time;
            }
            public float _time { get; private set; }
        }

        public Form1 MyForm { get; private set; }
        private float time = 0;
        public int imgLength = 0;
        public int imgDone = 0;
        public StatsActor(Form1 myForm, int _imgLength)
        {
            MyForm = myForm;
            time = 0f;
            imgLength = _imgLength;
        }
        protected override void OnReceive(object message)
        {
            if (message is StatsMessage)
            {
                time += ((StatsMessage)message)._time;
                imgDone++;
                MyForm.UpdateInfo("",imgDone-1, ((StatsMessage)message)._time,time);
            }
        }
        protected override void PreStart()
        {
            MyForm.UpdateInfo("", imgDone-1, 0, time);
        }
    }
}
