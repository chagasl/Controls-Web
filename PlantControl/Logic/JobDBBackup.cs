using Quartz;

namespace PlantControl
{
    public class JobDBBackup : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SQLQuery sQLQuery = new SQLQuery();
            sQLQuery.ExecuteDBBackup();
        }
    }
}