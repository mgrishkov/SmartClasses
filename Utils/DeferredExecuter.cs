using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Utils
{
    public class DeferredExecuter
    {
        public enum ExecutionMode { Sync, Async }

        private System.Timers.Timer _timer;
        private Action _action;

        public int Delay
        {
            get
            {
                return (_timer == null) ? 0 : (int)_timer.Interval;
            }
            set
            {
                if (value > 0)
                {
                    if (_timer == null)
                    {
                        _timer = new System.Timers.Timer()
                        {
                            Interval = value,
                            Enabled = false
                        };
                        _timer.Elapsed += _timer_Elapsed;
                    }
                    else
                    {
                        _timer.Interval = value;
                    };
                }
                else
                {
                    _timer.Dispose();
                };
            }
        }
        public Action Action
        {
            get { return _action; }
            set
            {
                if (_action != value)
                {
                    _action = value;
                };
            }
        }
        public ExecutionMode Mode { get; set; }

        public DeferredExecuter()
        {
            Mode = ExecutionMode.Async;
        }
        public void PostponeExecution()
        {
            if (_timer != null)
            {
                if (_timer.Enabled)
                {
                    _timer.Enabled = false;
                };
                _timer.Enabled = true;
            }
            else
            {
                execute();
            };
        }
        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            execute();
        }

        private void execute()
        {
            try
            {
                _timer.Enabled = false;
                switch (Mode)
                {
                    case ExecutionMode.Async:
                        Task.Factory.StartNew(Action);
                        break;
                    case ExecutionMode.Sync:
                        Action.Invoke();
                        break;
                }
            }
            catch
            {
            }
        }
    }
}
