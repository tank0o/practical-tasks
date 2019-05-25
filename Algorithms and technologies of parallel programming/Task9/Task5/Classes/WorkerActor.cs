using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Classes
{
    class WorkerActor : UntypedActor
    {
        public class ProcessImgMessage
        {
            public ProcessImgMessage(Picture picture, Filter filter, string editedPath)
            {
                this.picture = picture;
                this.filter = filter;
                this.editedPath = editedPath;
            }
            public Picture picture { get; private set; }
            public Filter filter { get; private set; }
            public string editedPath { get; private set; }
        }

        private readonly IActorRef _statsActor;

        public WorkerActor(IActorRef statsActor)
        {
            _statsActor = statsActor;
        }
        protected override void OnReceive(object message)
        {
            if (message is ProcessImgMessage)
            {
                float time = 0;
                Picture.GetImage(((ProcessImgMessage)message).filter.EditImage(((ProcessImgMessage)message).picture,out time)).Save(((ProcessImgMessage)message).editedPath + "\\" + +new Random().Next(0,99999) + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                _statsActor.Tell(new StatsActor.StatsMessage(time));
            }

        }
    }
}
