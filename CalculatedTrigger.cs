using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MCT.Functions.Models;

namespace MCT.Functions
{
    public class CalculatedTrigger
    {
        [FunctionName("CalculatedTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculater/{getal1}/{op}/{getal2}")] HttpRequest req,
            int getal1,
            string op,
            int getal2,
            ILogger log)
        {
            try{
                string result=string.Empty;
                if(op=="+")
                result=(getal1+getal2).ToString();
                if(op=="-")
                result=(getal1-getal2).ToString();
                if(op=="*")
                result=(getal1*getal2).ToString();
                if(string.IsNullOrEmpty(result)){
                    return new BadRequestObjectResult("Ongeldige operator");
                }   
                CalculationResult calculationResult = new CalculationResult
                {
                    Operator=op,
                    Result=result
                };

                return new OkObjectResult(result);
            }
            catch (System.Exception){
                return new StatusCodeResult(500);
            }
        }
    }
}
