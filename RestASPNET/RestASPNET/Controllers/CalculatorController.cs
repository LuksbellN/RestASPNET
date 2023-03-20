using Microsoft.AspNetCore.Mvc;

namespace RestASPNET.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sum/{firstNumber}/{secondNumber}")]
    public IActionResult Sum(string firstNumber, string secondNumber)
    {
        if(isNumeric(firstNumber) && isNumeric(secondNumber))
        {
            var sum = ConvertToDouble(firstNumber) + ConvertToDouble(secondNumber);
            return Ok(sum.ToString());
        }

        return BadRequest("Invalid Input");
    }


    [HttpGet("subtract/{firstNumber}/{secondNumber}")]
    public IActionResult Subtract(string firstNumber, string secondNumber)
    {
        if (isNumeric(firstNumber) && isNumeric(secondNumber))
        {
            var subtract = ConvertToDouble(firstNumber) - ConvertToDouble(secondNumber);
            return Ok(subtract.ToString());
        }

        return BadRequest("Invalid Input");
    }


    [HttpGet("multip/{firstNumber}/{secondNumber}")]
    public IActionResult Multip(string firstNumber, string secondNumber)
    {
        if (isNumeric(firstNumber) && isNumeric(secondNumber))
        {
            var multip = ConvertToDouble(firstNumber) * ConvertToDouble(secondNumber);
            return Ok(multip.ToString());
        }

        return BadRequest("Invalid Input");
    }


    [HttpGet("div/{firstNumber}/{secondNumber}")]
    public IActionResult Div(string firstNumber, string secondNumber)
    {
        if (isNumeric(firstNumber) && isNumeric(secondNumber))
        {
            var div = ConvertToDouble(firstNumber) / ConvertToDouble(secondNumber);
            return Ok(div.ToString());
        }

        return BadRequest("Invalid Input");
    }


    [HttpGet("media/{firstNumber}/{secondNumber}")]
    public IActionResult Media(string firstNumber, string secondNumber)
    {
        if (isNumeric(firstNumber) && isNumeric(secondNumber))
        {
            var media = (ConvertToDouble(firstNumber) + ConvertToDouble(secondNumber))/2;
            return Ok(media.ToString());
        }

        return BadRequest("Invalid Input");
    }


    [HttpGet("sqrt/{firstNumber}")]
    public IActionResult Sqrt(string firstNumber)
    {
        if (isNumeric(firstNumber))
        {
            var sqrt = Math.Sqrt(ConvertToDouble(firstNumber));
            return Ok(sqrt.ToString());
        }

        return BadRequest("Invalid Input");
    }




    private double ConvertToDouble(string number)
    {
        double dec;
        if (double.TryParse(number, out dec))
        {
            return dec;
        }
        return 0;
    }

    private bool isNumeric(string number)
    {
        double strNumber;
        bool isNumber = double.TryParse(number, 
            System.Globalization.NumberStyles.Any, 
            System.Globalization.NumberFormatInfo.InvariantInfo,
            out strNumber);

        return isNumber;
    }
}
