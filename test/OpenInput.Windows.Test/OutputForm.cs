﻿namespace OpenInput
{
    using Nine.Injection;
    using System.Windows.Forms;
    using System;

    using Timer = System.Timers.Timer;
    using ElapsedEventArgs = System.Timers.ElapsedEventArgs;

    public class OutputForm : Form
    {
        private IContainer container;
        private Timer timer;

        public OutputForm()
        {
            this.Width = 500;
            this.Height = 500;

            this.container = new Container();
            this.container
                .Map<IMouse>(new Mouse())
                .Map<IKeyboard>(new Keyboard());

            this.timer = new Timer((1.0f / 12) * 1000.0f);
            this.timer.Elapsed += TimerElapsed;
        }
        
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // TODO: This also creates an issue when tabbing back!
            container.Get<IKeyboard>().SetHandle(this.Handle);
            container.Get<IMouse>().SetHandle(this.Handle);

            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            var keyboard = container.Get<IKeyboard>();
            if (keyboard != null)
            {
                var keyboardState = keyboard.GetCurrentState();
                Console.WriteLine(keyboardState.ToString());
            }

            var mouse = container.Get<IMouse>();
            if (mouse != null)
            {
                var mouseState = mouse.GetCurrentState();
                Console.WriteLine(mouseState.ToString());
            }
        }
    }
}