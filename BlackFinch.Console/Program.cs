using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanApplication
{
    class Program
    {
        static List<Application> applications = new List<Application>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter amount of loan in GBP:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal loanAmount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal value.");
                    continue;
                }

                Console.WriteLine("Enter value of asset in GBP:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal assetValue))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal value.");
                    continue;
                }

                Console.WriteLine("Enter credit score of the applicant (between 1 and 999):");
                if (!int.TryParse(Console.ReadLine(), out int creditScore))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer value.");
                    continue;
                }

                // Check if loan amount is within acceptable range
                if (loanAmount < 100000 || loanAmount > 1500000)
                {
                    Console.WriteLine("Loan application declined. Loan amount must be between £100,000 and £1,500,000.");
                    RecordApplication(false, loanAmount);
                    continue;
                }

                // Calculate loan to value ratio
                decimal ltv = (loanAmount / assetValue) * 100;

                // Check if LTV and credit score meet requirements
                if (loanAmount >= 1000000)
                {
                    if (ltv > 60 || creditScore < 950)
                    {
                        Console.WriteLine("Loan application declined. LTV must be 60% or less and credit score must be 950 or higher.");
                        RecordApplication(false, loanAmount);
                        continue;
                    }
                }
                else
                {
                    if (ltv >= 90)
                    {
                        Console.WriteLine("Loan application declined. LTV must be less than 90%.");
                        RecordApplication(false, loanAmount);
                        continue;
                    }
                    else if (ltv >= 80 && creditScore < 800)
                    {
                        Console.WriteLine("Loan application declined. LTV must be less than 80% and credit score must be 800 or higher.");
                        RecordApplication(false, loanAmount);
                        continue;
                    }
                    else if (ltv >= 60 && creditScore < 750)
                    {
                        Console.WriteLine("Loan application declined. LTV must be less than 60% and credit score must be 750 or higher.");
                        RecordApplication(false, loanAmount);
                        continue;
                    }
                }

                Console.WriteLine("Loan application accepted!");
                RecordApplication(true, loanAmount);
            }
        }

        static void RecordApplication(bool success, decimal loanAmount)
        {
            applications.Add(new Application(success, loanAmount));

            int totalApplications = applications.Count();
            int successfulApplications = applications.Count(a => a.Success);
            int unsuccessfulApplications = totalApplications - successfulApplications;
            decimal totalLoanAmount = applications.Sum(a => a.LoanAmount);
            decimal meanLtv = applications.Average(a => (a.LoanAmount / a.AssetValue) * 100);

            Console.WriteLine($"Total number of applications to date: {totalApplications} (successful: {successfulApplications}, unsuccessful: {unsuccessfulApplications})");
            Console.WriteLine($"Total value of loans written to date: £{totalLoanAmount:N2}");
            Console.WriteLine($"Mean average Loan to Value of all applications received to date: {meanLtv:N2}%");
        }
    }

    class Application
    {
        public bool Success { get; }
        public decimal LoanAmount { get; }
        public decimal AssetValue { get; }

        public Application(bool success, decimal loanAmount, decimal assetValue = 0)
        {
            Success = success;
            LoanAmount = loanAmount;
            AssetValue = assetValue == 0 ? loanAmount : assetValue;
        }
    }
}