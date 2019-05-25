using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Classes
{
    class RouterActor : UntypedActor
    {
        public class StartWorker
        {

            public StartWorker(IReadOnlyList<Picture> picture, Filter filter,string editPath, IActorRef statsActor)
            {
                this.pictures = picture;
                this.filter = filter;
                this.editPath = editPath;
                this.StatsActor = statsActor;
            }

            public IReadOnlyList<Picture> pictures;
            public Filter filter { get; private set; }
            public string editPath{ get; private set; }
            public IActorRef StatsActor { get; private set; }
        }
        protected override void PostStop()
        {
            Context.System.Terminate();
        }
        IActorRef workerPool;
        protected override void OnReceive(object message)
        {
            if (message is StartWorker)
            {
                var msg = message as StartWorker;
                var pictures = ((StartWorker)message).pictures;
                workerPool = Context.ActorOf(Props.Create(() => new WorkerActor(msg.StatsActor)).WithRouter(new SmallestMailboxPool(5)));
                foreach (Picture p in pictures)
                {
                    workerPool.Tell(new WorkerActor.ProcessImgMessage(p, ((StartWorker)message).filter, ((StartWorker)message).editPath));
                }
            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            OneForOneStrategy k = new OneForOneStrategy(
                10,
                TimeSpan.FromSeconds(30),
                x =>
                {
                    if (x is NotSupportedException) return Directive.Stop;
                    if (x is IOException) return Directive.Restart;
                    return Directive.Stop;
                });
            return k;
        }
    }
}
