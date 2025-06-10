using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tago.Extensions.AspNetCore.Batch.Model;

namespace TAGO.Template.Batch
{
    public class BatchHandler
    {
        public const string JOB_TYPE = "my-job-type";

        private readonly ILogger<BatchHandler> _logger;
        private readonly IAsyncJobExecutor asyncJobExecutor;

        public BatchHandler(
            ILogger<BatchHandler> logger,
            IConfiguration config,
            IAsyncJobExecutor asyncJobExecutor)
        {
            _logger = logger;
            this.asyncJobExecutor = asyncJobExecutor;
        }
        public async Task<string> ExecuteAsync(MyBatchData jobData)
        {
            var jobType = JOB_TYPE;
            try
            {
                this._logger.LogInformation($"executing job: '{jobType}'");
                var jobId = await asyncJobExecutor.ExecuteAsync(jobType, jobData, async (model, ct) =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(1000), ct);
                });
                return jobId;
            }
            catch (AsyncJobAlreadyExistException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

    }

}
