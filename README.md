# BlackFinchTest

What would I have improved if production:


1. I would have applied SOLID principles by adding three separate classes. One class for loan application, one for loan decision and loan metrics that would implement given interfaces 

     public class LoanApplicationService : ILoanApplicationService
     public class LoanDecisionService : ILoanDecisionService
     public class LoanMetricsService: ILoanMetricsService

each with individual responsibility and each service class would inject each associated repository dependancy for DB access ect.. 
for example (repository design pattern)


    public LoanApplicationService(ILoanApplicationRepository repository, ILoanDecisionService decisionService, ILoanMetricsService metricsService)
     {
         _repository = repository;
         _decisionService = decisionService;
         _metricsService = metricsService;
     }
    
I would then have a final class where I would inject the services I need to obtain the final result. I would also have unit tests for each service along side integration tests and possibly implement some ORM or DAL where appropriate. for front end, assuming its a website I would include the appropriate Behavioural tests.
