using PlantControl.Views;
using Quartz;

namespace PlantControl
{
    public class JobLimitsComp : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            LimitsCompare limitsCompare = new LimitsCompare();
            limitsCompare.AutoCompare();
        }    
    }
}