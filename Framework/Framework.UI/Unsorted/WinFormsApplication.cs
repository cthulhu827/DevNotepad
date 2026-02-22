using Framework.AppInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Framework.UI
{
    public abstract class WinFormsApplication : BaseApplication
    {
        public override void Run()
        {
            base.Run();

            InitActions();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainWindow = DoCreateMainWindow();
            Application.Run(mainWindow);
        }

        private static void InitActions()
        {
            var implClasses = AnnotatedClasses.AllOf<ActionImplementor>();
            foreach (var implClass in implClasses)
            {
                var actionImplementor = (ActionImplementor)Activator.CreateInstance(implClass);
                actionImplementor.InitActions();
            }
        }

        protected abstract Form DoCreateMainWindow();
    }
}
