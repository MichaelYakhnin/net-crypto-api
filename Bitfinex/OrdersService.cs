using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ExternalServices
{
    
  public  class OrdersService
    {
         Timer _timer; // From System.Timers
         List<DateTime> _l; // Stores timer results

        public  List<DateTime> DateList // Gets the results
        {
            get
            {
                if (_l == null) // Lazily initialize the timer
                {
                    Start(); // Start the timer
                }
                return _l; // Return the list of dates
            }
        }

         void Start()
        {
            _l = new List<DateTime>(); // Allocate the list
            _timer = new Timer(3000); // Set up the timer for 3 seconds
                                      //
                                      // Type "_timer.Elapsed += " and press tab twice.
                                      //
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true; // Enable it
        }

         void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _l.Add(DateTime.Now); // Add date on each timer event
        }
    }
}
