using Project_Mangement_System.Data;

namespace Project_Mangement_System.MiddleWares
{
    public class TransactionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TransactionMiddleWare> _logger;

        public TransactionMiddleWare(RequestDelegate next, ILogger<TransactionMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Middleware execution method. Wraps the request inside a database transaction.
        /// </summary>
        /// <param name="httpContext">The HTTP context of the request.</param>
        public async Task InvokeAsync(HttpContext httpContext, Context dbContext)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync(); // Remove 'using'
            try
            {
                _logger.LogInformation("Transaction started.");

                // Proceed with the request
                await _next(httpContext);

                // Commit transaction if the request completes successfully

                await transaction.CommitAsync();
                _logger.LogInformation("Transaction committed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transaction rollback due to an exception.");

                // Rollback transaction in case of an error
                await transaction.RollbackAsync();
                throw; // Rethrow the exception to allow error handling middleware to process it
            }
            finally
            {
                await transaction.DisposeAsync(); // Dispose transaction explicitly at the end
            }
        }
    }
}
