using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    //1. Event Data Class
    public class CustomEventArgs : System.EventArgs
    {
        // 1.1 Public data members
        public int Number { get; set; }
        public string Message { get; set; } = "";
    }

    //2. Declare the Delegate
    public delegate void CustomEventHandler(object sender, CustomEventArgs e);

    //3. Declare Publisher class
    class Publisher
    {
        //3.1 Declare a variable for the delegate [Instantion part ]
        public event CustomEventHandler MyEvent;

        //3.2 Write a function which performs an activity and triggers notification
        public void TriggerNotifications()
        {
            //3.3 Iterate over to find the threshold levels
            for(int i = 0; i < 10; i++)
            {
                // 3.4 check for limits
                if(i==5 || i==6)
                {
                    // 3.5 Build the event data class with values
                    CustomEventArgs e = new CustomEventArgs
                    {
                        Number = i,
                        Message = "Threshold reached"
                    };

                    //3.6 check whether the event object is null, if not invoke it -> delegate invocation
                    MyEvent?.Invoke(null, e);

                    // ?. is called the null conditional operator and is equivalent to 
                    //if(MyEvent is not null) MyEvent.Invoke(null,e);
                }
            }
        }
    }

    //4. Declare Subscriber Class
    class Subscriber 
    { 
        // 4.1 Mehtod which matches delegate signature
        public void HandleNotification(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"SUBSCRIBER SAYS: [Number : {e.Number},Message: {e.Message}]");
        }
    }
    internal class EventDelegationModel
    {
        internal static void Test()
        {
            // 5.1 Declare the Publisher object
            Publisher p = new Publisher();
            // 5.2 Declare the subscriber object
            Subscriber s = new Subscriber();
            //5.3 Bind the publisher delegate with the subscriber funtion
            //the second part of delegate instantiation (adding more subscribers to delegate)s

            p.MyEvent += new CustomEventHandler(s.HandleNotification);

            // 5.4 Call the publisher method to trigger notifications
            p.TriggerNotifications();

            //5.5 add more subscribers using Anonymous methods and lambdas
            // and trigger notifactions again
            Console.WriteLine("\nAdding more handlers/subscribers...");
            p.MyEvent += delegate (object sender, CustomEventArgs args)
            {
                Console.WriteLine($"ANON SAYS FROM 1: [Number : {args.Number},Message: {args.Message}]");
            };
            p.MyEvent += (send, args) => { Console.WriteLine($"LAMBDA SAYS FROM 2: [Number : {args.Number},Message: {args.Message}]"); };
            p.TriggerNotifications();
        }
    }
}
